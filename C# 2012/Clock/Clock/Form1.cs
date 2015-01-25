using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Clock
{
    public partial class Form1 : Form
    {
		// İmleç Uzunlukları
        const int secLineLenght = 125; 
        const int minLineLenght = 110;
        const int hourLineLenght = 90;
        int sec = 0;
        int min = 0;
        int hour = 0;
		// Ekrana grafik çizdirmek için kulanılan class
        Graphics fg;
		// Çizilen Çizginin Niteliklerini Tutan Class
        Pen pensec,penmin,penhour,penEllipse;
		//Orijin Noktaları
        int ox, oy;
		// Her Saniye Saniyeyi Bir Arttırcaz ve Ekrana Saati Yine Çizdircez. 
		// Eyer Thread Kullanmazsak Kod Sleepe Geldiğinde Program Sleep işlemi Yaptığından
		// Pencere Kitlenir ve Program Cevap Vermiyo Dİye Hata Verir
        Thread ac;

        public Form1()
        {
            InitializeComponent();

			//Init
            fg = this.CreateGraphics();
            pensec = new Pen(Color.LightGray,2.0F);
            penmin = new Pen(Color.Gray,3);
            penhour = new Pen(Color.Gray,3);
            penEllipse = new Pen(Color.Gray, 3);
			// Threadın Hangi Fonksiyonu Çağıracağını Belirtiyoruz
            ac = new Thread(addSecs);
			
            ox = 180;
            oy = 180;
			// Ekranı Hafif Kirli Beyazla Dolduruyo
            fg.Clear(Color.WhiteSmoke);
        }

        public void DrawClock()
        {
            try
            {
				// Önce Ekranı Siliyoruz Sonra Herşeyi Çizdircez
                fg.Clear(Color.WhiteSmoke);
                // Draw Sec
                    int currsec = sec;// temp gibi bişey
                    double x, y; // ox, oy den x, y ye çizgi çizdirp durcaz.
                    currsec -= 15;
                    currsec *= 6;
                    x = secLineLenght * Math.Cos(convertRadian(currsec)); // buralarda x, y noktalarını buluyoruz cos ve sin ile.
                    y = secLineLenght * Math.Cos(convertRadian(90 - currsec));
                    fg.DrawLine(pensec, ox, oy, ox + (int)x, oy + (int)y); // ox, oy den x, y ye çizgi
                // Eo Sec
                // Draw Min
                    int currmin = min;
                    currmin -= 15;
                    currmin *= 6;
                    x = minLineLenght * Math.Cos(convertRadian(currmin));
                    y = minLineLenght * Math.Cos(convertRadian(90 - currmin));
                    fg.DrawLine(penmin, ox, oy, ox + (int)x, oy + (int)y);
                // Eo Sec
                // Draw Hour
                    double currhour = hour + (min / 60);
                    currhour -= 3;
                    currhour *= 30;
                    x = hourLineLenght * Math.Cos(convertRadian(currhour));
                    y = hourLineLenght * Math.Cos(convertRadian(90 - currhour));
                    fg.DrawLine(penhour, ox, oy, ox + (int)x, oy + (int)y);
                 // Eo Sec
                 // Draw Ellipses
                    fg.DrawEllipse(penEllipse, ox - 2, oy - 2, 4, 4);
                    fg.DrawEllipse(penEllipse,ox - secLineLenght-5,oy - secLineLenght-6,2*secLineLenght+9 , 2*secLineLenght+9);
                 //Eo Ellipses
                    fg.DrawString("3",new Font(DefaultFont, FontStyle.Bold),penEllipse.Brush,new PointF(ox + secLineLenght + 12, oy - 9)); // 3 6 9 12 yi çizdiriyoruz
                    fg.DrawString("6", new Font(DefaultFont, FontStyle.Bold), penEllipse.Brush, new PointF(ox - 4 , oy + secLineLenght + 13));
                    fg.DrawString("9", new Font(DefaultFont, FontStyle.Bold), penEllipse.Brush, new PointF(ox - secLineLenght - 22, oy - 9));
                    fg.DrawString("12", new Font(DefaultFont, FontStyle.Bold), penEllipse.Brush, new PointF(ox - 7 , oy - secLineLenght - 25));
                    fg.DrawString(getTime(), new Font(DefaultFont, FontStyle.Bold), penEllipse.Brush, new PointF(300,320)); // saatin elektronik hali
                    fg.Flush();
                    Application.ExitThread(); // neden var bilmiyorum sanırım gerek yok buna :D
            }
            catch (Exception ex)
            {
            }
        }
		
        private string getTime() // string olarak saati döndürüyo
        {
            string time = "";

            if (hour < 10)
                time += '0'; 
            time += hour;
            time += ":";
            if (min < 10)
                time += '0';
            time += min;
            time += ":";
            if (sec < 10)
                time += '0';
            time += sec;

            return time;
        }

        private double convertRadian(double x) // dereceyi radyana ceviriyo
        {
            return (Math.PI / 180) * x;
        }

        private void addSecs() // saniye ekleyip saati değiştiriyo
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(1000);

                    if ( ++sec == 60)
                    {
                        sec = 0;
                        if ( ++min == 60)
                        {
                            min = 0;
                            if ( ++hour == 12)
                                hour = 0;
                        }
                    }

                    new Thread(DrawClock).Start();
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) // form kapanınca thread ı kapıyo
        {
            ac.Abort();
        }

        private void Form1_Load(object sender, EventArgs e) // Init kodu gibi form oluşturulduğunda çağırılıyo
        {
            sec = DateTime.Now.Second;
            min = DateTime.Now.Minute;
            hour = DateTime.Now.Hour;
            ac.Start();
        }
    }
}
