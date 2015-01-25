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

namespace SnakeXna
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        const int width = 15;
        const int height = 15;
        const int nodelenghts = 30;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D snakenode;
        Texture2D food;
        Texture2D empty;

        Vector2 foodcoords;
        bool iseated = false;
        double lastmove;
        char dir;
        public class node
        {
            public int x, y;
            public node next;
            public node prev;
            public node(int x,int y,node next,node prev)
            {
                this.x = x;
                this.y = y;
                this.next = next;
                this.prev = prev;
            }
            public node getClone()
            {
                return new node(x, y, next, prev);
            }
        }
        node snakehead;
        int Score;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = width * nodelenghts;
            graphics.PreferredBackBufferHeight = height * nodelenghts + 40;
            graphics.ApplyChanges();
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            food = this.Content.Load<Texture2D>("Images/food");
            snakenode = this.Content.Load<Texture2D>("Images/node");
            empty = this.Content.Load<Texture2D>("Images/empty");

            lastmove = 0;
            Score = 0;

            foodcoords = getRandVector();

            node head = new node(3,3,null,new node(2,3,null,new node(1,3,null,null)));
            head.prev.next = head;
            head.prev.prev.next = head.prev;
            snakehead = head;
            dir = 'd';
        }

        protected override void UnloadContent()
        {
        }

        protected void move()
        {
            node temp = snakehead;
            if (iseated == false)
            {
                while (temp.prev != null)
                    temp = temp.prev;
                // temp.prev == null
                temp.next.prev = null;
                //snake.RemoveAt(getid(temp));
            }
            iseated = false;

            Vector2 v = Vector2.Zero;
            if (dir == 'w')
                v.Y = -1;
            else if (dir == 's')
                v.Y = 1;
            else if (dir == 'a')
                v.X = -1;
            else if (dir == 'd')
                v.X = 1;
            temp = snakehead.getClone();
            temp.x += (int)v.X;
            temp.y += (int)v.Y;
            temp.next = null;
            temp.prev = snakehead;
            snakehead.next = temp;
            //snake.Add(temp);
            snakehead = temp;
        }

        protected Vector2 getRandVector()
        {
            Random rand = new Random();
            return new Vector2(rand.Next(width),rand.Next(height));
        }

        protected bool isoutofbounds()
        {
            if (snakehead.x < 0 || snakehead.y < 0 || snakehead.x >= width || snakehead.y >= height)
                return true;
            return false;
        }

        protected bool iscracheditself()
        {
            node temp = snakehead.getClone();
            while (temp.prev != null)
            {
                temp = temp.prev.getClone();
                if (temp.x == snakehead.x && temp.y == snakehead.y)
                    return true;
            }
            return false;
        }

        protected bool iseatedfood()
        {
            if (snakehead.x == foodcoords.X && snakehead.y == foodcoords.Y)
                return true;
            return false;
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Left) && dir != 'd')
                dir = 'a';
            else if (ks.IsKeyDown(Keys.Right) && dir != 'a')
                dir = 'd';
            else if (ks.IsKeyDown(Keys.Up) && dir != 's')
                dir = 'w';
            else if (ks.IsKeyDown(Keys.Down) && dir != 'w')
                dir = 's';
            if( gameTime.TotalGameTime.TotalMilliseconds - lastmove > 55 )
            {
                lastmove = gameTime.TotalGameTime.TotalMilliseconds;
                move();
                if (isoutofbounds())
                    LoadContent();
                if (iscracheditself())
                    LoadContent();
                if (iseatedfood())
                {
                    foodcoords = getRandVector();
                    Score += 102 + new Random().Next(1500);
                    iseated = true;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);

            spriteBatch.Begin();

            spriteBatch.Draw(empty, new Rectangle(0, 0, width * nodelenghts, 2), Color.Gray);
            spriteBatch.Draw(empty, new Rectangle(0, height * nodelenghts, width * nodelenghts, 2), Color.Gray);
            spriteBatch.Draw(empty, new Rectangle(0, 0, 2, height * nodelenghts), Color.Gray);
            spriteBatch.Draw(empty, new Rectangle(width * nodelenghts - 2, 0, 2, height * nodelenghts), Color.Gray);

            node temp = snakehead.getClone();
            while (temp.prev != null)
            {
                spriteBatch.Draw(snakenode, new Rectangle(temp.x * nodelenghts, temp.y * nodelenghts, nodelenghts, nodelenghts), Color.ForestGreen);
                temp = temp.prev.getClone();
            }
            spriteBatch.Draw(snakenode, new Rectangle(temp.x * nodelenghts, temp.y * nodelenghts, nodelenghts, nodelenghts), Color.ForestGreen);

            spriteBatch.Draw(food, new Rectangle((int)foodcoords.X * nodelenghts , (int)foodcoords.Y * nodelenghts,nodelenghts,nodelenghts), Color.WhiteSmoke);

            spriteBatch.DrawString(this.Content.Load<SpriteFont>("SpriteFont1"), "Score : " + Score, new Vector2(20,height*nodelenghts + 10), Color.Black);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
