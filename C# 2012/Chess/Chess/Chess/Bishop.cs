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
    class Bishop : Piece
    {
        public Bishop(Texture2D tex,Rectangle rec,string color,Vector2 curpos)
        {
            this.texture = tex;
            this.rec = rec;
            this.color = color;
            this.curpos = curpos;
        }

        public void manace_area(ref char[,] manaceArea, List<Info> tableinfo, string[,] table)
        {
            Vector2 temppos = this.curpos;
            temppos.X += 1;
            temppos.Y += 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7 && table[(int)temppos.X, (int)temppos.Y] == " ")
            {
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.X += 1;
                temppos.Y += 1;
            }
            temppos = this.curpos;
            temppos.X -= 1;
            temppos.Y += 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7 && table[(int)temppos.X, (int)temppos.Y] == " ")
            {
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.X -= 1;
                temppos.Y += 1;
            }
            temppos = this.curpos;
            temppos.X -= 1;
            temppos.Y -= 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7 && table[(int)temppos.X, (int)temppos.Y] == " ")
            {
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.X -= 1;
                temppos.Y -= 1;
            }
            temppos = this.curpos;
            temppos.X += 1;
            temppos.Y -= 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7 && table[(int)temppos.X, (int)temppos.Y] == " ")
            {
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.X += 1;
                temppos.Y -= 1;
            }

        }

        public bool canplay(Vector2 to, string[,] table, List<Info> tableInfo, char[,] manaceArea)
        {
            if (to.Equals( curpos ) ) return false;
            if (Math.Abs(to.X - curpos.X) == Math.Abs(to.Y - curpos.Y))
            {
                foreach (Info info in tableInfo)
                {
                    if (info.pos.X == to.X && info.pos.Y == to.Y && info.color == this.color)
                    {
                        return false;
                    }
                }
                int add_x,add_y;
                if (curpos.X > to.X) add_x = -1;
                else add_x = 1;
                if (curpos.Y > to.Y) add_y = -1;
                else add_y = 1;
                Vector2 tempPos = new Vector2(curpos.X + add_x,curpos.Y + add_y );
                while (!tempPos.Equals(to))
                {
                    if (table[(int)tempPos.X, (int)tempPos.Y] != " ")
                        return false;
                    tempPos.X += add_x;
                    tempPos.Y += add_y;
                }
                //add şah bok olmuyosa
                return true;
            }
            return false;
        }
    }
}
