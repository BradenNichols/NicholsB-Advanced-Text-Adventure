using Advanced_Text_Adventure.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Advanced_Text_Adventure
{
    public class Player : BaseCharacter
    {
        public static Player player;

        public ConsoleKeyInfo input = new();
        public string key;

        public bool canShoot = true;
        public bool isShooting = false;
        public float bulletSpeed = 1f;

        public string lastMoveKey = "downarrow";
        public bool hasMoved = false;

        public Player(string name, float health = 1, float maxHealth = 2) : base(name: name, isEnemy: false, health: health, maxHealth: maxHealth)
        {
            position.Item1 = new Random().Next(0, Canvas.width) + Canvas.baseWidth;
            position.Item2 = new Random().Next(0, Canvas.height) + Canvas.baseHeight;

            color = ConsoleColor.Green;
        }
        public void Input()
        {
            if (Console.KeyAvailable)
            {
                input = Console.ReadKey(true);
                key = input.Key.ToString().ToLower();

                if (key.Equals("f"))
                    Shoot();
                else if (key.Equals("r"))
                    Player.player.Die();
            }
        }

        bool isInput(string keyName)
        {
            if (key == null) return false;

            if (key.Equals("leftarrow"))
                return true;
            else if (key.Equals("rightarrow"))
                return true;
            else if (key.Equals("uparrow"))
                return true;
            else if (key.Equals("downarrow"))
                return true;

            return false;
        }

        public void Reset()
        {
            base.Reset();

            key = null;
            lastMoveKey = "downarrow";
            hasMoved = false;
        }

        // Gun

        public void Shoot()
        {
            if (!canShoot || isDead || !hasMoved) return;

            canShoot = false;
            Task.Delay(350).ContinueWith(_ => {canShoot = true;});

            (float, float) shootDirection = (0, 0);

            string currentKey = key;
            string image = "";

            if (!isInput(currentKey))
                currentKey = lastMoveKey;

            switch (currentKey) {
                case "leftarrow":
                    shootDirection.Item1 -= bulletSpeed;
                    image = "<";
                    break;
                case "rightarrow":
                    shootDirection.Item1 += bulletSpeed;
                    image = ">";
                    break;
                case "uparrow":
                    shootDirection.Item2 -= bulletSpeed;
                    image = "▲";
                    break;
                default:
                    shootDirection.Item2 += bulletSpeed;
                    image = "▼";
                    break;
            }

            if (currentKey.Equals("leftarrow"))
                shootDirection.Item1 -= bulletSpeed;
            else if (currentKey.Equals("rightarrow"))
                shootDirection.Item1 += bulletSpeed;
            else if (currentKey.Equals("uparrow"))
                shootDirection.Item2 -= bulletSpeed;
            else // downarrow
                shootDirection.Item2 += bulletSpeed;

            Bullet newBullet = new(position, shootDirection);
            newBullet.image = image;

            Battle.activeBattle.otherCharacters.Add(newBullet);
        }

        // Loop Stuff

        public override void DoAction()
        {
            if (key != null)
            {
                // Movement
                
                if (key.Equals("leftarrow"))
                {
                    position.Item1 -= speed;
                    lastMoveKey = "leftarrow";
                    hasMoved = true;
                } else if (key.Equals("rightarrow"))
                {
                    position.Item1 += speed;
                    lastMoveKey = "rightarrow";
                    hasMoved = true;
                } else if (key.Equals("uparrow"))
                {
                    position.Item2 -= speed;
                    lastMoveKey = "uparrow";
                    hasMoved = true;
                } else if (key.Equals("downarrow"))
                {
                    position.Item2 += speed;
                    lastMoveKey = "downarrow";
                    hasMoved = true;
                }
            }

            ClampPosition();
        }
    }
}
