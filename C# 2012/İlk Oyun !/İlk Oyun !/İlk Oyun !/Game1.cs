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
using System.Collections;

namespace İlk_Oyun__
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Ball ball1;
        Sopa_xD sopa1;
        LinkedList<Cubuk> ll;
        Gun gun1;

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
            
            base.Initialize();
            //graphics.ToggleFullScreen();
            ll = new LinkedList<Cubuk>();
            gun1 = new Gun(this.Content.Load<Texture2D>("Images/Gun"), this.Content.Load<Texture2D>("Images/Bullet"));

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 16; j++)
                    ll.AddLast(new Cubuk(this.Content.Load<Texture2D>("Images/Cubuk"), new Vector2(j * 50+5, i * 30 + 10), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));

                    sopa1 = new Sopa_xD(this.Content.Load<Texture2D>("Images/Sopa"), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            ball1 = new Ball(this.Content.Load<Texture2D>("Images/Ball"), new Vector2(graphics.PreferredBackBufferWidth-50,300), new Vector2( 14 , -14 ), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
            this.sopa1.Update(gameTime);
            this.ball1.Update(gameTime);
            this.gun1.Update(gameTime,sopa1.position.X,sopa1.position.Y,sopa1.texture.Width );

            if (ball1.Position.Y + ball1.Texture.Height >= graphics.PreferredBackBufferHeight)
            {
                System.Windows.Forms.MessageBox.Show(" La Öldün Mal !", " Naparsın Aptal İşte :D");
                this.Exit();
            }

            if (ll.First == null)
            {
                System.Windows.Forms.MessageBox.Show("Kazandın Lan :D", "Zaaaa");
                this.Exit();
            }

            LinkedList<Cubuk> cikarilcaklar = new LinkedList<Cubuk>();
            foreach (Vector2 bullet in gun1.bullets)
            {
                foreach (Cubuk cbk in ll)
                {
                    if (bullet.X >= cbk.Position.X && bullet.X <= cbk.Position.X + cbk.Texture.Width)
                    {
                        if (bullet.Y + 36 >= cbk.Position.Y && bullet.Y <= cbk.Position.Y + cbk.Texture.Height)
                        {
                            cikarilcaklar.AddLast(cbk);
                            goto asd;
                        }
                    }
                }
            asd:
                continue;
            }



            foreach (Cubuk cbk in cikarilcaklar)
            {
                ll.Remove(cbk);
            }

            cikarilcaklar = new LinkedList<Cubuk>();
            foreach (Cubuk cbk in ll)
            {

                if (ball1.Position.X + ball1.Texture.Width >= cbk.Position.X && ball1.Position.X <= cbk.Position.X + cbk.Texture.Width)
                    if (ball1.Position.Y + ball1.Texture.Height >= cbk.Position.Y && ball1.Position.Y <= cbk.Position.Y + cbk.Texture.Height)
                    {
                        cikarilcaklar.AddLast(cbk);
                        ball1.Velocity.Y = -ball1.Velocity.Y;
                        goto xxx;
                    }
            }

            xxx:

            foreach (Cubuk cbk in cikarilcaklar)
            {
                ll.Remove(cbk);
            }
            
            if( ball1.Position.X + ball1.Texture.Width >= sopa1.position.X && ball1.Position.X <= sopa1.position.X + sopa1.texture.Width )
                if ( ball1.Position.Y + ball1.Texture.Height >= sopa1.position.Y && ball1.Position.Y + ball1.Texture.Height <= sopa1.position.Y + sopa1.texture.Height )
                {
                    if (ball1.Position.X + ball1.Texture.Width / 2 <= sopa1.position.X + sopa1.texture.Width / 2 + 20 && ball1.Position.X + ball1.Texture.Width / 2 >= sopa1.position.X + sopa1.texture.Width / 2 - 20)
                    {
                    }
                    else if (ball1.Position.X + ball1.Texture.Width / 2 < sopa1.position.X + sopa1.texture.Width / 2 - 20)
                    {
                        ball1.Velocity.X -= 3;
                    }
                    else
                    {
                        ball1.Velocity.X += 3;
                    }
                    ball1.Velocity.Y = -ball1.Velocity.Y;
                }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            ball1.Draw(spriteBatch);
            sopa1.Draw(spriteBatch);
            gun1.Draw(spriteBatch, sopa1.position.X, sopa1.position.Y, sopa1.texture.Width);
            foreach (Cubuk cbk in ll)
                cbk.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
