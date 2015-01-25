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

namespace Kadir_Savaşları
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D kadir,ucan_dog,bom;
        Rectangle kadir1rect, kadir2rect;
        SpriteFont font;
        int h = 10;
        bool gameover;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1; // 1000
            graphics.PreferredBackBufferHeight = 1; // 600
            IsMouseVisible = true;
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            kadir = Content.Load<Texture2D>("kadir");
            ucan_dog = Content.Load<Texture2D>("ucan_dog");
            bom = Content.Load<Texture2D>("bom");
            kadir1rect = new Rectangle(100,100,100,100);
            kadir2rect = new Rectangle(700,100,100,100);
            font = Content.Load<SpriteFont>("SpriteFont1");
            gameover = false;

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
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Up))
                kadir2rect.Y -= h;
            if (ks.IsKeyDown(Keys.Down))
                kadir2rect.Y += h;
            if (ks.IsKeyDown(Keys.Left))
                kadir2rect.X -= h;
            if (ks.IsKeyDown(Keys.Right))
                kadir2rect.X += h;
            if (ks.IsKeyDown(Keys.W))
                kadir1rect.Y -= h;
            if (ks.IsKeyDown(Keys.S))
                kadir1rect.Y += h;
            if (ks.IsKeyDown(Keys.A))
                kadir1rect.X -= h;
            if (ks.IsKeyDown(Keys.D))
                kadir1rect.X += h;

            if (gameover && ks.IsKeyDown(Keys.Space))
                LoadContent();
            if (kadir1rect.Intersects(kadir2rect))
                gameover = true;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);

            spriteBatch.Begin();

            if (gameover)
            {
                for (int i = 0; i < 100; i++)
                    for (int j = 0; j < 100; j++)
                        spriteBatch.Draw(bom, new Rectangle(100 * i, 100 * j, 100, 100), null, Color.WhiteSmoke, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                spriteBatch.Draw(ucan_dog, new Rectangle(300, 40, 400, 600), Color.WhiteSmoke);
                spriteBatch.DrawString(font, "..: !Kadir Kazandi! :..", new Vector2(300, 50), Color.Red);
                //spriteBatch.Draw(bom, new Rectangle(0, 0, 200, 200), Color.WhiteSmoke);
                //spriteBatch.Draw(bom, new Rectangle(800, 0, 200, 200), Color.WhiteSmoke);
            }
            else
            {
                for (int i = 0; i < 100; i++)
                    for (int j = 0; j < 100; j++)
                        spriteBatch.Draw(kadir, new Rectangle(100 * i, 100 * j, 100, 100), null, Color.WhiteSmoke, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                spriteBatch.Draw(kadir, kadir1rect, Color.WhiteSmoke);
                spriteBatch.Draw(kadir, kadir2rect, Color.WhiteSmoke);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
