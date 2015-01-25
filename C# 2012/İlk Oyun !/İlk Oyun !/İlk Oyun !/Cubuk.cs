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
    class Cubuk
    {
        public Texture2D Texture;
        public Vector2 Position;
        int StageWidht, StageHeight;
        public Cubuk(Texture2D tex,Vector2 pos,int widht,int height)
        {
            Texture = tex;
            Position = pos;
            StageWidht = widht;
            StageHeight = height;
        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.Draw(Texture, Position, Color.White);
            spritebatch.End();
        }
    }
}
