using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Not_Matik
{
    public partial class Form1 : Form
    {
        int i;
        public Form1()
        {
            InitializeComponent();
            MessageBox.Show("Virgullü sayıları 'VİRGÜL' ile Ayırınız ...");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            i = 0;
            button1_Click(null,null);
            button1_Click(null, null);
            button1_Click(null, null);
        }

        private void addTextFields(int x)
        {
            TextBox temp = new TextBox();
            temp.Location = new Point(x,i+60);
            this.Controls.Add(temp);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addTextFields(20);
            addTextFields(150);
            i += 30;
            if (i >= 30)
            {
                button3.Enabled = true;
            }
            if (i > 540 )
            {
                button1.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int j=0;
            float x, y;
            float top_saat=0, top_not=0;
            while (j < i)
            {
                try
                {
                    x = float.Parse(this.GetChildAtPoint(new Point(20, j + 60)).Text);
                    y = float.Parse(this.GetChildAtPoint(new Point(150, j + 60)).Text);
                    top_saat += x;
                    top_not += x * y;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lan !");
                    return;
                }
                j += 30;
            }
            if( top_saat != 0 ) MessageBox.Show("Ortalaman : " + (top_not / top_saat));
            else MessageBox.Show("Top Saat 0 Olamaz !");
        }

        private void button3_Click(object sender, EventArgs e)
        {
                this.GetChildAtPoint(new Point(20, i + 30)).Dispose();
                this.GetChildAtPoint(new Point(150, i + 30)).Dispose();
                i -= 30;
                if (i <= 30)
                {
                    button3.Enabled = false;
                }
        }
    }
}
