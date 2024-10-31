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
        private (float, float) randomSpeed = (0.35f, 0.6f);

        public bool isDeflected = false;
        public (float, float) deflectDirection = (0, 0);

        protected Enemy(string name, float health, float maxHealth) : base(name: name, isEnemy: true, health: health, maxHealth: maxHealth)
        {
            position.Item1 = random.Next(0, Canvas.width) + Canvas.baseWidth;
            position.Item2 = random.Next(0, Canvas.height) + Canvas.baseHeight;

            color = ConsoleColor.Red;
            speed = (float)random.Next((int)(randomSpeed.Item1 * 100), (int)(randomSpeed.Item2 * 100)) / 100;
        }

        public override void DoAction()
        {
            (float, float) playerPosition = Player.player.position;
            (float, float) flooredPosition;

            if (!isDeflected)
            {
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
            } else
            {
                // Move in deflect direction

                position.Item1 += deflectDirection.Item1;
                position.Item2 += deflectDirection.Item2;
            }


            // Clamp

            bool isOnMapBounds = ClampPosition();

            if (!isOnMapBounds && isDeflected)
            {
                Die();
                return;
            }

            flooredPosition = (MathF.Floor(position.Item1), MathF.Floor(position.Item2));

            // Check collision

            if (!isDeflected)
            {
                // Check between enemies

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

                    if (Player.player.isDeflecting)
                    {
                        Player.player.DeflectEnemy(this);
                        return;
                    }

                    Player.player.Die();
                }
            } else
            {
                // Hit enemies

                foreach (Enemy enemy in Battle.activeBattle.enemies)
                {
                    if (enemy != this && !enemy.isDead)
                    {
                        (float, float) enemyFloored = (MathF.Floor(enemy.position.Item1), MathF.Floor(enemy.position.Item2));

                        if (enemyFloored.Equals(flooredPosition))
                            enemy.Die();
                    }
                }
            }
        }
    }
}
