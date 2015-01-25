using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Olcak_Mı_Lan_Acaba
{
    public partial class Form2 : Form
    {
        public Color choosedColor;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            choosedColor = colorDialog1.Color;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            choosedColor = colorDialog1.Color;
        }

    }
}
