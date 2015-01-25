using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hesap_Makinesi_v3._4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        } 
        private void button1_Click(object sender, EventArgs e)
        {
            System.IO.File.WriteAllText("Simple.txt",this.textBox1.Text);
            System.Diagnostics.Process.Start("HesapMakinesi v 1.3 .exe");
            for (int i = 0; i < 100000000;i++ );
                this.textBox1.Text = System.IO.File.ReadAllText("Simple.txt");
        }
    }
}
