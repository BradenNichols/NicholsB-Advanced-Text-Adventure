using Advanced_Text_Adventure.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Text_Adventure
{
    public abstract class Enemy : BaseCharacter
    {
        private Random random = new();
        private (float, float) randomSpeed = (0.35f, 0.65f);

        private Stopwatch watch = new();
        private int timeToActivate;

        public bool isActive = false;
        public (float, float) deflectDirection = (0, 0);

        protected Enemy(string name, float health, float maxHealth) : base(name: name, isEnemy: true, health: health, maxHealth: maxHealth)
        {
            position.Item1 = random.Next(1, Canvas.width) + Canvas.baseWidth;
            position.Item2 = random.Next(1, Canvas.height) + Canvas.baseHeight;

            color = ConsoleColor.Red;
            speed = (float)random.Next((int)(randomSpeed.Item1 * 100), (int)(randomSpeed.Item2 * 100)) / 100;

            timeToActivate = random.Next(400, 800);
        }

        public void SetActive(bool isActive)
        {
            this.isActive = isActive;

            if (isActive)
                watch.Start();
        }

        public override void DoAction()
        {
            if (watch.ElapsedMilliseconds <= timeToActivate)
                return;
            else
                watch.Stop();

            (float, float) playerPosition = Player.player.position;
            (float, float) flooredPosition;

            // Path to player

            flooredPosition = (MathF.Floor(position.Item1), MathF.Floor(position.Item2));

            if (playerPosition.Item2 < flooredPosition.Item2)
                position.Item2 -= speed;
            else if (playerPosition.Item2 > flooredPosition.Item2)
                position.Item2 += speed;

            if (playerPosition.Item1 < flooredPosition.Item1)
                position.Item1 -= speed;
            else if (playerPosition.Item1 > flooredPosition.Item1)
                position.Item1 += speed;

            // Clamp

            bool isOnMapBounds = ClampPosition();

            /*
            if (!isOnMapBounds)
            {
                Die();
                return;
            }*/

            flooredPosition = (MathF.Floor(position.Item1), MathF.Floor(position.Item2));

            // Check enemy collision

            foreach (Enemy enemy in Battle.activeBattle.enemies)
            {
                if (enemy != this && !enemy.isDead)
                {
                    (float, float) enemyFloored = (MathF.Floor(enemy.position.Item1), MathF.Floor(enemy.position.Item2));

                    if (enemyFloored.Equals(flooredPosition))
                    {
                        position = prevPosition;
                        break;
                    }
                }
            }

            if (playerPosition.Item1 == flooredPosition.Item1 && playerPosition.Item2 == flooredPosition.Item2)
            {
                // Hit player

                Player.player.TakeDamage(1);
            }
        }
    }
}
