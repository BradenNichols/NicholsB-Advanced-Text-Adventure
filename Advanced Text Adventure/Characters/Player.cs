using Advanced_Text_Adventure.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Text_Adventure
{
    public class Player : BaseCharacter
    {
        public static Player player;

        public ConsoleKeyInfo input = new();
        public string key;

        public bool canDeflect = true;
        public bool isDeflecting = false;
        public float deflectEnemySpeed = 1.5f;

        public string lastMoveKey;
        private int deflectCount = 0;

        public Player(string name, float health = 1, float maxHealth = 2) : base(name: name, isEnemy: false, health: health, maxHealth: maxHealth)
        {
            position.Item1 = new Random().Next(0, Canvas.width) + Canvas.baseWidth;
            position.Item2 = new Random().Next(0, Canvas.height) + Canvas.baseHeight;

            color = ConsoleColor.Green;
        }
        public void Input()
        {
            if (Console.KeyAvailable)
            {
                input = Console.ReadKey(true);
                key = input.Key.ToString().ToLower();

                if (key.Equals("f"))
                    Deflect();
                else if (key.Equals("r"))
                    Player.player.Die();
            }
        }

        // Deflect

        public void Deflect()
        {
            if (!canDeflect && !isDead)
                return;

            int newCount = deflectCount++;
            deflectCount = newCount;

            canDeflect = false;
            isDeflecting = true;

            color = ConsoleColor.DarkYellow;

            Task.Run(() =>
            {
                Thread.Sleep(450);

                isDeflecting = false;
                color = ConsoleColor.Green;

                Thread.Sleep(650);

                if (deflectCount != newCount)
                    return;

                canDeflect = true;
            });
        }

        public void DeflectEnemy(Enemy enemy)
        {
            canDeflect = true;

            enemy.isDeflected = true;
            enemy.color = ConsoleColor.Blue;
            enemy.deflectDirection = (0, 0);

            // Horizontal

            if (key.Equals("leftarrow"))
                enemy.deflectDirection.Item1 -= deflectEnemySpeed;
            if (key.Equals("rightarrow"))
                enemy.deflectDirection.Item1 += deflectEnemySpeed;

            // Vertical

            if (key.Equals("uparrow"))
                enemy.deflectDirection.Item2 -= deflectEnemySpeed;
            if (key.Equals("downarrow"))
                enemy.deflectDirection.Item2 += deflectEnemySpeed;

            // Backup

            if (enemy.deflectDirection.Item1 == 0 && enemy.deflectDirection.Item2 == 0)
            {
                if (lastMoveKey.Equals("leftarrow"))
                    enemy.deflectDirection.Item1 -= deflectEnemySpeed;
                else if (lastMoveKey.Equals("rightarrow"))
                    enemy.deflectDirection.Item1 += deflectEnemySpeed;
                else if (lastMoveKey.Equals("uparrow"))
                    enemy.deflectDirection.Item2 -= deflectEnemySpeed;
                else // downarrow
                    enemy.deflectDirection.Item2 += deflectEnemySpeed;
            }
        }

        // Loop Stuff

        public override void DoAction()
        {
            if (key != null && !isDeflecting)
            {
                // Movement
                
                if (key.Equals("leftarrow"))
                {
                    position.Item1 -= speed;
                    lastMoveKey = "leftarrow";
                } else if (key.Equals("rightarrow"))
                {
                    position.Item1 += speed;
                    lastMoveKey = "rightarrow";
                } else if (key.Equals("uparrow"))
                {
                    position.Item2 -= speed;
                    lastMoveKey = "uparrow";
                } else if (key.Equals("downarrow"))
                {
                    position.Item2 += speed;
                    lastMoveKey = "downarrow";
                }
            }

            ClampPosition();
        }
    }
}
