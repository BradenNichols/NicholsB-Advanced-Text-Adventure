using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Text_Adventure
{
    public class Battle
    {
        protected bool isActive = false;
        private Friendly[] friendlyParty;
        private Enemy[] enemyParty;
        
        public Battle(Friendly[] friendlies, Enemy[] enemies)
        {
            friendlyParty = friendlies;
            enemyParty = enemies;
        }

        public void Start()
        {
            isActive = true;
            Reader.WriteLine("can you believe it guys. ", 30);
            Reader.Write("Christmas. ", 60, ConsoleColor.Green);
            Reader.Write("just a week away", 50);
        }
    }
}
