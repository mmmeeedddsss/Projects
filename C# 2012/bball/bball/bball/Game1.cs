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

namespace bball
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D ball;
        List<Ball> balls;


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

            IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 1024;
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
            ball = Content.Load<Texture2D>("Ball");
            balls = new List<Ball>();

            balls.Add(new Ball(ball, new Vector2(900, 600), 800, 980, 0, 0.8f));

            balls.Add(new Ball(ball, new Vector2(800, 600), 1000, 980, -3, 0.8f));
            balls.Add(new Ball(ball, new Vector2(700, 600), 1000,  980, 2, 0.8f));

            balls.Add(new Ball(ball, new Vector2(0, 600), 1100, 980, 0, 0.5f));
            balls.Add(new Ball(ball, new Vector2(100, 600), 1100, 980, 0, 0.6f));
            balls.Add(new Ball(ball, new Vector2(200, 600), 1100, 980, 0, 0.7f));
            balls.Add(new Ball(ball, new Vector2(300, 600), 1100, 980, 0, 0.8f));
            balls.Add(new Ball(ball, new Vector2(400, 600), 1100, 980, 0, 0.9f));

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
        /// 

        int SaniyedeKareSayisi = 0;
        double FPS = 0;
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (gameTime.TotalGameTime.Milliseconds == 0)
            {
                FPS = SaniyedeKareSayisi;
                SaniyedeKareSayisi = 0;
            }

            if( gameTime.TotalGameTime.Seconds > 2 )
            foreach (Ball b in balls)
            {
                b.Update(gameTime, graphics);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                balls.Clear();
                balls.Add(new Ball(ball, new Vector2(900, 600), 800, 980, 0, 0.8f));

                balls.Add(new Ball(ball, new Vector2(800, 600), 1000, 980, -3, 0.8f));
                balls.Add(new Ball(ball, new Vector2(700, 600), 1000, 980, 2, 0.8f));

                balls.Add(new Ball(ball, new Vector2(0, 600), 1100, 980, 0, 0.5f));
                balls.Add(new Ball(ball, new Vector2(100, 600), 1100, 980, 0, 0.6f));
                balls.Add(new Ball(ball, new Vector2(200, 600), 1100, 980, 0, 0.7f));
                balls.Add(new Ball(ball, new Vector2(300, 600), 1100, 980, 0, 0.8f));
                balls.Add(new Ball(ball, new Vector2(400, 600), 1100, 980, 0, 0.9f));
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);

            spriteBatch.Begin(SpriteSortMode.FrontToBack,BlendState.AlphaBlend);

            foreach (Ball b in balls)
            {
                b.Draw(spriteBatch);
            }

            SaniyedeKareSayisi++;

            spriteBatch.DrawString(Content.Load<SpriteFont>("SpriteFont1"), "FPS : " + FPS.ToString(), new Vector2(900, 5), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
