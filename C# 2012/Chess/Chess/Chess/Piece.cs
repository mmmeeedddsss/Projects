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

namespace Chess
{
    class Piece
    {
        public Texture2D texture;
        public Rectangle rec;
        public string color;
        public Vector2 curpos;

        public Piece()
        {

        }

        public void draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();

            if( ( curpos.X + curpos.Y ) % 2 == 1 ) spritebatch.Draw(texture, rec, Color.WhiteSmoke);
            else spritebatch.Draw(texture, rec, Color.DarkGreen);

            spritebatch.End();
        }
    }
}
