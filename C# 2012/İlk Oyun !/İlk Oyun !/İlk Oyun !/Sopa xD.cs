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
    class Sopa_xD
    {
        public Texture2D texture;
        public Vector2 position;
        MouseState ms;
        int StageWidht, StageHeight;

        public Sopa_xD(Texture2D tex,int widht,int height)
        {
            texture = tex;
            position = new Vector2( widht/2 , height -(tex.Height + 20) );
            StageWidht = widht;
            StageHeight = height;
        }

        public void Update(GameTime gametime)
        {
            ms = Mouse.GetState();
            // on Windows, the current state of the mouse cursor can be obtained at any time.
            #if WINDOWS
            position.X = ms.X;
            #endif
            if (position.X < 15)
            {
                position.X = 15;
            }

            else if (position.X + texture.Width > StageWidht - 15)
            {
                position.X = StageWidht - texture.Width - 15;
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.Draw(texture, position, Color.White);
            spritebatch.End();
        }
    }
}
