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

namespace Counter_Strike_2D
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        const int wh = 1024;
        const int hg = 768;
        const int manwh = 40;
        const int manhg = 40;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D ground;
        Texture2D man;
        Texture2D bullet;
        Texture2D blood;
        Texture2D block;
        SpriteFont sf;
        SpriteFont menufont;
        bool menu = false;
        List<Player> players;
        Player1C player1;
        List<Bullet> bullets;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = wh;
            graphics.PreferredBackBufferHeight = hg;
            graphics.ApplyChanges();
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            players = new List<Player>();
            bullets = new List<Bullet>();
            
            
            player1 = new Player1C(new Vector2(0, 0), Color.Black, man, new Vector2(wh, hg), manwh, manhg);

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            man = this.Content.Load<Texture2D>("man");
            ground = this.Content.Load<Texture2D>("ground");
            bullet = this.Content.Load<Texture2D>("bullet");
            sf = this.Content.Load<SpriteFont>("spriteFont1");
            menufont = this.Content.Load<SpriteFont>("menufont");
            block = this.Content.Load<Texture2D>("block");
            blood = this.Content.Load<Texture2D>("blood");

            Random a = new Random();
            for (int i = 0; i < 6; i++ )
                players.Add(new Player(new Vector2(a.Next(20, wh * 3 - manwh - 20), a.Next(20, hg * 3 - manhg - 20)), man, new Vector2(wh, hg), manwh, manhg, blood));
        }

        protected override void UnloadContent()
        {
        }

        protected void buyGun(int x)
        {
            switch (x)
            {
                case 1:
                    if (player1.money > 600)
                    {
                        player1.gun = new glock();
                        player1.money -= 600;
                        refresh();
                    }
                    break;
                case 2:
                    if (player1.money > 1500)
                    {
                        player1.gun = new p90();
                        player1.money -= 1500;
                        refresh();
                    }
                    break;
                case 3:
                    if (player1.money > 2000)
                    {
                        player1.gun = new ak47();
                        player1.money -= 2000;
                        refresh();
                    }
                    break;
                case 4:
                    if (player1.money > 2000)
                    {
                        player1.gun = new m4a1();
                        player1.money -= 2000;
                        refresh();
                    }
                    break;
                case 5:
                    if (player1.money > 3500)
                    {
                        player1.gun = new awp();
                        player1.money -= 3500;
                        refresh();
                    }
                    break;
            }
        }

        protected void refresh()
        {
            player1.ooammo = false;
            player1.reload = false;
            player1.ammo = player1.gun.mammo;
            player1.clip = player1.gun.mclip;
        }

        protected void buyAmmo()
        {
            if (player1.money >= 500)
            {
                player1.money -= 500;
                refresh();
            }

        }

        protected void buyLife()
        {
            if (player1.money >= 2000)
            {
                player1.money -= 2000;
                player1.health = 100;
            }
        }

        protected override void Update(GameTime gameTime)
        {


            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.B) && menu == false)
            {
                menu = true;
            }
            if (menu)
            {
                if (ks.IsKeyDown(Keys.D1))
                {
                    buyGun(1);
                    menu = false;
                }
                if (ks.IsKeyDown(Keys.D2))
                {
                    buyGun(2);
                    menu = false;
                } if (ks.IsKeyDown(Keys.D3))
                {
                    buyGun(3);
                    menu = false;
                } if (ks.IsKeyDown(Keys.D4))
                {
                    buyGun(4);
                    menu = false;
                } if (ks.IsKeyDown(Keys.D5))
                {
                    buyGun(5);
                    menu = false;
                } if (ks.IsKeyDown(Keys.K))
                {
                    buyAmmo();
                    menu = false;
                }
                if (ks.IsKeyDown(Keys.H))
                {
                    buyLife();
                    menu = false;
                }
                if (ks.IsKeyDown(Keys.D0))
                {
                    menu = false;
                }
            }





            Vector2 to = player1.Update(gameTime);
            if (to != Vector2.Zero)
            {
                //System.Windows.Forms.MessageBox.Show(""+ to.Y);
                bullets.Add(new Bullet(bullet,player1.gun.mdamage, new Vector2(player1.playerPos.X + wh, player1.playerPos.Y + hg), (+player1.playerPos.Y + hg - to.Y) / (+player1.playerPos.X + wh - to.X), (+player1.playerPos.X + wh - to.X), 20,false));
            }

            for (int i = 0; i < players.Count; i++ )
            {
                Player p = players[i];
                if (p.addmoney == true || p.isdied == false)
                {
                    Vector2 v = p.Update(gameTime, player1.playerPos);
                    if (v != Vector2.Zero)
                    {
                        bullets.Add(new Bullet(bullet, p.gun.mdamage, p.playerPos, (p.playerPos.Y - to.Y) / (p.playerPos.X - to.X), (p.playerPos.X - v.X), 20, true));
                    }
                    if (p.addmoney)
                    {
                        player1.money += 300;
                        Random a = new Random();
                        players.Add(new Player(new Vector2(a.Next(0, wh * 3), a.Next(0, hg * 3)), man, new Vector2(wh, hg), manwh, manhg, blood));
                        p.addmoney = false;
                    }
                }
            }
            for (int i = 0; i < bullets.Count; i++ )
            {
                Bullet b = bullets[i];
                b.Update(players,player1);
                if (isOutOfBounds(b.pos))
                    bullets.RemoveAt(i);
            }
            base.Update(gameTime);
        }

        protected bool isOutOfBounds(Vector2 pos)
        {
            if (pos.X < 0 || pos.Y < 0 || pos.X > wh * 3 || pos.Y > hg * 3)
                return true;
            return false;
        }

        protected void drawGround(SpriteBatch sb)
        {

            sb.Draw(ground, new Rectangle((int)(-wh - ((player1.playerPos.X) - (wh / 2))), (int)(-hg-((player1.playerPos.Y) - (hg / 2))), wh * 3, hg * 3), Color.White);
        }

        protected void drawPlayers(SpriteBatch sb)
        {
            MouseState ms = Mouse.GetState();
            int x = ms.X;
            int y = ms.Y;
            Vector2 to = new  Vector2(ms.X - ((int)(-wh - ((player1.playerPos.X) - (wh / 2)))), ms.Y - ((int)(-hg - ((player1.playerPos.Y) - (hg / 2)))));
            float tan = (+player1.playerPos.Y + hg - to.Y) / (+player1.playerPos.X + wh - to.X);
            float angle = MathHelper.ToDegrees((float)Math.Atan(tan));
            angle -= 90;
            float rotation;
            if (+player1.playerPos.X + wh - to.X < 0)
                rotation = MathHelper.ToRadians(angle+180);
            else
                rotation = MathHelper.ToRadians(angle);
            sb.Draw(man, new Rectangle(wh/2, hg/2, manwh, manhg), null,Color.White,rotation,Vector2.Zero,SpriteEffects.None,0); //player1
            foreach (Player p in players)
            {
                p.Draw(sb, new Vector2((int)(-wh - ((player1.playerPos.X) - (wh / 2))), (int)(-hg - ((player1.playerPos.Y) - (hg / 2)))));
            }
            foreach (Bullet b in bullets)
            {
                b.Draw(sb,new Vector2((int)(-wh - ((player1.playerPos.X) - (wh / 2))), (int)(-hg - ((player1.playerPos.Y) - (hg / 2)))));
            }
        }

        protected void drawPanel(SpriteBatch sb)
        {
            if (menu)
            {
                sb.DrawString(menufont, "1 : Glock( 20 | 100 ) | $600", new Vector2(50, 20), Color.White);
                sb.DrawString(menufont, "2 : p90( 20 | 100 ) | $1500", new Vector2(50, 50), Color.White);
                sb.DrawString(menufont, "3 : ak47( 20 | 100 ) | $2200", new Vector2(50, 80), Color.White);
                sb.DrawString(menufont, "4 : m4a1( 20 | 100 ) | $2200", new Vector2(50, 110), Color.White);
                sb.DrawString(menufont, "5 : awp( 20 | 100 ) | $3000", new Vector2(50, 140), Color.White);
                sb.DrawString(menufont, "k : ammo | $500", new Vector2(50, 170), Color.White);
                sb.DrawString(menufont, "h : 100 hp | $2000", new Vector2(50, 200), Color.White);
                sb.DrawString(menufont, "0 : close menu | free :)", new Vector2(50, 230), Color.White);
            }

            sb.DrawString(sf, "$ " + player1.money, new Vector2(25, hg - 80), Color.Red);
            sb.DrawString(sf, "" + player1.gun.mname , new Vector2(25, hg - 50), Color.Red);
            sb.DrawString(sf, "" + player1.health + " <3", new Vector2(wh - 130, hg - 80), Color.Red);
            sb.DrawString(sf, "" + player1.clip + " | " + player1.ammo,new Vector2( wh - 150,hg - 50 ),Color.Red);
            if( player1.reload == true )
                sb.DrawString(sf, " Reloading ! .. ", new Vector2(wh/2 - 55, hg/2 - 20), Color.Blue);
            if( player1.ooammo == true )
                sb.DrawString(sf, " Out Of Ammo !! ..", new Vector2(wh / 2 - 70, hg / 2 - 20), Color.Blue);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            drawGround(spriteBatch);
            drawPlayers(spriteBatch);
            drawPanel(spriteBatch);
            

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
