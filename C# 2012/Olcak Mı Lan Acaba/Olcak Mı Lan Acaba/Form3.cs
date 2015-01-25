using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Olcak_Mı_Lan_Acaba
{
    public partial class Form3 : Form
    {
        public String style;
        public int Value;
        public bool wtc;

        public Form3()
        {
            InitializeComponent();
            style = "fc";
            Value = 10;
            wtc = false;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            Value = trackBar1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            style = "es";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            style = "ec";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            style = "fs";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            style = "fc";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("by Mert !                                ", "L Lawiet(Death Note)", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            wtc = true;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
        }
    }
}
