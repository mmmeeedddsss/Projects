using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace Ogame12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private HttpWebRequest GetNewRequest(string targetUrl,CookieContainer cookies)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(targetUrl);
            request.CookieContainer = cookies;
            request.AllowAutoRedirect = false;
            return request;
        }

        string Connecturi;
        CookieContainer cont;

        private void Controll()
        {
            try
            {
                state.Text = "Kontrol Ediliyor ..";
                string tempuri;
                string page = "";
                if (cont.Count == 0)
                    tempuri = Connecturi;
                else
                    tempuri = "http://" + uni.Text + "/game/index.php?page=overview";
                HttpWebResponse res;
                do
                {
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(tempuri));
                    req.CookieContainer = cont;
                    req.AllowAutoRedirect = false;
                    /*
                    req.ContentType = "application/x-www-form-urlencoded";
                    req.Method = "POST";
                    string postParams = "kid=&uni=uni113.tr.ogame.org&login=mmmeeedddsss&pass=pirasasuyu31";
                    byte[] buffer = System.Text.ASCIIEncoding.UTF8.GetBytes(postParams);
                
                    req.ContentLength = buffer.Length;
                    MessageBox.Show(req.Address.AbsoluteUri);
                    using (Stream writer = req.GetRequestStream())
                    {
                        writer.Write(buffer, 0, buffer.Length);
                    }*/
                    //MessageBox.Show(uri);
                    res = req.GetResponse() as HttpWebResponse;

                    CookieCollection coll = res.Cookies;
                    cont.Add(coll);

                    StreamReader sr = new StreamReader(res.GetResponseStream());
                    page = sr.ReadToEnd();

                    if (res.StatusCode == HttpStatusCode.Found)
                        tempuri = res.Headers["Location"];
                    if (tempuri == "")
                        break;
                } while (res.StatusCode == HttpStatusCode.Found);
                if (page == "<script>document.location.href='http://" + "tr.ogame.org';</script>Veri Bankasi Sorunu 1 0 (Lütfen yeniden giris yapin)<br>")
                {
                    cont = new CookieContainer();
                    Controll();
                    return;
                }
                // Saldırı
                Regex saldiri = new Regex("<div id=\"attack_alert\" style=\"visibility:hidden;\">");
                if (!saldiri.IsMatch(page))
                {
                    atck.Text = "*** SALDIRI VAR !! ***";
                    if( saldiriCheckBox.Checked )
                        Alert();
                }
                else
                    atck.Text = "Saldırı Yok";
                //Mesaj
                Regex mesaj = new Regex("<a href=\"http://" + "uni113.tr.ogame.org/game/index.php?page=messages\" id=\"message_alert_box_default\" class=\"tipsStandard emptyMessage\" title=\"|0 yeni mesaj\">");
                if (!mesaj.IsMatch(page))
                {
                    msg.Text = "*** Yeni Mesajınız Var ***";
                    if( mesajCheckBox.Checked )
                        Alert();
                }
                else
                    msg.Text = "Yeni Mesaj Yok";
                //Metal
                Regex ress = new Regex("Mevcut: &lt;span class=''&gt;(.*?)&lt;/span&gt;");
                this.metal.Text = "" + ress.Matches(page)[0].Groups[1].Value;
                //Krist
                Regex kris = new Regex("Mevcut: &lt;span class=''&gt;(.*?)&lt;/span&gt;");
                this.kristal.Text = "" + ress.Matches(page)[1].Groups[1].Value;
                //Deut
                Regex deu = new Regex("Mevcut: &lt;span class=''&gt;(.*?)&lt;/span&gt;");
                this.deut.Text = "" + ress.Matches(page)[2].Groups[1].Value;
                //Deut
                Regex energj = new Regex("Mevcut: &lt;span class=''&gt;(.*?)&lt;/span&gt;");
                this.energj.Text = "" + ress.Matches(page)[3].Groups[1].Value;
                state.Text = "Bağlandı";
            }
            catch (Exception ex)
            {
            }
        }

        private void generateLink_Click(object sender, EventArgs e)
        {
            Connecturi = "http://" + uni.Text + "/game/reg/login2.php?login=" + name.Text + "&pass=" + pass.Text + "&kid=&v=2";
            loginLink.Text = Connecturi;
            File.WriteAllText("id.txt", name.Text);
        }

        private void Alert()
        {
            new Thread(playSound).Start();
        }

        private void playSound()
        {
            for (int i = 0; i < 10; i++)
            {
                ((System.Media.SystemSound)System.Media.SystemSounds.Beep).Play();
                Thread.Sleep(1000);
            }
        }

        private void ControllerThread()
        {
            while( iscontrolling )
            {
                Controll();
                Thread.Sleep(12000 + (new Random().Next(20))*500);
                try
                {
                    if (hedefKondroll == true)
                    {
                        if (float.Parse(metalH.Text) > 0 && float.Parse(metal.Text) > float.Parse(metalH.Text))
                            Alert();
                        if (float.Parse(KrisH.Text) > 0 && float.Parse(kristal.Text) > float.Parse(KrisH.Text))
                            Alert();
                        if (float.Parse(DeutH.Text) > 0 && float.Parse(deut.Text) > float.Parse(DeutH.Text))
                            Alert();
                    }
                    err.Text = "";
                }
                catch (Exception ex)
                {
                    err.Text = "Hedef Maddelerde Hata !";
                }
            }
            state.Text = "Bağlantı Kesildi";
        }

        bool iscontrolling = false;

        private void conn_Click(object sender, EventArgs e)
        {
            if (iscontrolling == false)
            {
                if (loginLink.Text == "")
                {
                    MessageBox.Show("Önce Linki Oluştur!");
                    return;
                }
                new Thread(ControllerThread).Start();
                iscontrolling = true;
                state.Text = "Bağlı";
            }

        }

        private void stop_Click(object sender, EventArgs e)
        {
            iscontrolling = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("id.txt"))
                name.Text = File.ReadAllText("id.txt");
            cont = new CookieContainer();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.exe", loginLink.Text);
            iscontrolling = false;
        }

        bool hedefKondroll = false;
        private void button2_Click(object sender, EventArgs e)
        {
            hedefKondroll = hedefKondroll == true ? false : true;
            Toggle.Text = "" + (hedefKondroll == true ? "Açık" : "Kapalı");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            iscontrolling = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string tempuri;
            string page = "";
            tempuri = "http://" + "uni113.tr.ogame.org/game/index.php?page=eventList";
            HttpWebResponse res;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(tempuri));
            req.CookieContainer = cont;
            req.AllowAutoRedirect = true;

            res = req.GetResponse() as HttpWebResponse;

            //CookieCollection coll = res.Cookies;
            //cont.Add(coll);

            StreamReader sr = new StreamReader(res.GetResponseStream());
            page = sr.ReadToEnd();


            Regex r = new Regex("<td class=\"missionFleet\"><img src=\"(.*?)\" class=\"tipsTitle\" title=\"(.*?)\"/></td><td class=\"originFleet\">(.*?)</td><td class=\"coordsOrigin\"><a href=\"(.*?)\" target=\"_top\">(.*?)</a></td><td class=\"detailsFleet\"> <span>(.*?)</span> </td><td class=\"icon_movement\"><span class=\"tipsTitleArrowClose\" href=\"(.*?)\" rel=\"(.*?)\" title=\"Filo detayları\">&nbsp;</span></td><td class=\"destFleet\">(.*?)</td><td class=\"destCoords\"><a href=\"(.*?)\" target=\"_top\">(.*?)</a></td><td class=\"sendProbe\"></td><td class=\"sendMail\"><a class=\"tipsStandard\" href=\"javascript:void(0);\" title=\"|(.*?) isimli oyuncuya mesaj yaz\" onClick=\"self.parent.tb_open_new('(.*?)');\"><img src=\"(.*?)\" width=\"16\" height=\"16\"/></a></td>");

            MatchCollection ms = r.Matches(page);
            foreach (Match m in ms)
            {
                foreach (string s in m.Groups)
                {
                    MessageBox.Show("" + s);
                }
            }


            // saldırı -> http://gf1.geo.gfsrv.net/cdn9a/cd360bccfc35b10966323c56ca8aac.gif
            // nakliye -> http://stackoverflow.com/questions/11816945/regex-with-a-long-search-pattern-which-has-tabs-and-newline-characters

            MessageBox.Show(r.Match(page).Groups[0].Value);
        }
    }
}
