using System;
using System.Collections.Generic;
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
            this.direction = direction;
        }

        public override void DoAction()
        {
            throw new NotImplementedException();
        }
    }
}
