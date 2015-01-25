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
    class Pawn : Piece
    {
        public Pawn(Texture2D tex,Rectangle rec,string color,Vector2 curpos)
        {
            this.texture = tex;
            this.rec = rec;
            this.color = color;
            this.curpos = curpos;
        }

        public bool canplay(Vector2 to, string[,] table, List<Info> tableInfo, char[,] manaceArea)
        {
            if (to.Equals( curpos ) ) return false;
            if (color == "w")
            {
                if (curpos.Y == 6 && to.Y == 4 && table[(int)curpos.X,5] == " " && table[(int)curpos.X,4 ] == " " )
                    return true;
                if (curpos.Y - to.Y == 1 && table[(int)curpos.X, (int)curpos.Y - 1] == " ")
                    return true;
                if (curpos.Y - to.Y == 1 && Math.Abs(curpos.X - to.X) == 1)
                    foreach (Info info in tableInfo)
                        if (info.color != this.color && Math.Abs(info.pos.X - curpos.X) == 1 && curpos.Y - info.pos.Y == 1 )
                            return true;
                return false;
            }
            else
            {
                if (curpos.Y == 1 && to.Y - curpos.Y == 2 && table[(int)curpos.X , (int)curpos.Y + 1] == " " && table[(int)curpos.X, (int)curpos.Y + 2] == " ")
                    return true;
                if (to.Y - curpos.Y == 1 && table[(int)curpos.X, (int)curpos.Y + 1] == " ")
                    return true;
                if (to.X - curpos.X == 1 && Math.Abs(curpos.Y - to.Y) == 1)
                    foreach (Info info in tableInfo)
                        if (info.color != this.color)
                            return true;
                return false;
            }
        }

        public void manace_area(ref char[,] manaceArea, List<Info> tableinfo, string[,] table)
        {
            if (this.color == "b")
            {
                if (curpos.X + 1 >= 0 && curpos.X + 1 <= 7 && curpos.Y - 1 >= 0 && curpos.Y - 1 <= 7) manaceArea[(int)curpos.X + 1, (int)curpos.Y + 1] = 'X';
                if (curpos.X - 1 >= 0 && curpos.X - 1 <= 7 && curpos.Y - 1 >= 0 && curpos.Y - 1 <= 7) manaceArea[(int)curpos.X - 1, (int)curpos.Y + 1] = 'X';
            }
            else
            {
                if (curpos.X + 1 >= 0 && curpos.X + 1 <= 7 && curpos.Y - 1 >= 0 && curpos.Y - 1 <= 7) manaceArea[(int)curpos.X + 1, (int)curpos.Y - 1] = 'X';
                if (curpos.X - 1 >= 0 && curpos.X - 1 <= 7 && curpos.Y - 1 >= 0 && curpos.Y - 1 <= 7) manaceArea[(int)curpos.X - 1, (int)curpos.Y - 1] = 'X';
            }
        }
    }
}
