using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Text_Adventure
{
    public class TestEnemy : Enemy
    {
        public TestEnemy(string name, float health = 1, float maxHealth = 1) : base(name: name, health: health, maxHealth: maxHealth)
        {
            
        }
    }
}
