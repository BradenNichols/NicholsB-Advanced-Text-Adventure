using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Text_Adventure
{
    public class Bullet : BaseCharacter
    {
        public (float, float) direction;

        public Bullet((float, float) position, (float, float) direction, float health = 1) : base(name: "Bullet", isEnemy: false, health: health, maxHealth: health)
        {
            this.position = position;
            this.prevPosition = position;
            this.direction = direction;
        }

        public override void DoAction()
        {
            // Move in deflect direction

            position.Item1 += direction.Item1;
            position.Item2 += direction.Item2;

            // Clamp

            bool isOnMapBounds = ClampPosition();

            if (!isOnMapBounds)
            {
                Die();
                return;
            }

            // Check bullet collision

            (float, float) flooredPosition = (MathF.Floor(position.Item1), MathF.Floor(position.Item2));

            foreach (Enemy enemy in Battle.activeBattle.enemies)
            {
                if (!enemy.isDead)
                {
                    (float, float) enemyFloored = (MathF.Floor(enemy.position.Item1), MathF.Floor(enemy.position.Item2));

                    if (MathF.Abs(enemyFloored.Item1 - flooredPosition.Item1) <= 1 && MathF.Abs(enemyFloored.Item2 - flooredPosition.Item2) <= 1)
                        {enemy.Die(); Die();}
                }
            }
        }
    }
}
