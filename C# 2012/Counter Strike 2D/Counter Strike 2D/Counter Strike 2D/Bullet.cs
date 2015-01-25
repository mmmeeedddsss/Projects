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
    class Bullet
    {
        Texture2D tex;
        public Vector2 pos;
        float angle;
        int speed;
        bool lol;
        int damage;
        bool isfromcomp;

        public Bullet(Texture2D te,int dmg, Vector2 po, double angl,float test, int spee, bool fromcmp)
        {
            isfromcomp = fromcmp;
            damage = dmg;
            tex = te;
            pos = po;
            angl = -angl;
            angle = MathHelper.ToDegrees((float)Math.Atan(angl));
            if (test > 0)
                lol = true;
            angle += 90;
            //System.Windows.Forms.MessageBox.Show("" + angl);
            speed = spee;
            double x = 25 * Math.Sin(MathHelper.ToRadians(angle));
            double y = 25 * Math.Cos(MathHelper.ToRadians(angle));
            if (lol) { x = -x; y = -y; }
            pos.X += (float)x;
            pos.Y += (float)y;
        }

        public void Update(List<Player> players,Player1C player1)
        {
            if (!isfromcomp)
            {
                double x = speed * Math.Sin(MathHelper.ToRadians(angle));
                double y = speed * Math.Cos(MathHelper.ToRadians(angle));
                if (lol) { x = -x; y = -y; }
                if (x == 0)
                {
                    x = 1;
                }
                for (int i = 0; i < Math.Abs(x); i++)
                {
                    pos.X += (x < 0) ? (-1) : (1);
                    pos.Y += (x < 0) ? -(float)(y / x) : (float)(y / x);
                    for (int j = 0; j < players.Count; j++)
                    {
                        Player p = players[j];
                        if (p.isdied == false && new Rectangle((int)pos.X, (int)pos.Y, 5, 5).Intersects(new Rectangle((int)p.playerPos.X, (int)p.playerPos.Y, 25, 25)))
                        {
                            p.hitted(damage, players);
                            end();
                        }
                    }
                }
            }
            else
            {
                double x = speed * Math.Sin(MathHelper.ToRadians(angle));
                double y = speed * Math.Cos(MathHelper.ToRadians(angle));
                if (lol) { x = -x; y = -y; }
                if (x == 0)
                {
                    x = 1;
                }
                for (int i = 0; i < Math.Abs(x); i++)
                {
                    pos.X += (x < 0) ? (-1) : (1);
                    pos.Y += (x < 0) ? -(float)(y / x) : (float)(y / x);
                    if (new Rectangle((int)pos.X, (int)pos.Y, 5, 5).Intersects(new Rectangle((int)player1.playerPos.X + 1024, (int)player1.playerPos.Y + 768, 25, 25)))
                    {
                        player1.hitted(damage);
                        end();
                    }
                }
            }
        }
        private void end()
        {
            pos = new Vector2(-10000, -10000);
        }

        public void Draw(SpriteBatch sb,Vector2 origin)
        {
            sb.Draw(tex,new Rectangle((int)pos.X + (int)origin.X,(int)pos.Y + (int)origin.Y,6,10),null,Color.White,MathHelper.ToRadians(-angle - 180),Vector2.Zero,SpriteEffects.None,0);
        }
    }
}
