using Advanced_Text_Adventure.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Text_Adventure
{
    public abstract class BaseCharacter
    {
        public string name;
        public bool isEnemy = false;

        public float health;
        public float maxHealth;

        public (float, float) position = (0, 0);
        protected (float, float) prevPosition = (0, 0);

        public float speed = 1;
        public ConsoleColor color;
        public string image = "█";

        public bool isDead = false;

        public BaseCharacter(string name, bool isEnemy = false, float health = 1, float maxHealth = 1)
        {
            this.name = name;
            this.isEnemy = isEnemy;

            this.health = health;
            this.maxHealth = maxHealth;
        }

        // Update

        public abstract void DoAction();

        public bool ClampPosition()
        {
            (float, float) oldPosition = position;
            position.Item1 = Math.Clamp(position.Item1, Canvas.baseWidth + 1, Canvas.width + Canvas.baseWidth - 1);
            position.Item2 = Math.Clamp(position.Item2, Canvas.baseHeight + 1, Canvas.height + Canvas.baseHeight - 1);

            bool isOnMapBounds = position.Equals(oldPosition);
            return isOnMapBounds;
        }

        // Canvas

        public void Draw()
        {
            if (isDead || (position.Item1 != prevPosition.Item1 || position.Item2 != prevPosition.Item2))
            {
                Console.SetCursorPosition((int)prevPosition.Item1, (int)prevPosition.Item2);
                Reader.Write(" ");
            }

            if (!isDead)
            {
                Console.SetCursorPosition((int)position.Item1, (int)position.Item2);
                prevPosition = (position.Item1, position.Item2);

                Reader.Write(image, -1, color);
            }
        }

        // Damage

        public void TakeDamage(int damage)
        {
            health = Math.Clamp(health - damage, 0, maxHealth);
            Debug.Print(health.ToString());

            if (health <= 0)
                Die();
        }

        public void Die()
        {
            if (isDead)
                return;

            isDead = true;
            health = 0;

            Draw();
        }

        // Game Reset

        public void Reset()
        {
            health = maxHealth;
            isDead = false;
        }
    }
}
