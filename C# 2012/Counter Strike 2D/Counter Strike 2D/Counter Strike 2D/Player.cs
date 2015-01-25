using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Counter_Strike_2D
{
    class Player
    {
        const int speed = 14;
        public Vector2 playerPos;
        Texture2D man;
        Texture2D blood;
        int wh;
        int hg;
        int manwh;
        int manhg;
        public Gun gun;
        double lastShoot;
        public int health;
        public bool isdied = false;
        public bool addmoney = false;

        public Player(Vector2 pos, Texture2D tex, Vector2 screen, int manx, int many,Texture2D blood)
        {
            playerPos = pos;
            man = tex;
            wh = (int)screen.X;
            hg = (int)screen.Y;
            manwh = manx;
            manhg = many;
            gun = new p90();
            lastShoot = -10000;
            health = 100;
            this.blood = blood;
            Random a = new Random();
            switch (a.Next(5))
            {
                case 0: gun = new glock();
                    break;
                case 1: gun = new p90();
                    break;
                case 2: gun = new ak47();
                    break;
                case 3: gun = new m4a1();
                    break;
                case 4: gun = new awp();
                    break;
            }
        }
        public void hitted(int dmg, List<Player> players)
        {
            this.health -= dmg;
            if (this.health < 0)
                die(players);
        }

        public void die(List<Player> players)
        {
            this.isdied = true;
            this.addmoney = true;
        }

        public Player()
        {

        }

        public Vector2 Update(GameTime gt,Vector2 player1Pos)
        {
            if ((gt.TotalGameTime.TotalMilliseconds - lastShoot) > gun.mdelay)
            {
                lastShoot = gt.TotalGameTime.TotalMilliseconds;
                return new Vector2( player1Pos.X + wh, player1Pos.Y + hg );
            }
            return Vector2.Zero;
        }

        public void Draw(SpriteBatch sb, Vector2 origin)
        {
            if (!isdied)
                sb.Draw(man, new Rectangle((int)origin.X + (int)playerPos.X, (int)origin.Y + (int)playerPos.Y, manwh, manhg), Color.White);
            else
                sb.Draw(blood, new Rectangle((int)origin.X + (int)playerPos.X, (int)origin.Y + (int)playerPos.Y, manwh, manhg), Color.White);
        }
    }
}
