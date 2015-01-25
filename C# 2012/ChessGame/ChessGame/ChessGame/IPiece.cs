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

namespace ChessGame
{
    public interface IPiece
    {
        Texture2D tex
        {
            get;
            set;
        }
        Rectangle rect
        {
            get;
            set;
        }
        string pieceName
        {
            get;
            set;
        }
        string color
        {
            get;
            set;
        }

        IPiece Clone();
        string toString();
        bool canPlayTo(Vector2 x,string[,] table);
        void Draw(SpriteBatch spritebatch,int wh);
        void calcManArea(ref char[,] manacearea, string[,] table);

    }
}
