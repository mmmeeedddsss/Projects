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
    class Queen : Piece
    {
        public Queen(Texture2D tex,Rectangle rec,string color,Vector2 curpos)
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

            temppos = this.curpos;
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
                else if (to.Equals( curpos ) ) return false;
                else addy = -1;
                
                Vector2 temppos = curpos;
                temppos.Y += addy;
                while( !temppos.Equals( to ) )
                {
                    if( table[(int)temppos.X,(int)temppos.Y] != " " )
                        return false;
                    temppos.Y += addy;
                }
                return true;
            }
            else if( to.Y == curpos.Y )
            {
                int addx;
                if (to.X > curpos.X) addx = 1;
                else addx = -1;

                Vector2 temppos = curpos;
                temppos.X += addx;
                while (!temppos.Equals(to))
                {
                    if (table[(int)temppos.X, (int)temppos.Y] != " ")
                        return false;
                    temppos.X += addx;
                }
                return true;
            }
            else if (Math.Abs(to.X - curpos.X) == Math.Abs(to.Y - curpos.Y))
            {
                int addx,addy;
                if (to.X > curpos.X) addx = 1;
                else addx = -1;
                if (to.Y > curpos.Y) addy = 1;
                else addy = -1;

                Vector2 temppos = curpos;
                temppos.Y += addy;
                temppos.X += addx;
                while (!temppos.Equals(to))
                {
                    if (temppos.X > 7 || temppos.X < 0 || temppos.Y > 7 || temppos.Y < 0)
                        return false;
                    if (table[(int)temppos.X, (int)temppos.Y] != " ")
                        return false;
                    temppos.X += addx;
                    temppos.Y += addy;
                }
                return true;
            }
            return false;
        }
    }
}
