﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Text_Adventure
{
    public class TestEnemy : Enemy
    {
        public TestEnemy(string name, float health = 100, float maxHealth = 100) : base(name: name, health: health, maxHealth: maxHealth)
        {
            
        }
    }
}
