using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Client___T
{
    public partial class Form1 : Form
    {
        TcpClient tcpclient;
        NetworkStream ns;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tcpclient = new TcpClient("127.0.0.1", 1234);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NetworkStream ns = tcpclient.GetStream();
            StreamWriter w = new StreamWriter(ns);
            StreamReader r = new StreamReader(ns);
            w.WriteLine(textBox1.Text);
            w.Flush();
            label1.Text = r.ReadLine();
        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            tcpclient.Close();
        }
    }
}
