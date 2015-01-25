using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace Translate
{
    public partial class Form1 : Form
    {
        string neye;
        string neyden;
        public Form1()
        {
            InitializeComponent();
            MessageBox.Show("Lutfen Yazarken ? ! gibi işaretler koymayınız ve türkçe karakter kullanmayınız ...", "Yapamadım Napiim :S", MessageBoxButtons.OKCancel);
            notifyIcon1.ShowBalloonTip(100000, "Beni Gör !", "Bana Tıklayarak Yeteneklerimi Görebilirsin ..", ToolTipIcon.Info);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://"+"www.google.com/translate_t?hl=en&ie=UTF8&text="+ richTextBox1.Text +"&langpair="+neyden+"|"+neye);
            req.Method = "GET";
            WebResponse res = req.GetResponse();
            StreamReader reader = new StreamReader(res.GetResponseStream(), System.Text.Encoding.UTF8);
            string Seach = "<span title=\"" + richTextBox1.Text + "\" onmouseover=\"this.style.backgroundColor='#ebeff9'\" onmouseout=\"this.style.backgroundColor='#fff'\">(.*?)</span>";
            Regex r = new Regex(Seach);
            string resp = reader.ReadToEnd();
            Match m = r.Match(resp);
            if (m.Groups[0].Value != null)
                richTextBox2.Text = m.Groups[0].Value;
            for (int i = 0; i < m.Groups.Count; i++)
            {
                MessageBox.Show(m.Groups[i].Value,""+i);
            }
                //richTextBox2.Text = m.Groups[0].Value;
                richTextBox2.Refresh();
            //richTextBox2.Text = reader.ReadToEnd();
            res.Close();
            reader.Close();
        }

        private void gösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        private void gizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Environment.Exit(1);
        }

        private void sözlükToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 yeniform = new Form2(neyden,neye);
            yeniform.Show();
        }

        private void çeviriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            notifyIcon1.ShowBalloonTip(100000, "Beni Gör !", "Ben Hala Burdayım :D", ToolTipIcon.Info);
            this.Visible = false;
        }

        private void almancaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            neyden = "de";
            label1.Text = "Almanca";
            label1.Refresh();
        }

        private void ingilizceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            neyden = "en";
            label1.Text = "Ingilizce";
            label1.Refresh();
        }

        private void türkçeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            neyden = "tr";
            label1.Text = "Turkçe";
            label1.Refresh();
        }

        private void almancaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            neye = "de";
            label3.Text = "Almanca";
            label1.Refresh();
        }

        private void ingilizceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            neye = "en";
            label3.Text = "Ingilizce";
            label1.Refresh();
        }

        private void türkçeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            neye = "tr";
            label3.Text = "Turkçe";
            label1.Refresh();
        }

        private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hazırlayan Mert Tunç...", "Ohuyom Ben Yha", MessageBoxButtons.OKCancel);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            string temp = neyden;
            neyden = neye;
            neye = temp;
            temp = label1.Text ;
            label1.Text = label3.Text;
            label3.Text = temp;
        }
    }
}
