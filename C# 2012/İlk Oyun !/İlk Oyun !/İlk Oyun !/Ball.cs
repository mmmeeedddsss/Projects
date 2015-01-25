using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace İlk_Oyun__
{
    class Ball
    {
        public Vector2 Position, Velocity;
        public Texture2D Texture;
        int StageX, StageY;

        public Ball(Texture2D tex,Vector2 pos,Vector2 vel,int stagex,int stagey)
        {
            Texture = tex;
            Position = pos;
            Velocity = vel;
            StageX = stagex;
            StageY = stagey;
        }

        public void Update(GameTime gametime)
        {
            Position += Velocity;
            if ((Position.X + Texture.Width >= StageX ) || ( Position.X <= 0 ) )
            {
                Velocity.X = -(Velocity.X);
            }
            if ((Position.Y <= 0))
            {
                Velocity.Y = -(Velocity.Y);
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.Draw(Texture, Position, Color.White);
            spritebatch.End();
        }
        
    }
}
