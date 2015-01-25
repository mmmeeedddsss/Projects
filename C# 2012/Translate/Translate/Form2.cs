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
    public partial class Form2 : Form
    {
        string neyden;
        string neye;
        public Form2(string neyden1,string neye1)
        {
            InitializeComponent();
            neyden = neyden1;
            neye = neye1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://" + "www.google.com/translate_t?hl=en&ie=UTF8&text=" + richTextBox1.Text + "&langpair=" + neyden + "|" + neye);
            req.Method = "GET";
            WebResponse res = req.GetResponse();
            StreamReader reader = new StreamReader(res.GetResponseStream(), System.Text.Encoding.UTF8);
            string Seach = "<span title=\"" + richTextBox1.Text + "\" onmouseover=\"this.style.backgroundColor='#ebeff9'\" onmouseout=\"this.style.backgroundColor='#fff'\">(.*?)</span>";
            Regex r = new Regex(Seach);
            string resp = reader.ReadToEnd();
            Match m = r.Match(resp);
            if (m.Groups[0].Value != null)
                MessageBox.Show(m.Groups[1].Value, "Sözlük", MessageBoxButtons.OK);
            else
            {
                MessageBox.Show("Gırdın Gırdın ! :D");
            }
            res.Close();
            reader.Close();
            this.Close();
        }
    }
}
