using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Game2.cs
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        LinkedList<Block> ll;
        MouseState ms;
        KeyboardState ks;
        bool isClicked2;
        bool isClicked3;
        LinkedList<Block> poped_blocks;
        bool isClicked;
        long currScore;
        bool Hard;
        LinkedList<Block> templist;
        double mScore;
        double value;
        Block temp;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Attempt to set the display mode to the desired resolution.  Itterates through the display
        /// capabilities of the default graphics adapter to determine if the graphics adapter supports the
        /// requested resolution.  If so, the resolution is set and the function returns true.  If not,
        /// no change is made and the function returns false.
        /// </summary>
        /// <param name="iWidth">Desired screen width.</param>
        /// <param name="iHeight">Desired screen height.</param>
        /// <param name="bFullScreen">True if you wish to go to Full Screen, false for Windowed Mode.</param>
        private bool InitGraphicsMode(int iWidth, int iHeight, bool bFullScreen)
        {
            // If we aren't using a full screen mode, the height and width of the window can
            // be set to anything equal to or smaller than the actual screen size.
            if (bFullScreen == false)
            {
                if ((iWidth <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                    && (iHeight <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height))
                {
                    graphics.PreferredBackBufferWidth = iWidth;
                    graphics.PreferredBackBufferHeight = iHeight;
                    graphics.IsFullScreen = bFullScreen;
                    graphics.ApplyChanges();
                    return true;
                }
            }
            else
            {
                // If we are using full screen mode, we should check to make sure that the display
                // adapter can handle the video mode we are trying to set.  To do this, we will
                // iterate thorugh the display modes supported by the adapter and check them against
                // the mode we want to set.
                foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
                {
                    // Check the width and height of each mode against the passed values
                    if ((dm.Width == iWidth) && (dm.Height == iHeight))
                    {
                        // The mode is supported, so set the buffer formats, apply changes and return
                        graphics.PreferredBackBufferWidth = iWidth;
                        graphics.PreferredBackBufferHeight = iHeight;
                        graphics.IsFullScreen = bFullScreen;
                        graphics.ApplyChanges();
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            System.Windows.Forms.MessageBox.Show("Oyun Kurallaraı : \n\n- Görceiniz 4 Renkte Kutuların Aynı Renklerini Birbirine Dik Veya Yatay Deyecek Şekilde Bitişik Patlatıyorsunuz ..\n Ne Kadar Çok Bitişik Kutu Patlatırsanız Aynı Oranda Puan Alıyorsunuz ..\n Kutular Sağa ve Aşağıya Doğru Öteleniyor ...\n Fazla Puan Alabilek İçin Aynı Renkteki Kutuları Birbirlerine Bitişik Hale Getirip Sonra Patlatmak Bayaa Bi Avantajlı Oluyo :D ..\n Bu Arada Her Sütun Bittiğinde Soldan Rastgele Uzunlukta Sütun Ekleniyor ...\n Geri Alma Yok O Nedenle Dikkatli Olmakta da Fayda Var :D\n\n Kontroller : \n -Sol Click Kutuları Patlatır ,\n-Sağ Click İse Mousenin O Anda Bulunduu Konumadaki Kutuları Patlattığıızda Alacağınız Puanı Hesaplar... \n\n - High Score Sini ss Alıp Yollayanın High Scoresi Geçilir Binu Da Bilseiniz ! :D\n\n\n Bu Tür 2d Oyun Fikri Olan Arkadaşlar da Bana Proje Sölesinler :D", "Kurallar ..",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);
            System.Windows.Forms.DialogResult x = System.Windows.Forms.MessageBox.Show("Zorda Oynamak İster Misin ? Not : 4 yerine 5 renk ama score ekranında bi artsiliin oluyo :D", "Acaba?",System.Windows.Forms.MessageBoxButtons.YesNo);
            currScore = 0;
            mScore = 0;
            if (x == System.Windows.Forms.DialogResult.Yes)
            {
                Hard = true;
            }
            else if (x == System.Windows.Forms.DialogResult.No)
            {
                Hard = false;
            }

            isClicked3 = false;
            value = 0;
            IsMouseVisible = true;
            base.Initialize();
            InitGraphicsMode(390, 500, false);
            isClicked = false;
            isClicked2 = false;
            templist = new LinkedList<Block>();
            temp = new Block(this.Content.Load<Texture2D>("Block"), new Vector2(),Hard);

            ll = new LinkedList<Block>();
            for (int j = 0; j * 30 < graphics.PreferredBackBufferWidth; j++)
            {
                //System.Threading.Thread.Sleep(50);
                for (int i = 0; i * 30 + 90 < graphics.PreferredBackBufferHeight; i++)
                {
                    //System.Threading.Thread.Sleep(50);
                    ll.AddLast(new Block(this.Content.Load<Texture2D>("Block"), new Vector2(j * 30, i * 30),Hard));
                }
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw text
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            ms = Mouse.GetState();
            ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.M))
            {
                currScore += 10759;
            }

            poped_blocks = new LinkedList<Block>();
            if (isClicked == false && ms.LeftButton == ButtonState.Pressed)
            {
                mScore = 1;
                value = 0;
                currScore += Patlat_Say(ms.X, ms.Y, true);
                duscekleri_kontrol();
                yana_kaydir();
                while (bosluk_var_mi())
                {
                    yeni_satir();
                    duscekleri_kontrol();
                    yana_kaydir();
                }
                if ( !bitti_mi() )
                {
                    if(Hard)
                        System.Windows.Forms.MessageBox.Show("Oyun Bitti .. \nSkorun ;  "+currScore+"\n\n Bide Hard Seçmişin Bunu da Bilmek Lazım ;) ","Game Over ...");
                    else
                        System.Windows.Forms.MessageBox.Show("Oyun Bitti .. \nSkorun ;  " + currScore, "Game Over ...");
                    System.Windows.Forms.DialogResult x = System.Windows.Forms.MessageBox.Show("Bidaha?", "Acaba?", System.Windows.Forms.MessageBoxButtons.YesNo);
                    if (x == System.Windows.Forms.DialogResult.Yes)
                    {
                        Initialize();
                    }
                    else if (x == System.Windows.Forms.DialogResult.No)
                    {
                        Exit();
                    }
                }
                isClicked = true;
            }

            /*
            if (ms.MiddleButton == ButtonState.Pressed && isClicked3 == false)
            {
                isClicked3 = true;

                foreach(Block blck in ll)
                    if (blck.position == new Vector2((ms.X / 30) * 30, (ms.Y / 30) * 30))
                    {
                        temp = blck;
                        break;
                    }
                try
                {
                    ll.Remove(temp);
                }
                catch (Exception ex)
                {

                }

                duscekleri_kontrol();
                yana_kaydir();
                while (bosluk_var_mi())
                {
                    yeni_satir();
                    duscekleri_kontrol();
                    yana_kaydir();
                }

            }

            if (ms.MiddleButton == ButtonState.Released)
                isClicked3 = false;
            */
            if ( ms.RightButton == ButtonState.Pressed && isClicked2 == false )
            {
                isClicked2 = true;
                mScore = 1;
                value = 0;
                Patlat_Say(ms.X, ms.Y, false);
                mScore += 1;
            }

            if( ms.LeftButton == ButtonState.Released )
                isClicked = false;

            if (ms.RightButton == ButtonState.Released)
                isClicked2 = false;

            base.Update(gameTime);
        }

        protected bool bitti_mi()
        {
            foreach (Block blck in ll)
            {
                foreach (Block blck2 in ll)
                {
                    if (blck.position.X == blck2.position.X && blck.color == blck2.color )
                    {
                        if (blck.position.Y == blck2.position.Y + blck2.Texture.Height || blck.position.Y == blck2.position.Y - blck2.Texture.Height)
                            return true;
                    }
                    if (blck.position.Y == blck2.position.Y && blck.color == blck2.color)
                    {
                        if (blck.position.X == blck2.position.X + blck2.Texture.Width || blck.position.X == blck2.position.X - blck2.Texture.Width )
                            return true;
                    }
                }
            }
            return false;
        }

        protected bool bosluk_var_mi()
        {
            foreach (Block blck in ll)
            {
                if (blck.position.X == 0)
                    return false;
            }
            return true;
        }

        protected void yeni_satir()
        {
            Random r = new Random();
            for (int i = 0; i < r.Next(14); i++)
                ll.AddLast(new Block(Content.Load<Texture2D>("Block"), new Vector2(0, i*30),Hard));
        }

        protected void yana_kaydir()
        {
        bas1:
            foreach (Block blck in ll)
            {
                if (blck.position.X != 360 )
                {
                    foreach (Block blck2 in ll)
                    {
                        if (blck.position.Y == blck2.position.Y)
                            if (blck.position.X + blck.Texture.Width == blck2.position.X)
                            {
                                //yani dolu
                                goto next_block1;
                            }
                            else
                                continue; // aramaya devam et
                    }
                    temp = blck;
                    goto deleter1;
                }
            next_block1:
                continue;
            }
            // İşlem Tamamlandı
            goto last_point1;
        deleter1:
            sil('x');
            goto bas1;
        last_point1:
            return;
        }

        protected void duscekleri_kontrol()
        {
            bas:
            foreach (Block blck in ll)
            {
                if (blck.position.Y != 390)
                {
                    foreach (Block blck2 in ll)
                    {
                        if(blck.position.X == blck2.position.X)
                            if (blck.position.Y + blck.Texture.Height == blck2.position.Y)
                            {
                                //altı dolu
                                goto next_block;
                            }
                            else
                                continue; // aramaya devam et
                    }
                    temp = blck;
                    goto deleter;
                }
            next_block:
                continue;
            }
            // İşlem Tamamlandı
            goto last_point;
        deleter:
            sil('y');
       goto bas;
   last_point:
       return;
        }

        protected void sil(char x)
        {
            if (x == 'y')
            {
                ll.Remove(temp);
                temp.position.Y += 30;
                ll.AddLast(temp);
            }
            else
            {
                ll.Remove(temp);
                temp.position.X += 30;
                ll.AddLast(temp);
            }
        }

        protected bool isCollision(Vector2 pos,Texture2D tex,Vector2 obj)
        {
            if (pos.X + tex.Width >= obj.X && pos.X <= obj.X)
                if (pos.Y + tex.Height >= obj.Y && pos.Y <= obj.Y)
                    return true;
            return false;
        }

        protected int Patlat_Say(int x,int y,bool wtPop)
        {
            Color clr = new Color();
            templist = new LinkedList<Block>();
            if (wtPop == false)
                foreach (Block blck in ll)
                {
                    templist.AddLast(blck);
                }
            foreach(Block blck in ll)
                if(blck.position == new Vector2( (x/30)*30,(y/30)*30) )
                {
                    clr = blck.color;
                    etrafi_say((x / 30) * 30, (y / 30) * 30, clr,wtPop);

                    mScore = 0;
                    int artis = 10;
                    for (int i = 0; i < value; i++)
                    {
                        if (i % 2 == 0)
                        {
                            artis += (i / 2) * 10 ;
                        }
                        mScore += artis;
                    }

                    return (int)mScore;
                }
            return 0;
        }

        protected void etrafi_say(int x,int y,Color color,bool wtPop)
        {
            //System.Windows.Forms.MessageBox.Show("x -->"+x+"\ny -->"+y ,"asd");
                //isFound(new Vector2(x + 30, y - 30), color, wtPop);
                isFound(new Vector2(x + 30, y), color, wtPop);
                //isFound(new Vector2(x + 30, y + 30), color, wtPop);
                isFound(new Vector2(x - 30, y), color, wtPop);
                isFound(new Vector2(x, y + 30), color, wtPop);
                //isFound(new Vector2(x - 30, y + 30), color, wtPop);
                //isFound(new Vector2(x - 30, y - 30), color, wtPop);
                isFound(new Vector2(x, y - 30), color, wtPop);

        }

        protected void isFound(Vector2 pos,Color color,bool isPop)
        {
                if (isPop == true)
                {
                    foreach (Block blck in ll)
                    {
                        if (blck.position == pos && blck.color == color)
                        {
                            value++;
                            temp = blck;
                            goto xyz;
                        }
                    }
                    return;
                xyz:
                    ll.Remove(temp);
                    etrafi_say((int)pos.X, (int)pos.Y, color, true);
                }
                else
                {
                    foreach (Block blck in templist)
                    {
                        if (blck.position == pos && blck.color == color)
                        {
                            value++;
                            temp = blck;
                            goto xxx;
                        }
                    }
                    return;
                xxx:
                    templist.Remove(temp);
                    etrafi_say((int)pos.X, (int)pos.Y, color, false);
                }
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);
            spriteBatch.Begin();

            spriteBatch.DrawString(Content.Load<SpriteFont>("SpriteFont1"), "Score -->" + currScore, new Vector2(10,435), Color.Black);
            spriteBatch.DrawString(Content.Load<SpriteFont>("SpriteFont1"), "On Mouse Score -->" + (mScore-1), new Vector2(10, 455), Color.Black);
            spriteBatch.Draw(Content.Load<Texture2D>("Bounds"), new Vector2(0, 420), Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("Bounds"), new Vector2(0, 492), Color.White);
            foreach (Block blck in ll)
                blck.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
