using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bilgisayar_SüreMatik
{
    public partial class SüreMatik : Form
    {
        bool isstoped;
        System.DateTime y;
        public SüreMatik()
        {
            InitializeComponent();
            y = System.DateTime.Now.AddHours(1);
            timer2.Start();
            isstoped = true;
        }

        private void button1_Click(object sender, EventArgs e) // durdur
        {
            isstoped = true;
            timer1.Stop();
        }

        private void button2_Click(object sender, EventArgs e)// başlat
        {
            isstoped = false;
            System.TimeSpan x = (y - System.DateTime.Now);
            timer1.Interval = (x.Hours * 60 * 60 * 1000) + (x.Minutes * 60 * 1000) + (x.Seconds * 1000);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            new System.Threading.Thread(bip_bip).Start();
            timer1.Stop();
            y = System.DateTime.Now.AddHours(1);
        }

        private static void bip_bip()
        {
            for (int i = 0; i < 100; i++) System.Console.Beep();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (isstoped == true) { y = y.AddSeconds(1);  }
            label1.Text = "" + (y - System.DateTime.Now).Hours +":"+ (y - System.DateTime.Now).Minutes +":"+ (y - System.DateTime.Now).Seconds;
        }
    }
}
