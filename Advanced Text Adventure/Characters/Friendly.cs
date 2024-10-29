using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Text_Adventure
{
    public class Friendly : BaseCharacter
    {
        public Friendly(string name, float health = 100, float maxHealth = 100) : base(name: name, isEnemy: false, health: health, maxHealth: maxHealth)
        {

        }
    }
}
