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

namespace Tetris
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        const int padwidth = 15;
        const int padheight = 20;
        const int gameoverliney = 3;
        const int blocklenghts = 30;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D block;

        Color[,] pad;
        char[,] currshape;
        Vector2 currshapeLenghts;
        int currshapeid; // www.upload.wikimedia.org/wikipedia/commons/thumb/3/39/Tetrominoes_IJLO_STZ_Worlds.svg/360px-Tetrominoes_IJLO_STZ_Worlds.svg.png
        Vector2 currshapecoords;
        Color currShapesColor;

        int pressedkey;
        bool ispressed = false;
        bool isdownpressed = false;

        int clearedlines = 0;
        int speed;
        int Score;
        double lastmovetime;
        Random rand;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferHeight = blocklenghts * padheight + 60; // 40 x 12 + 60
            graphics.PreferredBackBufferWidth = blocklenghts * padwidth; // 40 x 10
            graphics.ApplyChanges();
            IsMouseVisible = true;
            //graphics.ToggleFullScreen();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            block = this.Content.Load<Texture2D>("Images/Block");
            clearedlines = 0;
            Score = 0;
            lastmovetime = 0;
            clearedlines = 0;
            pad = new Color[padwidth, padheight];
            currShapesColor = Color.White;
            for (int i = 0; i < padwidth; i++)
                for (int j = 0; j < padheight; j++)
                    pad[i, j] = Color.WhiteSmoke;
            rand = new Random();
            int x = rand.Next(7);
            initcurrshape(x);
        }

        protected override void UnloadContent()
        {
        }

        protected void initcurrshape(int id)
        {
            currshapecoords = new Vector2(padwidth / 2, 0);
            switch(id)
            {
                case 0 :
                    currshapeLenghts = new Vector2(4, 1);
                    currShapesColor = Color.Cyan;
                    currshape = new char[4, 1];
                    currshape[0, 0] = 'x';
                    currshape[1, 0] = 'x';
                    currshape[2, 0] = 'x';
                    currshape[3, 0] = 'x';
                    currshapeid = 0;
                    break;
                case 1:
                    currshapeLenghts = new Vector2(3, 2);
                    currShapesColor = Color.DarkBlue;
                    currshape = new char[3, 2];
                    currshape[0, 0] = 'x';
                    currshape[0, 1] = 'x';
                    currshape[1, 1] = 'x';
                    currshape[2, 1] = 'x';
                    currshapeid = 1;
                    break;
                case 2:
                    currshapeLenghts = new Vector2(3, 2);
                    currShapesColor = Color.Orange;
                    currshape = new char[3, 2];
                    currshape[2, 0] = 'x';
                    currshape[0, 1] = 'x';
                    currshape[1, 1] = 'x';
                    currshape[2, 1] = 'x';
                    currshapeid = 2;
                    break;
                case 3:
                    currshapeLenghts = new Vector2(2, 2);
                    currShapesColor = Color.Yellow;
                    currshape = new char[2, 2];
                    currshape[0, 0] = 'x';
                    currshape[0, 1] = 'x';
                    currshape[1, 0] = 'x';
                    currshape[1, 1] = 'x';
                    currshapeid = 3;
                    break;
                case 4:
                    currshapeLenghts = new Vector2(3, 2);
                    currShapesColor = Color.Green;
                    currshape = new char[3, 2];
                    currshape[1, 0] = 'x';
                    currshape[2, 0] = 'x';
                    currshape[0, 1] = 'x';
                    currshape[1, 1] = 'x';
                    currshapeid = 4;
                    break;
                case 5:
                    currshapeLenghts = new Vector2(3, 2);
                    currShapesColor = Color.Purple;
                    currshape = new char[3, 2];
                    currshape[1, 0] = 'x';
                    currshape[0, 1] = 'x';
                    currshape[1, 1] = 'x';
                    currshape[2, 1] = 'x';
                    currshapeid = 5;
                    break;
                case 6:
                    currshapeLenghts = new Vector2(3, 2);
                    currShapesColor = Color.Red;
                    currshape = new char[3, 2];
                    currshape[0, 0] = 'x';
                    currshape[1, 0] = 'x';
                    currshape[1, 1] = 'x';
                    currshape[2, 1] = 'x';
                    currshapeid = 6;
                    break;
                default:
                    Environment.Exit(Environment.ExitCode);
                    break;
            }
        }

        protected void rotate90()
        {
            int x, y;
            x = (int)currshapeLenghts.Y; // yeni x = eski y
            y = (int)currshapeLenghts.X; // yeni y = eski x
            char[,] newshape = new char[x, y];

            for (int i = 0; i< currshapeLenghts.X;i++ )
            {
                for (int j = 0; j < currshapeLenghts.Y; j++)
                {
                    newshape[(int)currshapeLenghts.Y - j - 1, i] = currshape[i, j];
                }
            }

            currshape = newshape;
            currshapeLenghts = new Vector2(x, y);

            if (currshapecoords.X + currshapeLenghts.X >= padwidth)
                currshapecoords.X = padwidth - currshapeLenghts.X;
            else if (currshapecoords.X + currshapeLenghts.X < 0)
                currshapecoords.X = currshapeLenghts.X;
            if (currshapecoords.Y + currshapeLenghts.Y >= padheight)
                currshapecoords.Y = padheight - currshapeLenghts.Y;
            else if (currshapecoords.Y + currshapeLenghts.Y < 0)
                currshapecoords.Y = currshapeLenghts.Y;
        }

        protected void movecurrshape()
        {
            currshapecoords.Y++;
            if (currshapecoords.Y + currshapeLenghts.Y - 1 >= padheight || iscollisioning() )
            {
                currshapecoords.Y--;
                for (int i = 0; i < currshapeLenghts.X; i++)
                    for (int j = 0; j < currshapeLenghts.Y; j++)
                        if (currshape[i, j] == 'x')
                            pad[(int)currshapecoords.X + i,(int)currshapecoords.Y + j] = currShapesColor;
                initcurrshape(rand.Next(7));
            }
        }

        protected void movehorizontally(int x)
        {
            currshapecoords.X += x;
            if (currshapecoords.X + currshapeLenghts.X - 1 >= padwidth || currshapecoords.X < 0 || iscollisioning() )
            {
                currshapecoords.X -= x;
            }
        }

        protected bool iscollisioning()
        {
            for (int i = 0; i < currshapeLenghts.X; i++)
                for (int j = 0; j < currshapeLenghts.Y; j++)
                    if (currshape[i, j] == 'x')
                        if (pad[(int)currshapecoords.X + i, (int)currshapecoords.Y + j] != Color.WhiteSmoke)
                            return true;
            return false;
        }

        protected void clearlines()
        {
            bool isfound;
            int combo = 0;
            bas:
            for (int i = 0; i < padheight; i++) // y
            {
                isfound = false; 
                for (int j = 0; j < padwidth; j++) // x
                {
                    if (pad[j, i] == Color.WhiteSmoke) // j,i -> x,y
                    {
                        isfound = true;
                        break;
                    }
                }
                if (isfound == false)
                {
                    for (int x = 0; x < padwidth; x++)
                        pad[x, i] = Color.WhiteSmoke; // x,y
                    effectgravity();
                    combo++;
                    goto bas;
                }
            }
            if (combo == 0)
                return;
            clearedlines += combo;
            Score += (100 * (int)Math.Pow(2, combo));
            return;
        }

        protected void effectgravity()
        {
            init:
            for (int i = 0; i < padwidth; i++)
                for (int j = 0; j < padheight-1; j++)
                    if( pad[i,j] != Color.WhiteSmoke )
                        if( pad[i,j+1] == Color.WhiteSmoke )
                        {
                            pad[i,j+1] = pad[i,j];
                            pad[i, j] = Color.WhiteSmoke;
                            goto init;
                        }
            return;
        }

        protected bool isgameover()
        {
            for (int i = 0; i < padwidth; i++)
                for (int j = 0; j < padheight; j++)
                    if (pad[i, j] != Color.WhiteSmoke && j < gameoverliney )
                        return true;
            return false;
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if ( isdownpressed == false && ks.IsKeyDown(Keys.Down))
            {
                speed = 37;
                isdownpressed = true;
            }
            else if ( isdownpressed == true && ks.IsKeyUp(Keys.Down))
            {
                speed = clearedlines;
                isdownpressed = false;
            }
            //---------------------------------
            if (ispressed)
            {
                if (ks.IsKeyUp((Keys)pressedkey))
                    ispressed = false;
            }
            else
            {
                if (ks.IsKeyDown(Keys.A))
                {
                    ispressed = true;
                    pressedkey = (int)Keys.A;
                    rotate90();
                    rotate90();
                    rotate90();
                }
                else if (ks.IsKeyDown(Keys.D))
                {
                    ispressed = true;
                    pressedkey = (int)Keys.D;
                    rotate90();
                }
                else if (ks.IsKeyDown(Keys.Left))
                {
                    ispressed = true;
                    pressedkey = (int)Keys.Left;
                    movehorizontally(-1);
                }
                else if (ks.IsKeyDown(Keys.Right))
                {
                    ispressed = true;
                    pressedkey = (int)Keys.Right;
                    movehorizontally(1);
                }
            }
            if (speed > 37)
                speed = 37;
            if (gameTime.TotalGameTime.TotalMilliseconds - lastmovetime > 1000 - (25 * speed) )
            {
                lastmovetime = gameTime.TotalGameTime.TotalMilliseconds;
                movecurrshape();
                clearlines();
                if (isgameover())
                    LoadContent();
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);

            spriteBatch.Begin();

            for (int i = 0; i < padwidth; i++)
                for (int j = 0; j < padheight; j++)
                    spriteBatch.Draw(block, new Rectangle(i * blocklenghts, j * blocklenghts, blocklenghts, blocklenghts), pad[i, j]);

            for (int i = 0; i < currshapeLenghts.X; i++)
                for (int j = 0; j <currshapeLenghts.Y; j++)
                    if( currshape[i,j] == 'x' )
                        spriteBatch.Draw(block, new Rectangle((int)currshapecoords.X * blocklenghts + i * blocklenghts, (int)currshapecoords.Y * blocklenghts + j * blocklenghts, blocklenghts, blocklenghts), currShapesColor);

            spriteBatch.Draw(block, new Rectangle(0, blocklenghts * padheight, blocklenghts * padwidth, 2), Color.DarkGray);

            spriteBatch.Draw(block, new Rectangle(0, blocklenghts * gameoverliney, blocklenghts * padwidth, 1), Color.LightGray);

            spriteBatch.DrawString(this.Content.Load<SpriteFont>("SpriteFont1"), "Cleared Line Count : " + clearedlines , new Vector2(20,graphics.PreferredBackBufferHeight - 50 ), Color.Black);
            spriteBatch.DrawString(this.Content.Load<SpriteFont>("SpriteFont1"), "Score : " + Score, new Vector2(20, graphics.PreferredBackBufferHeight - 30), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
