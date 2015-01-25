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

namespace Bubble_Game_2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D block;
        Texture2D bounds;
        Texture2D refresh;
        Color[,] table;
        Color oldcolor;
        bool bpressed = false;
        const int x=15, y=15;
        int counter = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 450;
            graphics.PreferredBackBufferHeight = 550;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            base.Initialize();
            table = new Color[x,y];
            Random r = new Random();
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                {
                    int color = r.Next(5);
                    switch (color)
                    {
                        case 0: table[i, j] = Color.Red; break;
                        case 1: table[i, j] = Color.Blue; break;
                        case 2: table[i, j] = Color.Purple; break;
                        case 3: table[i, j] = Color.Yellow; break;
                        case 4: table[i, j] = Color.ForestGreen; break;
                        case 5: table[i, j] = Color.Orange; break;
                        default: table[i, j] = Color.Black; break;
                    }
                }
            IsMouseVisible = true;
            oldcolor = table[0, 0];
            counter = 0;
            bpressed = false;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            block = this.Content.Load<Texture2D>("Block");
            bounds = this.Content.Load<Texture2D>("Bounds");
            refresh = this.Content.Load<Texture2D>("refresh1");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed && bpressed == false)
            {
                bpressed = true;
                Rectangle msclick = new Rectangle(ms.X, ms.Y, 1, 1);
                oldcolor = table[0, 0];
                if (msclick.Intersects(new Rectangle(50, y * 30 + 20, 40, 40)) && oldcolor != Color.Red) // Red
                {
                    updategame(Color.Red, 0, 0);
                    counter++;
                }
                else if (msclick.Intersects(new Rectangle(110, y * 30 + 20, 40, 40)) && oldcolor != Color.Blue) // Blue
                {
                    updategame(Color.Blue, 0, 0);
                    counter++;
                }
                else if (msclick.Intersects(new Rectangle(170, y * 30 + 20, 40, 40)) && oldcolor != Color.Purple)// Purple
                {
                    updategame(Color.Purple, 0, 0);
                    counter++;
                }
                else if (msclick.Intersects(new Rectangle(230, y * 30 + 20, 40, 40)) && oldcolor != Color.Orange) // Orange
                {
                    updategame(Color.Orange, 0, 0);
                    counter++;
                }
                else if (msclick.Intersects(new Rectangle(290, y * 30 + 20, 40, 40)) && oldcolor != Color.ForestGreen) // Green
                {
                    updategame(Color.ForestGreen, 0, 0);
                    counter++;
                }
                else if (msclick.Intersects(new Rectangle(350, y * 30 + 20, 40, 40)) && oldcolor != Color.Yellow) // Yellow
                {
                    updategame(Color.Yellow, 0, 0);
                    counter++;
                }
                else if (msclick.Intersects(new Rectangle(420, 520, 22, 22)))
                {
                    this.Initialize();
                }

                if (gameover())
                {
                    if (counter < 15)
                        System.Windows.Forms.MessageBox.Show("" + counter + " Hamlede Oyunu Tamamladın Amk Senden Adam Olur !", "Win !");
                    else
                        System.Windows.Forms.MessageBox.Show("" + counter + " Hamlede Oyunu Tamamladın !", "Win !");
                    this.Initialize();
                }
            }
            if (ms.LeftButton == ButtonState.Released && bpressed != false)
                bpressed = false;

            base.Update(gameTime);
        }

        public void updategame(Color currcolor,int i,int j)
        {
            if ( i < 0 || j < 0 || i >= x || j >= y )
                return;
            if (table[i, j] == oldcolor)
            {
                table[i, j] = currcolor;
                updategame(currcolor, i, j + 1);
                updategame(currcolor, i, j - 1);
                updategame(currcolor, i + 1, j);
                updategame(currcolor, i - 1, j);
                return;
            }
            return;
        }

        public bool gameover()
        {
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    if (table[i, j] != table[0,0])
                        return false;
            return true;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);

            spriteBatch.Begin();

            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    spriteBatch.Draw(block, new Rectangle(i * 30, j * 30, 30, 30),table[i,j]);

            spriteBatch.Draw(bounds,new Rectangle(0,y*30,graphics.PreferredBackBufferWidth,3),Color.Black);

            spriteBatch.DrawString(this.Content.Load<SpriteFont>("SpriteFont1"), "" + counter, new Vector2(10,520), Color.Black);

            spriteBatch.Draw(refresh, new Rectangle(420,520,22,22), Color.WhiteSmoke);

            spriteBatch.Draw(block, new Rectangle(50, y * 30 + 20, 40, 40), Color.Red);
            spriteBatch.Draw(block, new Rectangle(110, y * 30 + 20, 40, 40), Color.Blue);
            spriteBatch.Draw(block, new Rectangle(170, y * 30 + 20, 40, 40), Color.Purple);
            spriteBatch.Draw(block, new Rectangle(230, y * 30 + 20, 40, 40), Color.Orange);
            spriteBatch.Draw(block, new Rectangle(290, y * 30 + 20, 40, 40), Color.ForestGreen);
            spriteBatch.Draw(block, new Rectangle(350, y * 30 + 20, 40, 40), Color.Yellow);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
