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
    class Rook : Piece
    {
        public Rook(Texture2D tex,Rectangle rec,string color,Vector2 curpos)
        {
            this.texture = tex;
            this.rec = rec;
            this.color = color;
            this.curpos = curpos;
        }

        public bool canplay(Vector2 to, string[,] table, List<Info> tableInfo, char[,] manaceArea)
        {
            if (to.Equals( curpos ) ) return false;
            foreach (Info info in tableInfo)
            {
                if (info.pos.X == to.X && info.pos.Y == to.Y && info.color == this.color)
                {
                    return false;
                }
            }
            if (to.X == curpos.X)
            {
                int addy;
                if (to.Y > curpos.Y) addy = 1;
                else addy = -1;

                Vector2 temppos = curpos;
                temppos.Y += addy;
                while (!temppos.Equals(to))
                {
                    if ((int)temppos.X < 0 || (int)temppos.X > 7 || (int)temppos.Y < 0 || (int)temppos.Y > 7) return false;
                    if (table[(int)temppos.X, (int)temppos.Y] != " ")
                        return false;
                    temppos.Y += addy;
                }
                return true;
            }
            else if (to.Y == curpos.Y)
            {
                int addx;
                if (to.X > curpos.X) addx = 1;
                else addx = -1;

                Vector2 temppos = curpos;
                temppos.X += addx;
                while (!temppos.Equals(to))
                {
                    if ((int)temppos.X < 0 || (int)temppos.X > 7 || (int)temppos.Y < 0 || (int)temppos.Y > 7) return false;
                    if (table[(int)temppos.X, (int)temppos.Y] != " ")
                        return false;
                    temppos.X += addx;
                }
                return true;
            }
            return false;
        }

        public void manace_area(ref char[,] manaceArea, List<Info> tableinfo, string[,] table)
        {
            Vector2 temppos = this.curpos;
            temppos.X += 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7 && table[(int)temppos.X, (int)temppos.Y] == " ")
            {
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.X += 1;
            }
            temppos = this.curpos;
            temppos.X -= 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7 && table[(int)temppos.X, (int)temppos.Y] == " ")
            {
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.X -= 1;
            }
            temppos = this.curpos;
            temppos.Y -= 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7 && table[(int)temppos.X, (int)temppos.Y] == " ")
            {
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.Y -= 1;
            }
            temppos = this.curpos;
            temppos.Y += 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7 && table[(int)temppos.X, (int)temppos.Y] == " ")
            {
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.Y += 1;
            }
        }
    }
}
