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

        public string levelName = "";
        public List<Enemy> enemies;
        
        public Battle(string levelName)
        {
            this.levelName = levelName;
            enemies = new List<Enemy>();
        }

        // Main

        public void Start()
        {
            isActive = true;
            Battle.activeBattle = this;

            SetupLevel();
            Canvas.Draw();

            while (isActive)
            {
                Player.player.Input();
                Player.player.DoAction();
                Player.player.Draw();

                foreach (Enemy enemy in enemies)
                {
                    if (enemy.isDead) continue;

                    enemy.DoAction();
                    enemy.Draw();
                }

                Thread.Sleep(50);

                if (Player.player.isDead)
                    break;
            }

            End();
        }

        public void End()
        {
            if (!isActive)
                return;

            isActive = false;

            Thread.Sleep(1000);
            Console.Clear();

            if (Player.player.isDead)
            {
                Reader.WriteLine("you died haha", 20, ConsoleColor.Red);
                Thread.Sleep(1500);
            }

            Battle.activeBattle = null;
        }

        public void SetupLevel()
        {
            if (levelName == "Tutorial")
            {
                //enemies.Add(new TestEnemy("Goku"));
                
                for (int i = 0; i < new Random().Next(3, 5); i++)
                    enemies.Add(new TestEnemy("Goku"));
            }
        }
    }
}
