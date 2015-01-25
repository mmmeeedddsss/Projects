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

namespace Game2.cs
{
    class Block
    {
        public Texture2D Texture;
        public Color color;
        public Vector2 position;
        static Random r = new Random();
        public Block(Texture2D tex,Vector2 pos,bool isHard_mode)
        {
            int x;
            if (isHard_mode == true)
                x = 5;
            else
                x = 4;
            switch(r.Next(x))
            {
                case 0:
                    color = Color.Red;
                    break;
                case 1:
                    color = Color.Blue;
                    break;
                case 2:
                    color = Color.Purple;
                    break;
                case 3:
                    color = Color.Yellow;
                    break;
                default:
                    color = Color.Gray;
                    break;
            }

            Texture = tex;
            position = pos;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Texture, position, color);
        }
    }
}
