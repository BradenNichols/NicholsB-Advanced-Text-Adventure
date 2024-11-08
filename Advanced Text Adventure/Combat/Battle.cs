using Advanced_Text_Adventure.Misc;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Text_Adventure
{
    public class Battle
    {
        public static Battle activeBattle;
        public bool isActive = false;
        public string outcome = "";

        public string levelName = "";
        public List<Enemy> enemies;
        public List<BaseCharacter> otherCharacters;
        
        public Battle(string levelName)
        {
            this.levelName = levelName;
            enemies = new List<Enemy>();
            otherCharacters = new List<BaseCharacter>();
        }

        // Main

        public void Start()
        {
            isActive = true;
            Battle.activeBattle = this;

            Player.player.Reset();

            SetupLevel();
            Canvas.Draw();

            while (isActive)
            {
                Player.player.Input();
                Player.player.DoAction();

                bool isEnemyAlive = false;

                foreach (BaseCharacter character in otherCharacters)
                {
                    if (character.isDead) continue;

                    character.DoAction();
                    character.Draw();
                }

                Player.player.Draw();

                foreach (Enemy enemy in enemies)
                {
                    if (enemy.isDead) continue;
                    isEnemyAlive = true;

                    if (Player.player.hasMoved && !enemy.isActive)
                        enemy.SetActive(true);

                    enemy.DoAction();
                    enemy.Draw();
                }

                Thread.Sleep(50);

                if (Player.player.isDead || !isEnemyAlive)
                    break;
            }

            End();
        }

        public void End()
        {
            if (!isActive)
                return;

            isActive = false;

            Thread.Sleep(850);

            // Battle Outcome

            foreach (Enemy enemy in enemies)
            {
                if (!enemy.isDead)
                    enemy.Die();
            }

            Console.Clear();
            Console.CursorVisible = true;

            if (Player.player.isDead)
            {
                outcome = "Death";

                int dollarsLost = Math.Clamp(new Random().Next(0, 2), 0, Program.mySave.shopPoints);
                Program.mySave.shopPoints -= dollarsLost;
                Program.SaveData();

                Reader.WriteLine($"You died at {levelName}..", 35, ConsoleColor.Red);

                if (levelName != "Tutorial")
                {
                    if (dollarsLost > 0)
                        Reader.WriteLine($"and lost {dollarsLost} derek dollar(s).", 50, ConsoleColor.DarkRed);
                    else
                        Reader.WriteLine($"and lost nothing.", 50, ConsoleColor.Blue);
                }

                Thread.Sleep(750);
            } else
                outcome = "Win";

            Battle.activeBattle = null;
        }

        public void SetupLevel()
        {
            if (levelName == "Tutorial")
            {
                for (int i = 0; i < 4; i++)
                    enemies.Add(new TestEnemy("Goku"));
            } else if (levelName.Contains("Level"))
            {
                int levelNumber = Convert.ToInt32(levelName.Split("Level ")[1]);

                for (int i = 0; i < new Random().Next(5, 8) + levelNumber; i++)
                    enemies.Add(new TestEnemy("Goku"));
            }
        }
    }
}
