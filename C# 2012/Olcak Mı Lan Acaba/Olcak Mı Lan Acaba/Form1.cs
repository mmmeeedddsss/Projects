using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Olcak_Mı_Lan_Acaba
{
    public partial class Form1 : Form
    {
        Graphics x;
        Rectangle rect;
        SolidBrush brush;
        Pen pen;
        Form2 form2;
        Form3 form3;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            x = this.CreateGraphics();
            rect = new Rectangle();
            rect.Width = 4;
            rect.Height = 4;
            brush = new SolidBrush(Color.Black);
            this.BringToFront();
            form2 = new Form2();
            form2.Show();
            form3 = new Form3();
            form3.Show();
            pen = new Pen(brush);
            timer1.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            x.Dispose();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

            rect.X = e.X;
            rect.Y = e.Y;
            brush.Color = form2.choosedColor;
            pen.Color = form2.choosedColor;
            rect.Height = form3.Value;
            rect.Width = form3.Value;

            switch(e.Button)
            {
                case MouseButtons.Left:

                    if (form3.style.Equals("es"))
                    {
                        x.DrawRectangle(pen, rect);
                    }
                    else if (form3.style.Equals("ec"))
                    {
                        x.DrawEllipse(pen, rect);
                    }
                    else if (form3.style.Equals("fc"))
                    {
                        x.FillEllipse(brush, rect);
                    }
                    else if (form3.style.Equals("fs"))
                    {
                        x.FillRectangle(brush, rect);
                    }


                    break;

                case MouseButtons.Right:
                    rect.Height += 10;
                    rect.Width += 10;
                    Color temp = brush.Color;
                    brush.Color = Color.White;
                    x.FillEllipse(brush, rect);
                    brush.Color = temp;
                    rect.Height -= 10;
                    rect.Width -= 10;
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (form3.wtc == true)
            {
                form3.wtc = false;
                x.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, 900, 700));
            }
        }
    }
}
