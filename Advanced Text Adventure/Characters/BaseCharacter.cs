using System;
using System.Collections.Generic;
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

        public bool isDead = false;

        public bool canBeDowned = false;
        public bool isDowned = false;

        public BaseCharacter(string name, bool isEnemy = false, float health = 100, float maxHealth = 100)
        {
            this.name = name;
            this.isEnemy = isEnemy;

            if (!isEnemy)
                canBeDowned = true;

            this.health = health;
            this.maxHealth = maxHealth;
        }

        // Damage

        public void TakeDamage(float damage)
        {
            health = Math.Clamp(health - damage, 0, maxHealth);

            if (health <= 0 && !isDowned && canBeDowned)
                Down();
        }

        public void Down()
        {
            if (isDowned)
                return;

            isDowned = true;
            health = 0;
        }
    }
}
