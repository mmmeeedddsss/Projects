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

namespace Chess2
{
    public class Info : ICloneable
    {
        public string piecename,color; // hepsi küçük türkçe isimler , renk s veya b
        public Texture2D tex;
        public Vector2 curpos; // pixel yerine kare kordinatı

        public object Clone()
        {
            return this;
        }

        public Info(Texture2D tex,string piecename,string color,Vector2 curpos)
        {
            this.tex = tex;
            this.piecename = piecename;
            this.color = color;
            this.curpos = curpos;
        }

        public bool canplay(Vector2 to,string[,] table,List<Info> tableinfo,char[,] manaceArea,Vector2 kingpos)
        {
            if (piecename == "kral") return canplayKral(to, table, tableinfo, manaceArea);
            if (piecename == "piyon") return canplayPiyon(to, table,tableinfo,manaceArea);
            if (piecename == "kale") return canplayKale(to, table, tableinfo, manaceArea);
            if (piecename == "at") return canplayAt(to, table, tableinfo, manaceArea);
            if (piecename == "fil") return canplayFil(to, table, tableinfo, manaceArea);
            if (piecename == "vezir") return canplayVezir(to, table, tableinfo, manaceArea);
            return false;
        }

        public void manaceArea(ref char[,] manaceArea, List<Info> tableinfo, string[,] table)
        {
            if (piecename == "piyon") mPiyon(ref manaceArea, tableinfo, table);
            if (piecename == "kale") mKale(ref manaceArea, tableinfo, table);
            if (piecename == "at") mAt(ref manaceArea, tableinfo, table);
            if (piecename == "fil") mFil(ref manaceArea, tableinfo, table);
            if (piecename == "vezir") mVezir(ref manaceArea, tableinfo, table);
            if (piecename == "kral") mKral(ref manaceArea, tableinfo, table);
        }

        public void draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();

            if ((curpos.X + curpos.Y) % 2 == 1) spritebatch.Draw(tex, new Rectangle(90 * ((int)curpos.X), 90 * ((int)curpos.Y), 90, 90), Color.WhiteSmoke);
            else spritebatch.Draw(tex, new Rectangle(90 * ((int)curpos.X), 90 * ((int)curpos.Y),90,90), Color.DarkGreen);

            spritebatch.End();
        }

        public bool canplayKral(Vector2 to, string[,] table, List<Info> tableinfo, char[,] manaceArea)
        {
            if (to.Equals(curpos)) return false;
            int x, y;
            x = (int)Math.Abs(to.X - curpos.X);
            y = (int)Math.Abs(to.Y - curpos.Y);
            if (Math.Abs((to.X + to.Y) - (curpos.X + curpos.Y)) == 1 || Math.Abs((to.X + to.Y) - (curpos.X + curpos.Y)) == 0 || Math.Abs((to.X + curpos.Y) - (to.Y + curpos.Y)) == 1)
            {
                foreach (Info info in tableinfo)
                {
                    if (info.curpos.X == to.X && info.curpos.Y == to.Y)
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
        public bool canplayKale(Vector2 to, string[,] table, List<Info> tableInfo, char[,] manaceArea)
        {
            if (to.Equals(curpos)) return false;
            foreach (Info info in tableInfo)
            {
                if (info.curpos.X == to.X && info.curpos.Y == to.Y && info.color == this.color)
                {
                    return false;
                }
            }
            if (to.X == curpos.X)
            {
                int addy;
                if (to.Y > curpos.Y) addy = 1;
                else addy = -1;

                Vector2 tempcurpos = curpos;
                tempcurpos.Y += addy;
                while (!tempcurpos.Equals(to))
                {
                    if ((int)tempcurpos.X < 0 || (int)tempcurpos.X > 7 || (int)tempcurpos.Y < 0 || (int)tempcurpos.Y > 7) return false;
                    if (table[(int)tempcurpos.X, (int)tempcurpos.Y] != " ")
                        return false;
                    tempcurpos.Y += addy;
                }
                return true;
            }
            else if (to.Y == curpos.Y)
            {
                int addx;
                if (to.X > curpos.X) addx = 1;
                else addx = -1;

                Vector2 tempcurpos = curpos;
                tempcurpos.X += addx;
                while (!tempcurpos.Equals(to))
                {
                    if ((int)tempcurpos.X < 0 || (int)tempcurpos.X > 7 || (int)tempcurpos.Y < 0 || (int)tempcurpos.Y > 7) return false;
                    if (table[(int)tempcurpos.X, (int)tempcurpos.Y] != " ")
                        return false;
                    tempcurpos.X += addx;
                }
                return true;
            }
            return false;
        }
        public bool canplayFil(Vector2 to, string[,] table, List<Info> tableInfo, char[,] manaceArea)
        {
            if (to.Equals(curpos)) return false;
            if (Math.Abs(to.X - curpos.X) == Math.Abs(to.Y - curpos.Y))
            {
                foreach (Info info in tableInfo)
                {
                    if (info.curpos.X == to.X && info.curpos.Y == to.Y && info.color == this.color)
                    {
                        return false;
                    }
                }
                int add_x, add_y;
                if (curpos.X > to.X) add_x = -1;
                else add_x = 1;
                if (curpos.Y > to.Y) add_y = -1;
                else add_y = 1;
                Vector2 tempcurpos = new Vector2(curpos.X + add_x, curpos.Y + add_y);
                while (!tempcurpos.Equals(to))
                {
                    if (table[(int)tempcurpos.X, (int)tempcurpos.Y] != " ")
                        return false;
                    tempcurpos.X += add_x;
                    tempcurpos.Y += add_y;
                }
                //add şah bok olmuyosa
                return true;
            }
            return false;
        }
        public bool canplayVezir(Vector2 to, string[,] table, List<Info> tableInfo, char[,] manaceArea)
        {
            if (to.Equals(curpos)) return false;
            foreach (Info info in tableInfo)
            {
                if (info.curpos.X == to.X && info.curpos.Y == to.Y && info.color == this.color)
                {
                    return false;
                }
            }
            if (to.X == curpos.X)
            {
                int addy;
                if (to.Y > curpos.Y) addy = 1;
                else if (to.Equals(curpos)) return false;
                else addy = -1;

                Vector2 tempcurpos = curpos;
                tempcurpos.Y += addy;
                while (!tempcurpos.Equals(to))
                {
                    if (table[(int)tempcurpos.X, (int)tempcurpos.Y] != " ")
                        return false;
                    tempcurpos.Y += addy;
                }
                return true;
            }
            else if (to.Y == curpos.Y)
            {
                int addx;
                if (to.X > curpos.X) addx = 1;
                else addx = -1;

                Vector2 tempcurpos = curpos;
                tempcurpos.X += addx;
                while (!tempcurpos.Equals(to))
                {
                    if (table[(int)tempcurpos.X, (int)tempcurpos.Y] != " ")
                        return false;
                    tempcurpos.X += addx;
                }
                return true;
            }
            else if (Math.Abs(to.X - curpos.X) == Math.Abs(to.Y - curpos.Y))
            {
                int addx, addy;
                if (to.X > curpos.X) addx = 1;
                else addx = -1;
                if (to.Y > curpos.Y) addy = 1;
                else addy = -1;

                Vector2 tempcurpos = curpos;
                tempcurpos.Y += addy;
                tempcurpos.X += addx;
                while (!tempcurpos.Equals(to))
                {
                    if (tempcurpos.X > 7 || tempcurpos.X < 0 || tempcurpos.Y > 7 || tempcurpos.Y < 0)
                        return false;
                    if (table[(int)tempcurpos.X, (int)tempcurpos.Y] != " ")
                        return false;
                    tempcurpos.X += addx;
                    tempcurpos.Y += addy;
                }
                return true;
            }
            return false;
        }
        public bool canplayPiyon(Vector2 to, string[,] table, List<Info> tableInfo, char[,] manaceArea)
        {
            if (to.Equals(curpos)) return false;
            if (color == "b")
            {
                if (curpos.Y == 6 && to.Y == 4 && table[(int)curpos.X, 5] == " " && table[(int)curpos.X, 4] == " ")
                    return true;
                if (curpos.Y - to.Y == 1 && table[(int)curpos.X, (int)curpos.Y - 1] == " ")
                    return true;
                if (curpos.Y - to.Y == 1 && Math.Abs(curpos.X - to.X) == 1)
                    foreach (Info info in tableInfo)
                        if (info.color != this.color && info.curpos.X == to.X && curpos.Y - info.curpos.Y == 1)
                            return true;
                return false;
            }
            else
            {
                if (curpos.Y == 1 && to.Y - curpos.Y == 2 && table[(int)curpos.X, (int)curpos.Y + 1] == " " && table[(int)curpos.X, (int)curpos.Y + 2] == " ")
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
        public bool canplayAt(Vector2 to, string[,] table, List<Info> tableInfo, char[,] manaceArea)
        {
            int x, y;
            x = (int)Math.Abs(to.X - curpos.X);
            y = (int)Math.Abs(to.Y - curpos.Y);
            if ((x + y) == 3 && x != 0 && y != 0)
            {
                foreach (Info info in tableInfo)
                {
                    if (info.curpos.X == to.X && info.curpos.Y == to.Y)
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

        public void mAt(ref char[,] manaceArea, List<Info> tableinfo, string[,] table)
        {
            if (curpos.X + 2 >= 0 && curpos.X + 2 <= 7 && curpos.Y + 1 >= 0 && curpos.Y + 1 <= 7) manaceArea[(int)curpos.X + 2, (int)curpos.Y + 1] = 'X';
            if (curpos.X + 2 >= 0 && curpos.X + 2 <= 7 && curpos.Y - 1 >= 0 && curpos.Y - 1 <= 7) manaceArea[(int)curpos.X + 2, (int)curpos.Y - 1] = 'X';
            if (curpos.X - 2 >= 0 && curpos.X - 2 <= 7 && curpos.Y + 1 >= 0 && curpos.Y + 1 <= 7) manaceArea[(int)curpos.X - 2, (int)curpos.Y + 1] = 'X';
            if (curpos.X - 2 >= 0 && curpos.X - 2 <= 7 && curpos.Y - 1 >= 0 && curpos.Y - 1 <= 7) manaceArea[(int)curpos.X - 2, (int)curpos.Y - 1] = 'X';
            if (curpos.X - 1 >= 0 && curpos.X - 1 <= 7 && curpos.Y + 2 >= 0 && curpos.Y + 2 <= 7) manaceArea[(int)curpos.X - 1, (int)curpos.Y + 2] = 'X';
            if (curpos.X + 1 >= 0 && curpos.X + 1 <= 7 && curpos.Y + 2 >= 0 && curpos.Y + 2 <= 7) manaceArea[(int)curpos.X + 1, (int)curpos.Y + 2] = 'X';
            if (curpos.X + 1 >= 0 && curpos.X + 1 <= 7 && curpos.Y - 2 >= 0 && curpos.Y - 2 <= 7) manaceArea[(int)curpos.X + 1, (int)curpos.Y - 2] = 'X';
            if (curpos.X - 1 >= 0 && curpos.X - 1 <= 7 && curpos.Y - 2 >= 0 && curpos.Y - 2 <= 7) manaceArea[(int)curpos.X - 1, (int)curpos.Y - 2] = 'X';

        }
        public void mPiyon(ref char[,] manaceArea, List<Info> tableinfo, string[,] table)
        {
            if (this.color == "b")
            {
                if (curpos.X + 1 >= 0 && curpos.X + 1 <= 7 && curpos.Y - 1 >= 0 && curpos.Y - 1 <= 7) manaceArea[(int)curpos.X + 1, (int)curpos.Y - 1] = 'X';
                if (curpos.X - 1 >= 0 && curpos.X - 1 <= 7 && curpos.Y - 1 >= 0 && curpos.Y - 1 <= 7) manaceArea[(int)curpos.X - 1, (int)curpos.Y - 1] = 'X';
            }
            else
            {
                if (curpos.X + 1 >= 0 && curpos.X + 1 <= 7 && curpos.Y + 1 >= 0 && curpos.Y + 1 <= 7) manaceArea[(int)curpos.X + 1, (int)curpos.Y + 1] = 'X';
                if (curpos.X - 1 >= 0 && curpos.X - 1 <= 7 && curpos.Y + 1 >= 0 && curpos.Y + 1 <= 7) manaceArea[(int)curpos.X - 1, (int)curpos.Y + 1] = 'X';
            }
        }
        public void mVezir(ref char[,] manaceArea, List<Info> tableinfo, string[,] table)
        {
            Vector2 temppos = this.curpos;
            temppos.X += 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7)
            {
                if (table[(int)temppos.X, (int)temppos.Y] != " ")
                {
                        manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                        break;
                }
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.X += 1;
            }
            temppos = this.curpos;
            temppos.X -= 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7)
            {
                if (table[(int)temppos.X, (int)temppos.Y] != " ")
                {
                        manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                        break;
                }
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.X -= 1;
            }
            temppos = this.curpos;
            temppos.Y -= 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7)
            {
                if (table[(int)temppos.X, (int)temppos.Y] != " ")
                {
                        manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                        break;
                }
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.Y -= 1;
            }
            temppos = this.curpos;
            temppos.Y += 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7)
            {
                if (table[(int)temppos.X, (int)temppos.Y] != " ")
                {
                        manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                        break;
                }
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.Y += 1;
            }
            temppos = this.curpos;
            temppos.X += 1;
            temppos.Y += 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7)
            {
                if (table[(int)temppos.X, (int)temppos.Y] != " ")
                {
                        manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                        break;
                }
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.X += 1;
                temppos.Y += 1;
            }
            temppos = this.curpos;
            temppos.X -= 1;
            temppos.Y += 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7)
            {
                if (table[(int)temppos.X, (int)temppos.Y] != " ")
                {
                        manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                        break;
                }
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.X -= 1;
                temppos.Y += 1;
            }
            temppos = this.curpos;
            temppos.X -= 1;
            temppos.Y -= 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7)
            {
                if (table[(int)temppos.X, (int)temppos.Y] != " ")
                {
                        manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                        break;
                }
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.X -= 1;
                temppos.Y -= 1;
            }
            temppos = this.curpos;
            temppos.X += 1;
            temppos.Y -= 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7)
            {
                if (table[(int)temppos.X, (int)temppos.Y] != " ")
                {
                        manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                        break;
                }
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.X += 1;
                temppos.Y -= 1;
            }

        }
        public void mFil(ref char[,] manaceArea, List<Info> tableinfo, string[,] table)
        {
            Vector2 temppos = this.curpos;
            temppos.X += 1;
            temppos.Y += 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7 )
            {
                if (table[(int)temppos.X, (int)temppos.Y] != " ")
                {
                        manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                        break;
                }
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.X += 1;
                temppos.Y += 1;
            }
            temppos = this.curpos;
            temppos.X -= 1;
            temppos.Y += 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7 )
            {
                if (table[(int)temppos.X, (int)temppos.Y] != " ")
                {
                        manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                        break;
                }
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.X -= 1;
                temppos.Y += 1;
            }
            temppos = this.curpos;
            temppos.X -= 1;
            temppos.Y -= 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7)
            {
                if (table[(int)temppos.X, (int)temppos.Y] != " ")
                {
                        manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                        break;
                }
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.X -= 1;
                temppos.Y -= 1;
            }
            temppos = this.curpos;
            temppos.X += 1;
            temppos.Y -= 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7 )
            {
                if (table[(int)temppos.X, (int)temppos.Y] != " ")
                {
                        manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                        break;
                }
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.X += 1;
                temppos.Y -= 1;
            }

        }
        public void mKale(ref char[,] manaceArea, List<Info> tableinfo, string[,] table)
        {
            Vector2 temppos = this.curpos;
            temppos.X += 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7)
            {
                if (table[(int)temppos.X, (int)temppos.Y] != " ")
                {
                        manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                        break;
                }
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.X += 1;
            }
            temppos = this.curpos;
            temppos.X -= 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7)
            {
                if (table[(int)temppos.X, (int)temppos.Y] != " ")
                {
                        manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                        break;
                }
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.X -= 1;
            }
            temppos = this.curpos;
            temppos.Y -= 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7)
            {
                if (table[(int)temppos.X, (int)temppos.Y] != " ")
                {
                        manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                        break;
                }
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.Y -= 1;
            }
            temppos = this.curpos;
            temppos.Y += 1;
            while (temppos.X >= 0 && temppos.X <= 7 && temppos.Y >= 0 && temppos.Y <= 7)
            {
                if (table[(int)temppos.X, (int)temppos.Y] != " ")
                {
                        manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                        break;
                }
                manaceArea[(int)temppos.X, (int)temppos.Y] = 'X';
                temppos.Y += 1;
            }
        }
        public void mKral(ref char[,] manaceArea, List<Info> tableinfo, string[,] table)
        {
            if (curpos.X + 1 >= 0 && curpos.X + 1 <= 7 && curpos.Y >= 0 && curpos.Y <= 7) manaceArea[(int)curpos.X + 1, (int)curpos.Y] = 'X';
            if (curpos.X >= 0 && curpos.X <= 7 && curpos.Y + 1 >= 0 && curpos.Y + 1 <= 7) manaceArea[(int)curpos.X, (int)curpos.Y + 1] = 'X';
            if (curpos.X - 1 >= 0 && curpos.X - 1 <= 7 && curpos.Y >= 0 && curpos.Y <= 7) manaceArea[(int)curpos.X - 1, (int)curpos.Y] = 'X';
            if (curpos.X >= 0 && curpos.X <= 7 && curpos.Y - 1 >= 0 && curpos.Y - 1 <= 7) manaceArea[(int)curpos.X, (int)curpos.Y - 1] = 'X';
            if (curpos.X + 1 >= 0 && curpos.X + 1 <= 7 && curpos.Y + 1 >= 0 && curpos.Y + 1 <= 7) manaceArea[(int)curpos.X + 1, (int)curpos.Y + 1] = 'X';
            if (curpos.X + 1 >= 0 && curpos.X + 1 <= 7 && curpos.Y - 1 >= 0 && curpos.Y - 1 <= 7) manaceArea[(int)curpos.X + 1, (int)curpos.Y - 1] = 'X';
            if (curpos.X - 1 >= 0 && curpos.X - 1 <= 7 && curpos.Y + 1 >= 0 && curpos.Y + 1 <= 7) manaceArea[(int)curpos.X - 1, (int)curpos.Y + 1] = 'X';
            if (curpos.X - 1 >= 0 && curpos.X - 1 <= 7 && curpos.Y - 1 >= 0 && curpos.Y - 1 <= 7) manaceArea[(int)curpos.X - 1, (int)curpos.Y - 1] = 'X';
        }

        public bool ischeckmate(char[,] manacearea, string[,] table)
        {
            if (curpos.X >= 0 && curpos.X < 8 && curpos.Y >= 0 && curpos.Y < 8) if (manacearea[(int)curpos.X, (int)curpos.Y] != 'X' && table[(int)curpos.X, (int)curpos.Y][0] != this.color[0]) return false;
            if ((curpos.X + 1 >= 0 && curpos.X + 1 < 8 && curpos.Y >= 0 && curpos.Y < 8)) if (manacearea[(int)curpos.X + 1, (int)curpos.Y] != 'X' && table[(int)curpos.X + 1, (int)curpos.Y][0] != this.color[0]) return false;
            if ((curpos.X >= 0 && curpos.X < 8 && curpos.Y + 1 >= 0 && curpos.Y + 1 < 8)) if (manacearea[(int)curpos.X, (int)curpos.Y + 1] != 'X' && table[(int)curpos.X, (int)curpos.Y + 1][0] != this.color[0]) return false;
            if ((curpos.X - 1 >= 0 && curpos.X - 1 < 8 && curpos.Y >= 0 && curpos.Y < 8)) if (manacearea[(int)curpos.X - 1, (int)curpos.Y] != 'X' && table[(int)curpos.X - 1, (int)curpos.Y][0] != this.color[0]) return false;
            if ((curpos.X >= 0 && curpos.X < 8 && curpos.Y - 1 >= 0 && curpos.Y - 1 < 8)) if (manacearea[(int)curpos.X, (int)curpos.Y - 1] != 'X' && table[(int)curpos.X, (int)curpos.Y - 1][0] != this.color[0]) return false;
            if ((curpos.X + 1 >= 0 && curpos.X + 1 < 8 && curpos.Y + 1 >= 0 && curpos.Y + 1 < 8)) if (manacearea[(int)curpos.X + 1, (int)curpos.Y + 1] != 'X' && table[(int)curpos.X + 1, (int)curpos.Y + 1][0] != this.color[0]) return false;
            if ((curpos.X - 1 >= 0 && curpos.X - 1 < 8 && curpos.Y - 1 >= 0 && curpos.Y - 1 < 8)) if (manacearea[(int)curpos.X - 1, (int)curpos.Y - 1] != 'X' && table[(int)curpos.X - 1, (int)curpos.Y - 1][0] != this.color[0]) return false;
            if ((curpos.X + 1 >= 0 && curpos.X + 1 < 8 && curpos.Y - 1 >= 0 && curpos.Y - 1 < 8)) if (manacearea[(int)curpos.X + 1, (int)curpos.Y - 1] != 'X' && table[(int)curpos.X + 1, (int)curpos.Y - 1][0] != this.color[0]) return false;
            if ((curpos.X - 1 >= 0 && curpos.X - 1 < 8 && curpos.Y + 1 >= 0 && curpos.Y + 1 < 8)) if (manacearea[(int)curpos.X - 1, (int)curpos.Y + 1] != 'X' && table[(int)curpos.X - 1, (int)curpos.Y + 1][0] != this.color[0]) return false;
            if (!ischeck(manacearea, table)) return false;
            return true;
        }
        public bool ischeck(char[,] manacearea, string[,] table)
        {
            if (manacearea[(int)curpos.X, (int)curpos.Y] == 'X') return true;
            return false;
        }
    }
}