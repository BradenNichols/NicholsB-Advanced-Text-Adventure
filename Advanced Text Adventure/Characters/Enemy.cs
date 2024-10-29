using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Text_Adventure
{
    public abstract class Enemy : BaseCharacter
    {
        protected Enemy(string name, float health, float maxHealth) : base(name: name, isEnemy: true, health: health, maxHealth: maxHealth)
        {

        }
    }
}
