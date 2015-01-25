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

namespace isimsiz
{
    class Node
    {
        public Vector2 pos;
        public Texture2D tex;
        public int[] conns;
        public Rectangle rec;

        public Node(Vector2 poss,int[] connss, Texture2D texx)
        {
            pos = poss;
            conns = connss;
            tex = texx;
        }

        public void draw(SpriteBatch spritebatch)
        {
            rec = new Rectangle((int)pos.X, (int)pos.Y, 20, 20);
            spritebatch.Draw(tex, rec, Color.White);
        }
    }
}
