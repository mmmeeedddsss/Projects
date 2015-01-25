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

namespace bball
{
    class Ball
    {
        float Vy, t = 0, g,Vx;
        Vector2 ballpos;
        int currstate = 1;
        Texture2D Tex;
        float k;
        int rotation = 0;

        public Ball( Texture2D tex,Vector2 pos ,float Vo,float g,float Vx,float k)
        {
            ballpos = pos;
            Tex = tex;
            this.Vy = Vo;
            this.Vx = Vx;
            this.g = g;
            this.k = k;
        }

        public void Update(GameTime gt, GraphicsDeviceManager graphics)
        {
            if(ballpos.X < 0 || ballpos .X > graphics.PreferredBackBufferWidth - 50 - 25 )
            {
                Vx = -Vx;
            }

            if (currstate == 1)
            {
                ballpos.Y = -(Vy * t) + (g * t * t) / 2 + graphics.GraphicsDevice.Viewport.Height - 75;
                t += (float)gt.ElapsedGameTime.TotalSeconds;
                ballpos.X += Vx;
            }
            if (ballpos.Y + 74 > graphics.PreferredBackBufferHeight)
            {
                if (Vy < 0)
                {
                    Vy = -Vy;
                }
                if (Vy < 2)
                    Vy = 0;
                if (Vy < 2 && Vx < 1)
                    currstate = 0;
                Vy = k * Vy;
                if (Vx != 0)
                {
                    if (Vx > 0) { Vx -= (float)0.1; rotation += 1; }
                    else if (Vx < 0) { Vx += (float)0.1; rotation -= 1; }
                }

                t = 0;
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Tex , new Rectangle((int)ballpos.X, (int)ballpos.Y, 75, 75), Color.WhiteSmoke);
        }
    }
}
