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
    class King : Piece
    {
        public King(Texture2D tex,Rectangle rec,string color,Vector2 curpos)
        {
            this.texture = tex;
            this.rec = rec;
            this.color = color;
            this.curpos = curpos;
        }

        public bool canplay(Vector2 to,string[,] table,List<Info> tableInfo,char[,] manaceArea)
        {
            if (to.Equals( curpos ) ) return false;
            int x, y;
            x = (int)Math.Abs(to.X - curpos.X);
            y = (int)Math.Abs(to.Y - curpos.Y);
            if (Math.Abs((to.X + to.Y) - (curpos.X + curpos.Y)) == 1 || Math.Abs((to.X + to.Y) - (curpos.X + curpos.Y)) == 0 || Math.Abs((to.X + curpos.Y) - (to.Y + curpos.Y)) == 1 )
            {
                foreach (Info info in tableInfo)
                {
                    if (info.pos.X == to.X && info.pos.Y == to.Y)
                    {
                        if (this.color == info.color)
                            return false;
                    }
                }
            }
            if (manaceArea[(int)to.X, (int)to.Y] != 'X')
                return true;
            return false;
        }
        public bool ischeck( char[,] manacearea , string[,] table )
        {
            if ( (manacearea[(int)curpos.X, (int)curpos.Y] == 'X') == false ) return false;
            return true;
        }

        public bool ischeckmate( char[,] manacearea , string[,] table )
        {
            if (!(curpos.X >= 0 && curpos.X < 8 && curpos.Y >= 0 && curpos.Y < 8 && (manacearea[(int)curpos.X, (int)curpos.Y] == 'X' || table[(int)curpos.X, (int)curpos.Y] != " "))) return false;
            if (!(curpos.X + 1 >= 0 && curpos.X + 1 < 8 && curpos.Y >= 0 && curpos.Y < 8 && (manacearea[(int)curpos.X + 1, (int)curpos.Y] == 'X' || table[(int)curpos.X + 1, (int)curpos.Y] != " "))) return false;
            if (!(curpos.X >= 0 && curpos.X < 8 && curpos.Y + 1 >= 0 && curpos.Y + 1 < 8 && (manacearea[(int)curpos.X, (int)curpos.Y + 1] == 'X' || table[(int)curpos.X, (int)curpos.Y + 1] != " "))) return false;
            if (!(curpos.X - 1 >= 0 && curpos.X - 1 < 8 && curpos.Y >= 0 && curpos.Y < 8 && (manacearea[(int)curpos.X - 1, (int)curpos.Y] == 'X' || table[(int)curpos.X - 1, (int)curpos.Y] != " "))) return false;
            if (!(curpos.X >= 0 && curpos.X < 8 && curpos.Y - 1 >= 0 && curpos.Y - 1 < 8 && (manacearea[(int)curpos.X, (int)curpos.Y - 1] == 'X' || table[(int)curpos.X, (int)curpos.Y - 1] != " "))) return false;
            if (!(curpos.X + 1 >= 0 && curpos.X + 1 < 8 && curpos.Y + 1 >= 0 && curpos.Y + 1 < 8 && (manacearea[(int)curpos.X + 1, (int)curpos.Y + 1] == 'X' || table[(int)curpos.X + 1, (int)curpos.Y + 1] != " "))) return false;
            if (!(curpos.X - 1 >= 0 && curpos.X - 1 < 8 && curpos.Y - 1 >= 0 && curpos.Y - 1 < 8 && (manacearea[(int)curpos.X - 1, (int)curpos.Y - 1] == 'X' || table[(int)curpos.X - 1, (int)curpos.Y - 1] != " "))) return false;
            if (!(curpos.X + 1 >= 0 && curpos.X + 1 < 8 && curpos.Y - 1 >= 0 && curpos.Y - 1 < 8 && (manacearea[(int)curpos.X + 1, (int)curpos.Y - 1] == 'X' || table[(int)curpos.X + 1, (int)curpos.Y - 1] != " "))) return false;
            if (!(curpos.X - 1 >= 0 && curpos.X - 1 < 8 && curpos.Y + 1 >= 0 && curpos.Y + 1 < 8 && (manacearea[(int)curpos.X - 1, (int)curpos.Y + 1] == 'X' || table[(int)curpos.X - 1, (int)curpos.Y + 1] != " "))) return false;
            return true;
        }

        public void manace_area(ref char[,] manaceArea, List<Info> tableinfo, string[,] table)
        {
            if (curpos.X + 1 >= 0 && curpos.X + 1 <= 7 && curpos.Y >= 0 && curpos.Y <= 7) manaceArea[(int)curpos.X + 1, (int)curpos.Y] = 'X';
            if (curpos.X >= 0 && curpos.X <= 7 && curpos.Y + 1 >= 0 && curpos.Y + 1 <= 7) manaceArea[(int)curpos.X, (int)curpos.Y + 1] = 'X';
            if (curpos.X - 1 >= 0 && curpos.X - 1 <= 7 && curpos.Y >= 0 && curpos.Y <= 7) manaceArea[(int)curpos.X - 1, (int)curpos.Y] = 'X';
            if (curpos.X >= 0 && curpos.X <= 7 && curpos.Y - 1 >= 0 && curpos.Y - 1 <= 7)  manaceArea[(int)curpos.X, (int)curpos.Y - 1] = 'X';
            if (curpos.X + 1 >= 0 && curpos.X + 1 <= 7 && curpos.Y + 1 >= 0 && curpos.Y + 1 <= 7) manaceArea[(int)curpos.X + 1, (int)curpos.Y + 1] = 'X';
            if (curpos.X + 1 >= 0 && curpos.X + 1 <= 7 && curpos.Y - 1 >= 0 && curpos.Y - 1 <= 7) manaceArea[(int)curpos.X + 1, (int)curpos.Y - 1] = 'X';
            if (curpos.X - 1 >= 0 && curpos.X - 1 <= 7 && curpos.Y + 1 >= 0 && curpos.Y + 1 <= 7) manaceArea[(int)curpos.X - 1, (int)curpos.Y + 1] = 'X';
            if (curpos.X - 1 >= 0 && curpos.X - 1 <= 7 && curpos.Y - 1 >= 0 && curpos.Y - 1 <= 7) manaceArea[(int)curpos.X - 1, (int)curpos.Y - 1] = 'X';
        }
    }
}
