using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kaplumbağa_Dili
{
    public partial class Form1 : Form
    {
        Graphics fg;
        Pen Turtle;
        Pen bgLines;

        Point currpos;
        int currdir;
        Point mempos;
        int memdir;

        const int step = 50;
        const int tablewidth = step * 10;
        const int tableheight = step * 10;

        public Form1()
        {
            InitializeComponent();
            fg = this.CreateGraphics();
            fg.Clear(Color.WhiteSmoke);
            Turtle = new Pen(Color.Blue, 5);
            bgLines = new Pen(Color.Gray, 1);
            currpos = new Point(5,5);
            currdir = 0;
            drawLines();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currpos = new Point(5, 5);
            currdir = 0;
            fg.Clear(Color.WhiteSmoke);
            string query = textBox1.Text;
            doMove(query);
            drawLines();
        }

        protected void doMove( string query )
        {
            int i=-1;
            while ((++i) < query.Length)
            {
                char command = query[i];
                if (command == 'i')
                {
                    drawMove(currpos, returnTOPoint());
                    currpos = returnTOPoint();
                }
                else if (command == '<')
                    currdir = (currdir + 1) % 8;
                else if (command == '>')
                {
                    if (--currdir < 0)
                        currdir = 7;
                }
                else if (command == '[')
                {
                    memdir = currdir;
                    mempos = currpos;
                }
                else if (command == ']')
                {
                    currdir = memdir;
                    currpos = mempos;
                }
                else
                {
                    int k = int.Parse("" + command);
                    int newi = findchar(query, i + 1) + 1;
                    string loopquery = query.Substring(i + 2, findchar(query, i + 1) - i - 2); // ?
                    i = newi;
                    while ((k--) > 0)
                    {
                        doMove(loopquery);
                    }
                }
            }
        }

        protected int findchar(string query, int startIndex )
        {
            int a = 0;
            while( (++startIndex) < query.Length )
            {
                if (query[startIndex] == '(')
                    a++;
                else if (query[startIndex] == ')')
                {
                    if (a == 0)
                        return startIndex;
                    else
                        a--;
                }
            }
            return -1;
        }

        protected void drawMove(Point from, Point to)
        {
            fg.DrawLine(Turtle, new Point(step * from.X, step * from.Y), new Point(step * to.X, step * to.Y));
        }

        protected Point returnTOPoint()
        {
            Point To = new Point(currpos.X, currpos.Y);
            switch( currdir )
            {
                case 0: To.Y--; break;
                case 1: To.Y--; To.X--; break;
                case 2: To.X--; break;
                case 3: To.Y++; To.X--; break;
                case 4: To.Y++; break;
                case 5: To.Y++; To.X++; break;
                case 6: To.X++; break;
                case 7: To.Y--; To.X++; break;
            }
            return To;
        }

        protected void drawLines()
        {
            for (int i = 1; i < tableheight / step; i++)
                fg.DrawLine(bgLines, new Point(step * i, 0), new Point(step * i, tableheight));
            for (int i = 1; i < tablewidth / step; i++)
                fg.DrawLine(bgLines, new Point(0, step * i), new Point(tablewidth, step * i));
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawLines();
        }
    }
}
