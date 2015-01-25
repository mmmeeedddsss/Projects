using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Tavla
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 pulwh;
        Vector2 tahtawh;
        Vector2 dzar;
        Zar zar;
        Texture2D Textahta;
        Texture2D Texkpul;
        Texture2D Texbpul;
        Texture2D Textcek;
        SpriteFont font1;
        char tur;
        int selected;
        int kkirik;
        int bkirik;
        bool isselected;
        bool isclicked;
        Node[] tahta;

        class Node
        {
            public int pulSayisi;
            public char renk;

            public Node()
            {
                pulSayisi = 0;
                renk = '-';
            }

            public Node(int x,char y)
            {
                pulSayisi = x;
                renk = y;
            }
        }
        
        class Zar
        {
            List<int> zarlar;

            public Zar()
            {
                zarlar = new List<int>();
            }

            public Vector2 at()
            {
                Random r = new Random();
                zarlar.Clear();
                zarlar.Add(r.Next(1, 7));
                zarlar.Add(r.Next(1, 7));
                if (zarlar[0] == zarlar[1])
                    zarlar.AddRange(zarlar);
                return new Vector2(zarlar[0], zarlar[1]);
            }

            private Zar( List<int> x )
            {
                zarlar = x;
            }

            public void oyna(int x)
            {
                zarlar.Remove(x);
            }

            public string returnValues()
            {
                string s = "";
                foreach (int i in zarlar)
                    s += i + " - ";
                return s;
            }

            public void maxoyna()
            {
                int max=0;
                for (int i = 0; i < zarlar.Count; i++)
                    if (zarlar[i] > max)
                        max = zarlar[i];
                zarlar.Remove(max);
            }

            public bool Kontrol(int x)
            {
                return (zarlar.Contains(x) ? true : false);
            }

            public bool bittiMi()
            {
                return (zarlar.Count == 0 ? true : false);
            }

            public Zar returnSelf()
            {
                List<int> czarlar = new List<int>();
                foreach( int x in zarlar )
                    czarlar.Add(x);
                return new Zar(czarlar);
            }
        }


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            // Resimlerin Yüklenmesi
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texbpul = this.Content.Load<Texture2D>("Img/beyaz");
            Texkpul = this.Content.Load<Texture2D>("Img/kirmizi");
            Textahta = this.Content.Load<Texture2D>("Img/board");
            Textcek = this.Content.Load<Texture2D>("Img/cm");

            //Fontlarýn Yüklenmesi
            font1 = this.Content.Load<SpriteFont>("font1");

            // Tahtanýn Dizilmesi
            tahta = new Node[24];
            for (int i = 0; i < 24; i++)
                tahta[i] = new Node(0,'-');
            
            tahta[0] = new Node(2, 'k');
            tahta[5] = new Node(5, 'b');
            tahta[7] = new Node(3, 'b');
            tahta[11] = new Node(5, 'k');
            tahta[12] = new Node(5, 'b');
            tahta[16] = new Node(3, 'k');
            tahta[18] = new Node(5, 'k');
            tahta[23] = new Node(2, 'b');

            // diðer initler
            tur = 'b';
            selected = -1;
            kkirik = 0;
            bkirik = 0;
            isselected = false;
            isclicked = false;
            dzar = new Vector2();
            zar = new Zar();
            tahtawh = new Vector2(1024, 768);
            pulwh = new Vector2(75, 75);

            turAtlat();

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (IsActive)
            {
                if (tur == 'k') // oyuncu
                {
                    MouseState ms = Mouse.GetState();

                    if (isclicked == true && ms.LeftButton == ButtonState.Released)
                        isclicked = false;
                    else if (isselected == false && isclicked == false && ms.LeftButton == ButtonState.Pressed) // seçmemiþse
                    {
                        isclicked = true;
                        //boktan kod
                        int x = ms.X;
                        int y = ms.Y;
                        x -= 30;
                        if (x > (int)6 * pulwh.X)
                            x -= 60;
                        x /= (int)pulwh.X;
                        if (y > tahtawh.Y / 2)
                            x = 23 - x;
                        // þu an x arrayýn kaçýncý elemanýna týklandýðýnýn deðerini tutuyor.
                        //System.Windows.Forms.MessageBox.Show("" + x);
                        selected = x;
                        if ((tur == 'k' ? kkirik : bkirik) == 0) // kýrýk yoksa
                        {
                            isselected = true;
                            if (tahta[selected].renk != tur)
                            {
                                ((System.Media.SystemSound)System.Media.SystemSounds.Beep).Play();
                                isselected = false;
                            }
                        }
                        else // kýrýk var
                        {
                            if (tur == 'k')
                            {
                                if (zar.Kontrol(selected + 1)) // kýrýðý yerleþtir    zar.X == selected + 1 || zar.Y == selected + 1
                                {
                                    if (tahta[selected].renk == tur || tahta[selected].pulSayisi < 2)//uygunsa
                                    {
                                        if (tahta[selected].pulSayisi == 1 && tahta[selected].renk != tur) // kýr
                                        {
                                            if (tahta[selected].renk == 'k')
                                                kkirik++;
                                            else
                                                bkirik++;
                                            tahta[selected].pulSayisi = 0;
                                        }
                                        tahta[selected].renk = tur;
                                        tahta[selected].pulSayisi++;
                                        //---------------------
                                        if (tur == 'k')
                                            kkirik--;
                                        else
                                            bkirik--;
                                        //---------------------
                                        zar.oyna(selected + 1);
                                        if (zar.bittiMi())
                                            turAtlat();
                                        if (!canplay())
                                        {
                                            System.Windows.Forms.MessageBox.Show("Oynanabilinecek hamle yok, Sýra diðer oyuncuya geçiyor.\n\n Zarlar : " + dzar.X + " - " + dzar.Y);
                                            turAtlat();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (zar.Kontrol(24 - selected)) // kýrýðý yerleþtir      (24 - zar.X) == selected || ( 24 - zar.Y ) == selected
                                {
                                    if (tahta[selected].renk == tur || tahta[selected].pulSayisi < 2)//uygunsa
                                    {
                                        if (tahta[selected].pulSayisi == 1 && tahta[selected].renk != tur) // kýr
                                        {
                                            if (tahta[selected].renk == 'k')
                                                kkirik++;
                                            else
                                                bkirik++;
                                            tahta[selected].pulSayisi = 0;
                                        }
                                        tahta[selected].renk = tur;
                                        tahta[selected].pulSayisi++;
                                        //---------------------
                                        if (tur == 'k')
                                            kkirik--;
                                        else
                                            bkirik--;
                                        //---------------------
                                        zar.oyna(24 - selected);
                                        if (zar.bittiMi())
                                            turAtlat();
                                        if (!canplay())
                                        {
                                            System.Windows.Forms.MessageBox.Show("Oynanabilinecek hamle yok, Sýra diðer oyuncuya geçiyor.\n\n Zarlar : " + dzar.X + " - " + dzar.Y);
                                            turAtlat();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (isselected == true && isclicked == false && ms.LeftButton == ButtonState.Pressed) // seçmiþse
                    {
                        isclicked = true;
                        //boktan kod
                        int x = ms.X;
                        int y = ms.Y;
                        x -= 30;
                        if (x > (int)6 * pulwh.X)
                            x -= 60;
                        x /= (int)pulwh.X;
                        if (y > tahtawh.Y / 2)
                            x = 23 - x;
                        // þu an x arrayýn kaçýncý elemanýna týklandýðýnýn deðerini tutuyor.
                        if (toplamaBasladi(tur) && selected == x) // taþlarý toplamaya baþladýk
                        {
                            if (tahta[selected].pulSayisi > 0 && tahta[selected].renk == tur && ((tur == 'k' ? zar.Kontrol(24 - selected) : zar.Kontrol(selected + 1)) || sonkalan(selected)))//taþý al
                            {
                                tahta[selected].pulSayisi--;
                                if (tahta[selected].pulSayisi == 0)
                                    tahta[selected].renk = '-';
                                if (!sonkalan(selected))
                                {
                                    if (tur == 'b')
                                        zar.oyna(selected + 1);
                                    else
                                        zar.oyna(24 - selected);
                                }
                                else
                                {
                                    zar.maxoyna();
                                }

                                if (oyunBittiMi(tur))
                                {
                                    System.Windows.Forms.MessageBox.Show("Oyunu " + (tur == 'k' ? "kýrmýzý" : "beyaz") + " kazandý !!");
                                }

                                if (zar.bittiMi())
                                    turAtlat();
                                if (!canplay())
                                {
                                    System.Windows.Forms.MessageBox.Show("Oynanabilinecek hamle yok, Sýra diðer oyuncuya geçiyor.\n\n Zarlar : " + dzar.X + " - " + dzar.Y);
                                    turAtlat();
                                }
                            }
                        }
                        else if (validMove(selected, x))
                        {
                            move(selected, x);
                            if (zar.bittiMi())
                                turAtlat();
                            if (!canplay())
                            {
                                System.Windows.Forms.MessageBox.Show("Oynanabilinecek hamle yok, Sýra diðer oyuncuya geçiyor.\n\n Zarlar : " + dzar.X + " - " + dzar.Y);
                                turAtlat();
                            }

                        }
                        isselected = false;
                    }
                }
                else // ai
                {
                    System.Windows.Forms.MessageBox.Show("Bilgisayara Gelen Zarlar " + zar.returnValues());
                    while (bkirik != 0)
                    {
                        //System.Windows.Forms.MessageBox.Show("asdas "+zar.returnValues());
                        bool flag = false;
                        for (int i = 23; i > 17; i--)
                        {
                            if (zar.Kontrol(24 - i)) // kýrýðý yerleþtircez, zar uygun mu diye bakýyoruz :D
                            {
                                flag = true;
                                if (tahta[i].renk == tur || tahta[i].pulSayisi < 2)//uygunsa
                                {
                                    if (tahta[i].pulSayisi == 1 && tahta[i].renk != tur) // kýr
                                    {
                                        if (tahta[i].renk == 'k')
                                            kkirik++;
                                        else
                                            bkirik++;
                                        tahta[i].pulSayisi = 0;
                                    }
                                    tahta[i].renk = tur;
                                    tahta[i].pulSayisi++;
                                    tahta[i].renk = tur;
                                    //---------------------
                                    if (tur == 'k')
                                        kkirik--;
                                    else
                                        bkirik--;
                                    //---------------------
                                    zar.oyna(24 - i);
                                    if (zar.bittiMi())
                                    {
                                        turAtlat();
                                        goto x;
                                    }
                                    if (!canplay())
                                    {
                                        System.Windows.Forms.MessageBox.Show("Oynanabilinecek hamle yok, Sýra diðer oyuncuya geçiyor.\n\n Zarlar : " + dzar.X + " - " + dzar.Y);
                                        turAtlat();
                                        goto x;
                                    }
                                    break;
                                }
                            }
                        }
                        if (flag == false) // oynayamýyoz demek oluyo
                        {
                            System.Windows.Forms.MessageBox.Show("Oynanabilinecek hamle yok, Sýra diðer oyuncuya geçiyor.\n\n Zarlar : " + dzar.X + " - " + dzar.Y);
                            turAtlat();
                            goto x; // boktan kod
                        }
                    }
                    max = -99;
                    maxtable = new Node[24];
                    //System.Windows.Forms.MessageBox.Show("" + zar.returnValues());
                    Node[] ctahta = new Node[24];
                    for (int i = 0; i < 24; i++)
                    {
                        maxtable[i] = new Node();
                        ctahta[i] = new Node();

                        ctahta[i].renk = tahta[i].renk;
                        ctahta[i].pulSayisi = tahta[i].pulSayisi;

                        maxtable[i].renk = tahta[i].renk;
                        maxtable[i].pulSayisi = tahta[i].pulSayisi;
                    }
                    int tempkirik = kkirik;
                    aiPlay(ctahta, zar.returnSelf(), ref tempkirik);
                    for (int i = 0; i < 24; i++)
                    {
                        tahta[i].renk = maxtable[i].renk;
                        tahta[i].pulSayisi = maxtable[i].pulSayisi;
                    }
                    kkirik = maxkirik;
                    turAtlat();
                }
            }
            x:

            base.Update(gameTime);
        }

        Node[] maxtable;
        int max;
        int maxkirik;

        private int aiPlay(Node[] ctahta, Zar czar,ref int ckkirik)
        {
            if (oyunBittiMi(tur, ctahta, ckkirik))
            {
                maxkirik = ckkirik;
                for (int m = 0; m < 24; m++)
                {
                    maxtable[m].renk = ctahta[m].renk;
                    maxtable[m].pulSayisi = ctahta[m].pulSayisi;
                }
                return 34;
            }
            else if (!canplay(ctahta,czar,ckkirik))
            {
                System.Windows.Forms.MessageBox.Show("Oynanabilinecek hamle yok, Sýra diðer oyuncuya geçiyor.\n\n Zarlar : " + dzar.X + " - " + dzar.Y);
                return 0;
            }
            else
            {
                Node[] btahta = new Node[24];
                Zar bzar = czar.returnSelf();
                int bkkirik = ckkirik; // kýrýðý yedeði
                for (int i = 0; i < 24; i++)
                {
                    btahta[i] = new Node();
                    btahta[i].renk = ctahta[i].renk;
                    btahta[i].pulSayisi = ctahta[i].pulSayisi;
                } // fonksiyona ilk gelen tahta ve zar
                for (int i = 0; i < 24; i++) // her kare için bakýyoruz
                {
                    if ((ctahta[i].renk == 'b') && (ctahta[i].pulSayisi > 0)) // taþ bulduk joey !
                    {
                        for (int j = 0; j < 24; j++) // oyntma zamaný, j inci kareye oynayabiliyomu diye bakýyoruz
                        {
                            if (validMove(i, j, ctahta, czar, ref ckkirik)) // hamle oynanabiliyosa
                            {
                                move(i, j, ctahta, czar, ref ckkirik); // oynat
                                if (czar.bittiMi()) // cocuk node  ----------------------------------------- eksik
                                {
                                    int puan = puanSay(ctahta, ckkirik);
                                    if (puan > max)
                                    {
                                        max = puan;
                                        maxkirik = ckkirik;
                                        for (int m = 0; m < 24; m++)
                                        {
                                            maxtable[m].renk = ctahta[m].renk;
                                            maxtable[m].pulSayisi = ctahta[m].pulSayisi;
                                        }
                                    }
                                }
                                else
                                {
                                    if (aiPlay(ctahta, czar, ref ckkirik) == 34) // aþþa nodelara bak
                                        return 34;
                                }
                                czar = bzar.returnSelf();
                                ckkirik = bkkirik;
                                for (int a = 0; a < 24; a++)
                                {
                                    ctahta[a].renk = btahta[a].renk;
                                    ctahta[a].pulSayisi = btahta[a].pulSayisi;
                                }
                            }
                        }
                    }
                }
            }
            return 0;
        }

        private enum puanlar
        {
            toplamak = 3,
            kapi = 4,
            kirmak = 2,
            bosta = -1,
        }

        private int puanSay( Node[] tahta, int kkirik )
        {
            int temp = 0;
            for (int i = 0; i < 24; i++)
            {
                if (tahta[i].renk == tur)
                {
                    if (i < 12 && tahta[i].pulSayisi > 1)
                        temp += (int)puanlar.kapi;
                    else
                        temp += (int)puanlar.bosta;
                }
            }
            temp += kkirik * (int)puanlar.kirmak;
            return temp;
        }

        private bool sonkalan( int x )
        {
            if (tur == 'k')
            {
                for (int i = x - 1; i > 17; i--)
                {
                    if (tahta[i].pulSayisi > 0 && tahta[i].renk == tur)
                        return false;
                }
                return true;
            }
            else
            {
                for (int i = x + 1; i < 6; i++)
                {
                    if (tahta[i].pulSayisi > 0 && tahta[i].renk == tur)
                        return false;
                }
                return true;
            }
        }

        private bool oyunBittiMi( char tur )
        {
            for (int i = 0; i < 24; i++)
            {
                if (tahta[i].renk == tur && tahta[i].pulSayisi > 0)
                    return false;
            }
            return true;
        }

        private bool oyunBittiMi( char tur , Node[] tahta, int kkirik )  // ------------------------- hata var
        {
            for (int i = 0; i < 24; i++)
            {
                if (tahta[i].renk == tur && tahta[i].pulSayisi > 0)
                    return false;
            }
            return true;
        }

        private bool toplamaBasladi( char tur )
        {
            if (tur == 'b')
            {
                for (int i = 6; i < 24; i++) // dýþarda kaldýysa false döndür
                {
                    if (tahta[i].pulSayisi > 0 && tahta[i].renk == tur)
                        return false;
                }
            }
            else
            {
                for (int i = 0; i < 18; i++) // dýþarda kaldýysa false döndür
                {
                    if (tahta[i].pulSayisi > 0 && tahta[i].renk == tur)
                        return false;
                }
            }
            return true;
        }

        private bool toplamaBasladi(char tur, Node[] tahta)
        {
            if (tur == 'b')
            {
                for (int i = 6; i < 24; i++) // dýþarda kaldýysa false döndür
                {
                    if (tahta[i].pulSayisi > 0 && tahta[i].renk == tur)
                        return false;
                }
            }
            else
            {
                for (int i = 0; i < 18; i++) // dýþarda kaldýysa false döndür
                {
                    if (tahta[i].pulSayisi > 0 && tahta[i].renk == tur)
                        return false;
                }
            }
            return true;
        }

        private void turAtlat()
        {
            tur = (tur == 'k' ? 'b' : 'k');
            dzar = zar.at();
            ((System.Media.SystemSound)System.Media.SystemSounds.Asterisk).Play();
            if (!canplay())
            {
                System.Windows.Forms.MessageBox.Show("Oynanabilinecek hamle yok, Sýra diðer oyuncuya geçiyor.\n\n Zarlar : " + dzar.X + " - " + dzar.Y);
                turAtlat();
            }
        }

        private bool validMove(int from, int to,Node[] tahta,Zar zar,ref int kkirik)
        {
            if (tahta[to].renk == tahta[from].renk || tahta[to].pulSayisi < 2)
                //if ((tur == 'k' ? kkirik : bkirik) == 0)
                    if ((tur == 'k' && zar.Kontrol(to - from)) || (tur == 'b' && zar.Kontrol(from - to)))
                        return true;
            return false;

        }

        private bool validMove(int from, int to)
        {
            if (tahta[to].renk == tahta[from].renk || tahta[to].pulSayisi < 2)
                if ((tur == 'k' ? kkirik : bkirik) == 0)
                    if ((tur == 'k' && zar.Kontrol(to - from)) || (tur == 'b' && zar.Kontrol(from - to)))
                        return true;
            return false;

        }

        private bool canplay(  )
        {
            if ((tur == 'k' ? kkirik : bkirik) == 0)
            {
                if ( toplamaBasladi(tur) )
                    return true;
                for (int i = 0; i < 24; i++) // her kare için
                {
                    if (tahta[i].pulSayisi != 0) // bu karede pul varsa
                        for (int j = 0; j < 24; j++) // tüm kareler için gidebiliyrmu diye bak
                        {
                            if (validMove(i, j))
                            {
                                return true; // gidebiliyorsa true
                            }
                        }
                }
            }
            else
            {
                if (tur == 'k')
                {
                    for (int i = 0; i < 24; i++ )
                        if (zar.Kontrol(i + 1)) // kýrýðý yerleþtir
                        {
                            if (tahta[i].renk == tur || tahta[i].pulSayisi < 2)//uygunsa
                            {
                                return true;
                            }
                        }
                }
                else
                {
                    for (int i = 0; i < 24; i++)
                        if ((zar.Kontrol(24 - i))) // kýrýðý yerleþtir
                        {
                            if (tahta[i].renk == tur || tahta[i].pulSayisi < 2)//uygunsa
                            {
                                return true;
                            }
                        }
                }
            }
            return false; // yok olmuyo
        }

        private bool canplay( Node[] tahta, Zar zar , int kkirik)
        {
            //if ((tur == 'k' ? kkirik : bkirik) == 0)
            //{
                if ( toplamaBasladi(tur,tahta) )
                    return true;
                for (int i = 0; i < 24; i++) // her kare için
                {
                    if (tahta[i].pulSayisi != 0) // bu karede pul varsa
                        for (int j = 0; j < 24; j++) // tüm kareler için gidebiliyrmu diye bak
                        {
                            if (validMove(i, j))
                            {
                                return true; // gidebiliyorsa true
                            }
                        }
                }
            //}
            /*else
            {
                if (tur == 'k')
                {
                    for (int i = 0; i < 24; i++ )
                        if (zar.Kontrol(i + 1)) // kýrýðý yerleþtir
                        {
                            if (tahta[i].renk == tur || tahta[i].pulSayisi < 2)//uygunsa
                            {
                                return true;
                            }
                        }
                }
                else
                {
                    for (int i = 0; i < 24; i++)
                        if ((zar.Kontrol(24 - i))) // kýrýðý yerleþtir
                        {
                            if (tahta[i].renk == tur || tahta[i].pulSayisi < 2)//uygunsa
                            {
                                return true;
                            }
                        }
                }
            }*/
            return false; // yok olmuyo
        }

        private void move( int from, int to )
        {
            if( tahta[to].pulSayisi == 1 && tahta[to].renk != tahta[from].renk ) // kýr
            {
                if( tahta[to].renk == 'k' )
                    kkirik++;
                else
                    bkirik++;
                tahta[to].pulSayisi = 0;
            }
            tahta[to].pulSayisi++;
            tahta[to].renk = tahta[from].renk;
            tahta[from].pulSayisi--;
            if( tahta[from].pulSayisi == 0 )
                tahta[from].renk = '-';
            zar.oyna(Math.Abs(to - from));

        }

        private void move(int from, int to,Node[] tahta, Zar zar,ref int kkirik)
        {
            if (tahta[to].pulSayisi == 1 && tahta[to].renk != tahta[from].renk) // kýr
            {
                if (tahta[to].renk == 'k')
                    kkirik++;
                else
                    bkirik++;
                tahta[to].pulSayisi = 0;
            }
            tahta[to].pulSayisi++;
            tahta[to].renk = tahta[from].renk;
            tahta[from].pulSayisi--;
            if (tahta[from].pulSayisi == 0)
                tahta[from].renk = '-';
            zar.oyna(Math.Abs(to - from));

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(Textahta, new Rectangle(0, 0, (int)tahtawh.X, (int)tahtawh.Y), Color.White);
            for ( int i=0; i<24; i++ )
            {
                Node n = tahta[i];
                if ( n.pulSayisi != 0 ) // boþ deðilse
                {
                    int x = 30 + (i > 5 && i < 18 ? 60 : 0) + (int)pulwh.X * (i < 12 ? i : 23 - i);
                    int y = (i < 12 ? 30 : (int)tahtawh.Y - 30 - (int)pulwh.Y);
                    for (int j = 0; j < n.pulSayisi; j++)
                    {
                        spriteBatch.Draw((n.renk == 'k' ? Texkpul : Texbpul), new Rectangle(x, y, (int)pulwh.X, (int)pulwh.Y), Color.White);
                        y += (i < 12 ? (int)pulwh.Y : -(int)pulwh.Y);
                    }
                }
            }
            if (isselected)
            {
                for (int i = 0; i < 24; i++)
                {
                    if (validMove(selected, i))
                    {
                        int x = 30 + (i > 5 && i < 18 ? 60 : 0) + (int)pulwh.X * (i < 12 ? i : 23 - i);
                        int y = (i < 12 ? 10 : (int)tahtawh.Y - 50);
                        spriteBatch.Draw(Textcek, new Rectangle(x+20, y, 40,40), Color.White);
                    }
                }
            }
            if (kkirik > 0)
            {
                spriteBatch.Draw(Texkpul, new Rectangle(480, 390, 60, 60), Color.White);
                spriteBatch.DrawString(font1, "" + kkirik, new Vector2(504,408), Color.Black);
            }
            if (bkirik > 0)
            {
                spriteBatch.Draw(Texbpul, new Rectangle(480, 320, 60, 60), Color.White);
                spriteBatch.DrawString(font1, "" + bkirik, new Vector2(504,338), Color.Black);
            }

            spriteBatch.DrawString(font1, "Zar : " + dzar.X + " - " + dzar.Y,new Vector2(880,740),Color.Black);
            spriteBatch.DrawString(font1, "Sira : " + (tur=='k'?"Kirmizi":"Beyaz") , new Vector2(20, 740), Color.Black);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
