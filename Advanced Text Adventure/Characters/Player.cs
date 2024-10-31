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

        private int deflectCount = 0;

        public Player(string name, float health = 100, float maxHealth = 100) : base(name: name, isEnemy: false, health: health, maxHealth: maxHealth)
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
                key = input.KeyChar.ToString().ToLower();

                if (key.Equals("f"))
                    Deflect();
            }
        }

        // Deflect

        public void Deflect()
        {
            if (!canDeflect)
                return;

            int newCount = deflectCount++;
            deflectCount = newCount;

            canDeflect = false;
            isDeflecting = true;

            color = ConsoleColor.DarkYellow;

            Task.Run(() =>
            {
                Thread.Sleep(550);

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
            enemy.deflectDirection = (0, 0);

            // Horizontal

            if (key.Equals("a"))
                enemy.deflectDirection.Item1 -= deflectEnemySpeed;
            if (key.Equals("d"))
                enemy.deflectDirection.Item1 += deflectEnemySpeed;

            // Vertical

            if (key.Equals("w"))
                enemy.deflectDirection.Item2 -= deflectEnemySpeed;
            if (key.Equals("s"))
                enemy.deflectDirection.Item2 += deflectEnemySpeed;

            // Backup

            if (enemy.deflectDirection.Item1 == 0 && enemy.deflectDirection.Item2 == 0)
                enemy.deflectDirection.Item2 += deflectEnemySpeed;
        }

        // Loop Stuff

        public override void DoAction()
        {
            /*
            if (Console.KeyAvailable)
                input = Console.ReadKey(true);*/

            if (key != null && !isDeflecting)
            {
                // Horizontal

                if (key.Equals("a"))
                    position.Item1 -= speed;
                if (key.Equals("d"))
                    position.Item1 += speed;

                // Vertical

                if (key.Equals("w"))
                    position.Item2 -= speed;
                if (key.Equals("s"))
                    position.Item2 += speed;
            }

            ClampPosition();
        }
    }
}
