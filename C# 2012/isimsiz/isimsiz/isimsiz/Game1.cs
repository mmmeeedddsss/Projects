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

namespace isimsiz
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Node> nodes;
        bool isselected, isrelased,isgameover;
        Node selectednode;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
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
            nodes = new List<Node>();
            /*
                nodes.Add(new Node(new Vector2(30, 75),new int[3]{ 1, 2, 4 },this.Content.Load<Texture2D>("Node"))); // 0
                nodes.Add(new Node(new Vector2(300, 100), new int[3] { 0, 3 ,4 }, this.Content.Load<Texture2D>("Node"))); // 1
                nodes.Add(new Node(new Vector2(700, 200), new int[3] { 0, 3, 4 }, this.Content.Load<Texture2D>("Node"))); // 2
                nodes.Add(new Node(new Vector2(230, 170), new int[2] { 1, 2 }, this.Content.Load<Texture2D>("Node"))); // 3
                nodes.Add(new Node(new Vector2(500, 20), new int[3] { 0, 1, 2 }, this.Content.Load<Texture2D>("Node"))); // 4
             */
            for (int i = 0; i < 7; i++)
            {
                int[] conns = new int[2];
                if (i != 6) conns[0] = i + 1;
                else conns[0] = 0;
                if(i % 2 == 0) conns[1] = Math.Abs(4 - i);
                int x, y;
                y = 200;
                if (i % 2 == 0) y += 70 + i*i*i;
                else y -= 70 + i*i*i;
                x = i * 90 + 100;
                nodes.Add(new Node(new Vector2(x,y), conns, this.Content.Load<Texture2D>("Node"))); // 0
            }
                isselected = false;
            isrelased = true;
            selectednode = null;
            isgameover = false;
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
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 1000;
            graphics.ApplyChanges();

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
            MouseState ms = Mouse.GetState();

            if (ms.LeftButton == ButtonState.Released && isrelased == false)
            {
                isrelased = true;
                isselected = false;
                if (isgameover == true)
                    System.Windows.Forms.MessageBox.Show("You Win In " + gameTime.TotalGameTime.Seconds + " sec !");
            }
            else if (ms.LeftButton == ButtonState.Pressed && isselected == false && isrelased == true )
            {
                isrelased = false;
                // seçiyoruz
                foreach (Node n in nodes)
                {
                    if( n.rec.Intersects(new Rectangle(ms.X,ms.Y,1,1)) )
                    {
                        selectednode = n;
                        isselected = true;
                        break;
                    }
                }
            }
            else if (ms.LeftButton == ButtonState.Pressed && isselected == true && isrelased == false )
            {
                selectednode.pos = new Vector2(ms.X, ms.Y);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            isgameover = true;
            foreach (Node n in nodes)
            {
                //eğer n ile başka bir m in doğrusu listedeki herhangi başka doğrudaysa kırmızı yap
                Vector2 i = n.pos; // i
                foreach (int m in n.conns) // her bağlantılı dğru için
                {
                    Vector2 j = nodes[m].pos; // j
                    for (int u = 0; u < nodes.Count;u++ )
                    {
                        Node t1 = nodes[u];
                        if (t1.Equals(nodes[m]) || t1.Equals(n)) continue;
                        Vector2 k = t1.pos; //k
                        for (int s = 0; s < nodes.Count; s++)
                        {
                            Node t2 = nodes[s];
                            if (t2.Equals(t1) || t2.Equals(nodes[m]) || t2.Equals(n)) continue;
                            Vector2 l = t2.pos; // l
                            /*
                            float a1,a2,b1,b2;
                            float y1 = i.Y,y2 = j.Y,y3 = k.Y,y4 = l.Y;
                            float x1 = i.X,x2 = j.X,x3 = k.X,x4 = l.X;
                            a1 = (y1 - y2) / (x1 - x2);
                            a2 = (y3 - y4) / (x3 - x4);
                            b1 = y1 - (a1 * x1);
                            b2 = y3 - (a2 * x3); // Doğru Denklemleri Hazır
                            float X,Y;
                            X = (b1 - b2) / (a2 - a1);
                            Y = (a1*X) + b1;
                            if ( X > Math.Min(i.X,j.X) && X < Math.Max(i.X,j.X) && Y > Math.Min(i.Y,j.Y) && Y < Math.Max(i.Y,j.Y) )
                            {
                                DrawLine(new Vector2(j.X + (n.rec.Width / 2), j.Y + (n.rec.Height / 2)), new Vector2(i.X + (nodes[m].rec.Width / 2), i.Y + (nodes[m].rec.Height / 2)), Color.Red);
                                spriteBatch.Draw(Content.Load<Texture2D>("Empty"), new Rectangle((int)i.X, (int)i.Y, 40, 40), Color.Blue);
                                spriteBatch.Draw(Content.Load<Texture2D>("Empty"), new Rectangle((int)j.X, (int)j.Y, 40, 40), Color.Yellow);
                                spriteBatch.Draw(Content.Load<Texture2D>("Empty"), new Rectangle((int)k.X, (int)k.Y, 40, 40), Color.Green);
                                spriteBatch.Draw(Content.Load<Texture2D>("Empty"), new Rectangle((int)l.X, (int)l.Y, 40, 40), Color.Pink);
                                goto ok;
                            }
                            */
                            if (t1.conns.Contains<int>(s) || t2.conns.Contains<int>(u))
                            {
                                Double d = (((j.X - i.X) * (k.Y - l.Y)) - ((k.X - l.X) * (j.Y - i.Y)));
                                if (d != 0) // parelel diilse
                                {
                                    Double p1, p2;
                                    p1 = ((k.X - l.X) * (i.Y - k.Y) - (i.X - k.X) * (k.Y - l.Y)) / d;
                                    p2 = ((i.X - k.X) * (j.Y - i.Y) - (j.X - i.X) * (i.Y - k.Y)) / d;
                                    if (p1 > 0 && p1 < 1 && p2 > 0 && p2 < 1 && !i.Equals(j) && !i.Equals(k) && !i.Equals(l) && !j.Equals(k) && !j.Equals(l) && !k.Equals(l))
                                    {
                                        //System.Windows.Forms.MessageBox.Show("p1 = " + p1 + "  p2 = " + p2 + " d = " + d);
                                        //  Kesişiyolar yani bağımız kırmızı olucak
                                        Double X, Y;
                                        X = i.X + p1 * (j.X - i.X);
                                        Y = i.Y + p2 * (j.Y - i.Y);
                                        if (X > Math.Min(i.X, j.X) && X < Math.Max(i.X, j.X) && Y > Math.Min(i.Y, j.Y) && Y < Math.Max(i.Y, j.Y))
                                        {
                                            foreach (Node nod in nodes)
                                                if (new Rectangle((int)X - 10, (int)Y - 10, 20, 20).Intersects(nod.rec))
                                                    continue;
                                            isgameover = false;
                                            //System.Windows.Forms.MessageBox.Show(x + " - " + y);
                                            DrawLine(new Vector2(i.X + (n.rec.Width / 2), i.Y + (n.rec.Height / 2)), new Vector2(j.X + (nodes[m].rec.Width / 2), j.Y + (nodes[m].rec.Height / 2)), Color.Red);
                                            /*
                                            spriteBatch.Draw(Content.Load<Texture2D>("Empty"), new Rectangle((int)i.X, (int)i.Y, 40, 40), Color.Blue);
                                            spriteBatch.Draw(Content.Load<Texture2D>("Empty"), new Rectangle((int)j.X, (int)j.Y, 40, 40), Color.Yellow);
                                            spriteBatch.Draw(Content.Load<Texture2D>("Empty"), new Rectangle((int)k.X, (int)k.Y, 40, 40), Color.Green);
                                            spriteBatch.Draw(Content.Load<Texture2D>("Empty"), new Rectangle((int)l.X, (int)l.Y, 40, 40), Color.Pink);
                                             * */
                                            goto ok;
                                        }
                                    }
                                }
                            }
                            //*/
                        }
                    }
                    DrawLine(new Vector2(j.X + (n.rec.Width / 2), j.Y + (n.rec.Height / 2)), new Vector2(i.X + (nodes[m].rec.Width / 2), i.Y + (nodes[m].rec.Height / 2)), Color.Black);
                ok:
                    continue;
                }
            }

            foreach (Node n in nodes)
                n.draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        void DrawLine(Vector2 a, Vector2 b, Color col)
        {

            Texture2D spr = this.Content.Load<Texture2D>("Empty");
            Vector2 Origin = new Vector2(0.5f, 0.0f);
            Vector2 diff = b - a;
            float angle;
            Vector2 Scale = new Vector2(1.0f, diff.Length() / spr.Height);

            angle = (float)(Math.Atan2(diff.Y, diff.X)) - MathHelper.PiOver2;

            spriteBatch.Draw(spr, a, null, col, angle, Origin, Scale, SpriteEffects.None, 1.0f);
        }
    }
}
