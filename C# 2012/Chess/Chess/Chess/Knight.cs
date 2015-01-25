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
    class Knight : Piece
    {
        public Knight(Texture2D tex,Rectangle rec,string color,Vector2 curpos)
        {
            this.texture = tex;
            this.rec = rec;
            this.color = color;
            this.curpos = curpos;
        }

        public bool canplay(Vector2 to, string[,] table, List<Info> tableInfo, char[,] manaceArea)
        {
            int x, y;
            x = ( int ) Math.Abs( to.X - curpos.X );
            y = ( int ) Math.Abs( to.Y - curpos.Y );
            if ((x + y) == 3 && x != 0 && y != 0)
            {
                foreach( Info info in tableInfo )
                {
                    if (info.pos.X == to.X && info.pos.Y == to.Y)
                    {
                        if (this.color == info.color)
                            return false;
                    }
                }
                // add şah mal oluyomu konutroll
                return true;
            }
            return false;
        }

        public void manace_area(ref char[,] manaceArea, List<Info> tableinfo, string[,] table)
        {
            if (curpos.X + 2 >= 0 && curpos.X + 2 <= 7 && curpos.Y + 1 >= 0 && curpos.Y + 1 <= 7)  manaceArea[(int)curpos.X + 2, (int)curpos.Y + 1] = 'X';
            if (curpos.X + 2 >= 0 && curpos.X + 2 <= 7 && curpos.Y - 1 >= 0 && curpos.Y - 1 <= 7)  manaceArea[(int)curpos.X + 2, (int)curpos.Y - 1] = 'X';
            if (curpos.X - 2 >= 0 && curpos.X - 2 <= 7 && curpos.Y + 1 >= 0 && curpos.Y + 1 <= 7)  manaceArea[(int)curpos.X - 2, (int)curpos.Y + 1] = 'X';
            if (curpos.X - 2 >= 0 && curpos.X - 2 <= 7 && curpos.Y - 1 >= 0 && curpos.Y - 1 <= 7)  manaceArea[(int)curpos.X - 2, (int)curpos.Y - 1] = 'X';
            if (curpos.X - 1 >= 0 && curpos.X - 1 <= 7 && curpos.Y + 2 >= 0 && curpos.Y + 2 <= 7)  manaceArea[(int)curpos.X - 1, (int)curpos.Y + 2] = 'X';
            if (curpos.X + 1 >= 0 && curpos.X + 1 <= 7 && curpos.Y + 2 >= 0 && curpos.Y + 2 <= 7)  manaceArea[(int)curpos.X + 1, (int)curpos.Y + 2] = 'X';
            if (curpos.X + 1 >= 0 && curpos.X + 1 <= 7 && curpos.Y - 2 >= 0 && curpos.Y - 2 <= 7)  manaceArea[(int)curpos.X + 1, (int)curpos.Y - 2] = 'X';
            if (curpos.X - 1 >= 0 && curpos.X - 1 <= 7 && curpos.Y - 2 >= 0 && curpos.Y - 2 <= 7)  manaceArea[(int)curpos.X - 1, (int)curpos.Y - 2] = 'X';

        }
    }
}
