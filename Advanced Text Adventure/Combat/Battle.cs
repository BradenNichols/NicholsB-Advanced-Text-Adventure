using Advanced_Text_Adventure.Misc;
using System;
using System.Collections.Generic;
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

            Player.player.key = null;
            Player.player.lastMoveKey = null;

            SetupLevel();
            Canvas.Draw();

            while (isActive)
            {
                Player.player.Input();
                Player.player.DoAction();
                Player.player.Draw();

                bool isEnemyAlive = false;

                foreach (BaseCharacter character in otherCharacters)
                {
                    if (character.isDead) continue;

                    character.DoAction();
                    character.Draw();
                }

                foreach (Enemy enemy in enemies)
                {
                    if (enemy.isDead) continue;
                    isEnemyAlive = true;

                    if (Player.player.lastMoveKey != null && !enemy.isActive)
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

            if (Player.player.isDead)
            {
                outcome = "Death";

                Reader.WriteLine("you died...", 25, ConsoleColor.Red);
                Thread.Sleep(750);
            } else
                outcome = "Win";

            Battle.activeBattle = null;
        }

        public void SetupLevel()
        {
            if (levelName == "Tutorial")
            {
                //enemies.Add(new TestEnemy("Goku"));
                
                for (int i = 0; i < new Random().Next(8, 14); i++)
                    enemies.Add(new TestEnemy("Goku"));
            }
        }
    }
}
