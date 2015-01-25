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

namespace Chess
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        enum PieceVals { Bishop = 3, King = int.MaxValue, Knight = 3, Pawn = 1, Queen = 9, Rook = 5 };
        string[,] table;
        List<Info> info;
        List<Pawn> Pawns = new List<Pawn>();
        List<Queen> Queens;
        List<Knight> Knights;
        List<Bishop> Bishops;
        List<King> Kings;
        List<Rook> Rooks;
        char[,] whitem;
        char[,] blackm;
        Object selected;
        King WhiteKing;
        King BlackKing;
        bool ismanaceok;
        bool isselected;
        bool isrealesed;
        string message;
        int x, y;
        bool isp1sturn;
        Info aibest;
        Vector2 aimove;

        public Game1()
        {
            IsMouseVisible = true;
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
            graphics.PreferredBackBufferHeight = 750;
            graphics.PreferredBackBufferWidth = 720;
            
            graphics.ApplyChanges();

            // TODO: use this.Content to load your game content here
            this.Content.Load<Texture2D>("Pieces/black_bishop");
            this.Content.Load<Texture2D>("Pieces/black_king");
            this.Content.Load<Texture2D>("Pieces/black_knight");
            this.Content.Load<Texture2D>("Pieces/black_pawn");
            this.Content.Load<Texture2D>("Pieces/black_queen");
            this.Content.Load<Texture2D>("Pieces/black_rook");
            this.Content.Load<Texture2D>("Pieces/white_bishop");
            this.Content.Load<Texture2D>("Pieces/white_king");
            this.Content.Load<Texture2D>("Pieces/white_queen");
            this.Content.Load<Texture2D>("Pieces/white_knight");
            this.Content.Load<Texture2D>("Pieces/white_pawn");
            this.Content.Load<Texture2D>("Pieces/white_rook");

            aibest = new Info("","",new Vector2());
            aimove = new Vector2();

            isselected = false;
            isp1sturn = true;

            table = new string[8, 8];
            whitem = new char[8, 8];
            blackm = new char[8, 8];
            info = new List<Info>();
            Pawns = new List<Pawn>();
            Queens = new List<Queen>();
            Knights = new List<Knight>();
            Bishops = new List<Bishop>();
            Kings = new List<King>();
            Rooks = new List<Rook>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    table[i, j] = " ";
                    whitem[i, j] = ' ';
                    blackm[i, j] = ' ';
                }
            }

            for (int i = 0; i < 8; i++)
            {
                table[i, 1] = "bpawn";
                table[i, 6] = "wpawn";

                Pawns.Add(new Pawn(this.Content.Load<Texture2D>("Pieces/black_pawn"), new Rectangle(90 * i, 90 * 1, 90, 90), "b", new Vector2(i, 1)));
                Pawns.Add(new Pawn(this.Content.Load<Texture2D>("Pieces/white_pawn"), new Rectangle(90 * i, 90 * 6, 90, 90), "w", new Vector2(i, 6)));
                info.Add(new Info("pawn", "b", new Vector2(i, 1)));
                info.Add(new Info("pawn", "w", new Vector2(i, 6)));
            }
            table[0, 0] = "brook";
            table[1, 0] = "bknight";
            table[2, 0] = "bbishop";
            table[3, 0] = "bqueen";
            table[4, 0] = "bking";
            table[5, 0] = "bbishop";
            table[6, 0] = "bknight";
            table[7, 0] = "brook";

            table[0, 7] = "wrook";
            table[1, 7] = "wknight";
            table[2, 7] = "wbishop";
            table[3, 7] = "wqueen";
            table[4, 7] = "wking";
            table[5, 7] = "wbishop";
            table[6, 7] = "wknight";
            table[7, 7] = "wrook";

            info.Add(new Info("rook", "b", new Vector2(0, 0)));
            Rooks.Add(new Rook(this.Content.Load<Texture2D>("Pieces/black_rook"), new Rectangle(90 * 0, 0, 90, 90), "b", new Vector2(0, 0)));
            info.Add(new Info("knight", "b", new Vector2(1, 0)));
            Knights.Add(new Knight(this.Content.Load<Texture2D>("Pieces/black_knight"), new Rectangle(90 * 1, 0, 90, 90), "b", new Vector2(1, 0)));
            info.Add(new Info("bishop", "b", new Vector2(2, 0)));
            Bishops.Add(new Bishop(this.Content.Load<Texture2D>("Pieces/black_bishop"), new Rectangle(90 * 2, 0, 90, 90), "b", new Vector2(2, 0)));
            info.Add(new Info("queen", "b", new Vector2(3, 0)));
            Queens.Add(new Queen(this.Content.Load<Texture2D>("Pieces/black_queen"), new Rectangle(90 * 3, 0, 90, 90), "b", new Vector2(3, 0)));
            info.Add(new Info("king", "b", new Vector2(4, 0)));
            Kings.Add(new King(this.Content.Load<Texture2D>("Pieces/black_king"), new Rectangle(90 * 4, 0, 90, 90), "b", new Vector2(4, 0)));
            BlackKing = new King(this.Content.Load<Texture2D>("Pieces/black_king"), new Rectangle(90 * 4, 0, 90, 90), "b", new Vector2(4, 0));
            info.Add(new Info("bishop", "b", new Vector2(5, 0)));
            Bishops.Add(new Bishop(this.Content.Load<Texture2D>("Pieces/black_bishop"), new Rectangle(90 * 5, 0, 90, 90), "b", new Vector2(5, 0)));
            info.Add(new Info("knight", "b", new Vector2(6, 0)));
            Knights.Add(new Knight(this.Content.Load<Texture2D>("Pieces/black_knight"), new Rectangle(90 * 6, 0, 90, 90), "b", new Vector2(6, 0)));
            info.Add(new Info("rook", "b", new Vector2(7, 0)));
            Rooks.Add(new Rook(this.Content.Load<Texture2D>("Pieces/black_rook"), new Rectangle(90 * 7, 0, 90, 90), "b", new Vector2(7, 0)));


            info.Add(new Info("rook", "w", new Vector2(0, 7)));
            Rooks.Add(new Rook(this.Content.Load<Texture2D>("Pieces/white_rook"), new Rectangle(90 * 0, 90 * 7, 90, 90), "w", new Vector2(0, 7)));
            info.Add(new Info("knight", "w", new Vector2(1, 7)));
            Knights.Add(new Knight(this.Content.Load<Texture2D>("Pieces/white_knight"), new Rectangle(90 * 1, 90 * 7, 90, 90), "w", new Vector2(1, 7)));
            info.Add(new Info("bishop", "w", new Vector2(2, 7)));
            Bishops.Add(new Bishop(this.Content.Load<Texture2D>("Pieces/white_bishop"), new Rectangle(90 * 2, 90 * 7, 90, 90), "w", new Vector2(2, 7)));
            info.Add(new Info("queen", "w", new Vector2(3, 7)));
            Queens.Add(new Queen(this.Content.Load<Texture2D>("Pieces/white_queen"), new Rectangle(90 * 3, 90 * 7, 90, 90), "w", new Vector2(3, 7)));
            info.Add(new Info("king", "w", new Vector2(4, 7)));
            Kings.Add(new King(this.Content.Load<Texture2D>("Pieces/white_king"), new Rectangle(90 * 4, 90 * 7, 90, 90), "w", new Vector2(4, 7)));
            WhiteKing = new King(this.Content.Load<Texture2D>("Pieces/white_king"), new Rectangle(90 * 4, 90 * 7, 90, 90), "w", new Vector2(4, 7));
            info.Add(new Info("bishop", "w", new Vector2(5, 7)));
            Bishops.Add(new Bishop(this.Content.Load<Texture2D>("Pieces/white_bishop"), new Rectangle(90 * 5, 90 * 7, 90, 90), "w", new Vector2(5, 7)));
            info.Add(new Info("knight", "w", new Vector2(6, 7)));
            Knights.Add(new Knight(this.Content.Load<Texture2D>("Pieces/white_knight"), new Rectangle(90 * 6, 90 * 7, 90, 90), "w", new Vector2(6, 7)));
            info.Add(new Info("rook", "w", new Vector2(7, 7)));
            Rooks.Add(new Rook(this.Content.Load<Texture2D>("Pieces/white_rook"), new Rectangle(90 * 7, 90 * 7, 90, 90), "w", new Vector2(7, 7)));
            
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
        protected void delete(int x,int y)
        {
            for (int i = 0; i < Pawns.Count;i++ )
            {
                if (Pawns.ElementAt<Pawn>(i).curpos == new Vector2(x, y))
                {
                    Pawns.RemoveAt(i);
                }
            }
            for (int i = 0; i < Rooks.Count; i++)
            {
                if (Rooks.ElementAt<Rook>(i).curpos == new Vector2(x, y))
                {
                    Rooks.RemoveAt(i);
                }
            }
            for (int i = 0; i < Bishops.Count; i++)
            {
                if (Bishops.ElementAt<Bishop>(i).curpos == new Vector2(x, y))
                {
                    Bishops.RemoveAt(i);
                }
            }
            for (int i = 0; i < Knights.Count; i++)
            {
                if (Knights.ElementAt<Knight>(i).curpos == new Vector2(x, y))
                {
                    Knights.RemoveAt(i);
                }
            }
            for (int i = 0; i < Queens.Count; i++)
            {
                if (Queens.ElementAt<Queen>(i).curpos == new Vector2(x, y))
                {
                    Queens.RemoveAt(i);
                }
            }
            for (int i = 0; i < Kings.Count; i++)
            {
                if (Kings.ElementAt<King>(i).curpos == new Vector2(x, y))
                {
                    Kings.RemoveAt(i);
                }
            }
            table[x, y] = " ";
            for (int i = 0; i < info.Count; i++)
                if (info[i].pos == new Vector2(x, y))
                {
                    info.RemoveAt(i);
                    break;
                }
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            isp1sturn = true;
            if (isp1sturn == true)
            {
                MouseState ms = Mouse.GetState();
                if (ms.LeftButton == ButtonState.Released && isrealesed == false)
                {
                    isrealesed = true;
                }
                if (ms.LeftButton == ButtonState.Pressed && ms.X > 0 && ms.Y > 0 && ms.X < 720 && ms.Y < 720 && isrealesed == true && isselected == false)
                {
                    x = (ms.X) / 90;
                    y = (ms.Y) / 90;
                    foreach (Info i in info)
                    {
                        if (i.pos == new Vector2(x, y))
                        {
                            isselected = true;
                            isrealesed = false;
                        }
                    }
                    //System.Windows.Forms.MessageBox.Show("1");
                }
                else if (ms.LeftButton == ButtonState.Pressed && ms.X > 0 && ms.Y > 0 && ms.X < 720 && ms.Y < 720 && isrealesed == true && isselected == true)
                {
                    isselected = false;
                    isrealesed = false;
                    //System.Windows.Forms.MessageBox.Show("2");
                    play();
                    //check("b",table);
                    //check("w",table);
                    //BlackKing.ischeckmate(whitem,table);
                    //BlackKing.ischeck(whitem,table);
                    //isp1sturn = false;
                }
                // End Of p1s Turn 
            }
               /*
            else  // Start Of Comp. Turn 
            {
                aipickbestmove(20);
                if( aibest.piecename == "pawn" )
                    foreach (Pawn p in Pawns)
                    {
                        if (p.curpos == aibest.pos)
                        {
                            p.curpos = aimove;
                            p.rec = new Rectangle(90 * ((int)aibest.pos.X), 90 * ((int)aibest.pos.Y), 90, 90);
                        }
                    }
                else if (aibest.piecename == "rook")
                    foreach (Rook p in Rooks)
                    {
                        if (p.curpos == aibest.pos)
                        {
                            p.curpos = aimove;
                            p.rec = new Rectangle(90 * ((int)aibest.pos.X), 90 * ((int)aibest.pos.Y), 90, 90);
                        }
                    }
                else if (aibest.piecename == "knight")
                    foreach (Knight p in Knights)
                    {
                        if (p.curpos == aibest.pos)
                        {
                            p.curpos = aimove;
                            p.rec = new Rectangle(90 * ((int)aibest.pos.X), 90 * ((int)aibest.pos.Y), 90, 90);
                        }
                    }
                else if (aibest.piecename == "bishop")
                    foreach (Bishop p in Bishops)
                    {
                        if (p.curpos == aibest.pos)
                        {
                            p.curpos = aimove;
                            p.rec = new Rectangle(90 * ((int)aibest.pos.X), 90 * ((int)aibest.pos.Y), 90, 90);
                        }
                    }
                else if (aibest.piecename == "queen")
                    foreach (Queen p in Queens)
                    {
                        if (p.curpos == aibest.pos)
                        {
                            p.curpos = aimove;
                            p.rec = new Rectangle(90 * ((int)aibest.pos.X), 90 * ((int)aibest.pos.Y), 90, 90);
                        }
                    }
                else if (aibest.piecename == "king")
                    foreach (King p in Kings)
                    {
                        if (p.curpos == aibest.pos)
                        {
                            p.curpos = aimove;
                            p.rec = new Rectangle(90 * ((int)aibest.pos.X), 90 * ((int)aibest.pos.Y), 90, 90);
                        }
                    }

                table[(int)aibest.pos.X, (int)aibest.pos.Y] = " ";
                foreach (Info i in info)
                {
                    if (i.Equals(aibest))
                        i.pos = aimove;
                }
                table[(int)aimove.X, (int)aimove.Y] = aibest.color+aibest.piecename;
                isp1sturn = true;
                isp1sturn = true;
            }
                * */
            base.Update(gameTime);
        }

        protected void aipickbestmove(int step)
        {
            int max = -1000;
            foreach (Knight p in Knights)
            {
                if (p.color == "b")
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            if (p.canplay(new Vector2(i, j), table, info, whitem) == true)
                            {
                                int a = aipickbestmovestep(step - 1, table, info);
                                if (max < a)
                                {
                                    max = a;
                                    aibest = new Info("knight", "b", p.curpos);
                                    aimove = new Vector2(i, j);
                                }
                            }
                        }
            }
            foreach (Pawn p in Pawns)
            {
                if( p.color == "b" )
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            if( p.canplay(new Vector2(i,j),table,info,whitem) == true )
                            {
                                int a = aipickbestmovestep(step - 1, table, info);
                                if (max < a)
                                {
                                    max = a;
                                    aibest = new Info("pawn","b",p.curpos);
                                    aimove = new Vector2(i,j);
                                }
                            }
                        }
            }
            foreach (Rook p in Rooks)
            {
                if (p.color == "b")
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            if (p.canplay(new Vector2(i, j), table, info, whitem) == true)
                            {
                                int a = aipickbestmovestep(step - 1, table, info);
                                if (max < a)
                                {
                                    max = a;
                                    aibest = new Info("rook", "b", p.curpos);
                                    aimove = new Vector2(i, j);
                                }
                            }
                        }
            }
            foreach (Bishop p in Bishops)
            {
                if (p.color == "b")
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            if (p.canplay(new Vector2(i, j), table, info, whitem) == true)
                            {
                                int a = aipickbestmovestep(step - 1, table, info);
                                if (max < a)
                                {
                                    max = a;
                                    aibest = new Info("bishop", "b", p.curpos);
                                    aimove = new Vector2(i, j);
                                }
                            }
                        }
            }
            foreach (Queen p in Queens)
            {
                if (p.color == "b")
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            if (p.canplay(new Vector2(i, j), table, info, whitem) == true)
                            {
                                int a = aipickbestmovestep(step - 1, table, info);
                                if (max < a)
                                {
                                    max = a;
                                    aibest = new Info("queen", "b", p.curpos);
                                    aimove = new Vector2(i, j);
                                }
                            }
                        }
            }
            foreach (King p in Kings)
            {
                if (p.color == "b")
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            if (p.canplay(new Vector2(i, j), table, info, whitem) == true)
                            {
                                int a = aipickbestmovestep(step - 1, table, info);
                                if (max < a)
                                {
                                    max = a;
                                    aibest = new Info("king", "b", p.curpos);
                                    aimove = new Vector2(i, j);
                                }
                            }
                        }
            }
        }

        protected int aipickbestmovestep(int currstep,string[,] currtable,List<Info> currinfo)
        {
            if (currstep == 0)
                return 0;
            int max=-1000;
            string color, enemcolor;
            if (currstep % 2 == 0) { color = "w"; enemcolor = "b"; }
            else { color = "b"; enemcolor = "w"; }

            for (int x = 0; x < currinfo.Count;x++ ) //  Every single piece in info
            {
                Info inf = currinfo.ElementAt<Info>(x);
                if (inf.color == color)// if this piece belongs player "color"
                {
                    string piecename = inf.piecename;
                    if (piecename == "pawn")
                    {
                        Pawn temp = new Pawn(null, new Rectangle(), "color", new Vector2());
                        for (int i = 0; i < 8; i++)
                            for (int j = 0; j < 8; j++)
                                if (temp.canplay(new Vector2(i, j), currtable, currinfo, whitem) == true) // if inf piece can move i,j
                                {
                                    List<Info> tinfo = currinfo; // new info when piece plays
                                    string[,] ttable = new string[8, 8]; // new table " "
                                    ttable[(int)inf.pos.X, (int)inf.pos.Y] = " ";
                                    ttable[i, j] = color + piecename;
                                    if (currtable[i, j] != " ") // if eats init table and info list
                                    {
                                        for (int u = 0; u < tinfo.Count; u++)
                                        {
                                            if (tinfo.ElementAt<Info>(u).pos == new Vector2(i, j))
                                            {
                                                tinfo.RemoveAt(u);
                                            }
                                        }
                                    } // end of if eats
                                    foreach (Info infoo in tinfo)
                                        if (infoo.pos == inf.pos)
                                            infoo.pos = new Vector2(i, j);
                                    // Start Calc. Points
                                    int point = 0;
                                    check("b",ttable);
                                    check("w",ttable);
                                    foreach (Info infofK in tinfo) // Find Kings
                                    {
                                        if (infofK.piecename == "king" && infofK.color == color)
                                        {
                                            King color_k = new King(null, new Rectangle(), color,infofK.pos);
                                            if (color_k.ischeckmate(check(color,ttable), ttable)) point += 9999999;
                                            if (color_k.ischeck(check(color,ttable), ttable) == true) point += 1;
                                        }
                                        else if (infofK.piecename == "king" && infofK.color == enemcolor)
                                        {
                                            King color_k = new King(null, new Rectangle(), color, infofK.pos);
                                            if (color_k.ischeckmate(check(color,ttable), ttable)) point += 9999999;
                                            if (color_k.ischeck(check(color,ttable), ttable) == true) point += 1;
                                        }
                                    } // End Of Find Kings
                                    point += aipickbestmovestep(currstep - 1, ttable, tinfo);
                                    if (currtable[i, j] != " ") // if piece Eats A Piece
                                        if(color == "b") point += 2*valof(piecename);
                                        else point -= 2*valof(piecename);
                                    if (max < point)
                                        max = point;
                                    // end of point calc.

                                } //end of if inf can move i,j
                    }
                    if (piecename == "rook")
                    {
                        Rook temp = new Rook(null, new Rectangle(), "color", new Vector2());
                        for (int i = 0; i < 8; i++)
                            for (int j = 0; j < 8; j++)
                                if (temp.canplay(new Vector2(i, j), currtable, currinfo, whitem) == true) // if inf piece can move i,j
                                {
                                    List<Info> tinfo = currinfo; // new info when piece plays
                                    string[,] ttable = new string[8, 8]; // new table " "
                                    ttable[(int)inf.pos.X, (int)inf.pos.Y] = " ";
                                    ttable[i, j] = color + piecename;
                                    if (currtable[i, j] != " ") // if eats init table and info list
                                    {
                                        for (int u = 0; u < tinfo.Count; u++)
                                        {
                                            if (tinfo.ElementAt<Info>(u).pos == new Vector2(i, j))
                                            {
                                                tinfo.RemoveAt(u);
                                            }
                                        }
                                    } // end of if eats
                                    foreach (Info infoo in tinfo)
                                        if (infoo.pos == inf.pos)
                                            infoo.pos = new Vector2(i, j);
                                    // Start Calc. Points
                                    int point = 0;
                                    check("b", ttable);
                                    check("w", ttable);
                                    foreach (Info infofK in tinfo) // Find Kings
                                    {
                                        if (infofK.piecename == "king" && infofK.color == color)
                                        {
                                            King color_k = new King(null, new Rectangle(), color, infofK.pos);
                                            if (color_k.ischeckmate(check(color, ttable), ttable)) point += 9999999;
                                            if (color_k.ischeck(check(color, ttable), ttable) == true) point += 1;
                                        }
                                        else if (infofK.piecename == "king" && infofK.color == enemcolor)
                                        {
                                            King color_k = new King(null, new Rectangle(), color, infofK.pos);
                                            if (color_k.ischeckmate(check(color, ttable), ttable)) point += 9999999;
                                            if (color_k.ischeck(check(color, ttable), ttable) == true) point += 1;
                                        }
                                    } // End Of Find Kings
                                    point += aipickbestmovestep(currstep - 1, ttable, tinfo);
                                    if (currtable[i, j] != " ") // if piece Eats A Piece
                                        if (color == "b") point += 2 * valof(piecename);
                                        else point -= 2 * valof(piecename);
                                    if (max < point)
                                        max = point;
                                    // end of point calc.

                                } //end of if inf can move i,j
                    }
                    if (piecename == "knight")
                    {
                        Knight temp = new Knight(null, new Rectangle(), "color", new Vector2());
                        for (int i = 0; i < 8; i++)
                            for (int j = 0; j < 8; j++)
                                if (temp.canplay(new Vector2(i, j), currtable, currinfo, whitem) == true) // if inf piece can move i,j
                                {
                                    List<Info> tinfo = currinfo; // new info when piece plays
                                    string[,] ttable = new string[8, 8]; // new table " "
                                    ttable[(int)inf.pos.X, (int)inf.pos.Y] = " ";
                                    ttable[i, j] = color + piecename;
                                    if (currtable[i, j] != " ") // if eats init table and info list
                                    {
                                        for (int u = 0; u < tinfo.Count; u++)
                                        {
                                            if (tinfo.ElementAt<Info>(u).pos == new Vector2(i, j))
                                            {
                                                tinfo.RemoveAt(u);
                                            }
                                        }
                                    } // end of if eats
                                    foreach (Info infoo in tinfo)
                                        if (infoo.pos == inf.pos)
                                            infoo.pos = new Vector2(i, j);
                                    // Start Calc. Points
                                    int point = 0;
                                    check("b", ttable);
                                    check("w", ttable);
                                    foreach (Info infofK in tinfo) // Find Kings
                                    {
                                        if (infofK.piecename == "king" && infofK.color == color)
                                        {
                                            King color_k = new King(null, new Rectangle(), color, infofK.pos);
                                            if (color_k.ischeckmate(check(color, ttable), ttable)) point += 9999999;
                                            if (color_k.ischeck(check(color, ttable), ttable) == true) point += 1;
                                        }
                                        else if (infofK.piecename == "king" && infofK.color == enemcolor)
                                        {
                                            King color_k = new King(null, new Rectangle(), color, infofK.pos);
                                            if (color_k.ischeckmate(check(color, ttable), ttable)) point += 9999999;
                                            if (color_k.ischeck(check(color, ttable), ttable) == true) point += 1;
                                        }
                                    } // End Of Find Kings
                                    point += aipickbestmovestep(currstep - 1, ttable, tinfo);
                                    if (currtable[i, j] != " ") // if piece Eats A Piece
                                        if (color == "b") point += 2 * valof(piecename);
                                        else point -= 2 * valof(piecename);
                                    if (max < point)
                                        max = point;
                                    // end of point calc.

                                } //end of if inf can move i,j
                    }
                    if (piecename == "bishop")
                    {
                        Bishop temp = new Bishop(null, new Rectangle(), "color", new Vector2());
                        for (int i = 0; i < 8; i++)
                            for (int j = 0; j < 8; j++)
                                if (temp.canplay(new Vector2(i, j), currtable, currinfo, whitem) == true) // if inf piece can move i,j
                                {
                                    List<Info> tinfo = currinfo; // new info when piece plays
                                    string[,] ttable = new string[8, 8]; // new table " "
                                    ttable[(int)inf.pos.X, (int)inf.pos.Y] = " ";
                                    ttable[i, j] = color + piecename;
                                    if (currtable[i, j] != " ") // if eats init table and info list
                                    {
                                        for (int u = 0; u < tinfo.Count; u++)
                                        {
                                            if (tinfo.ElementAt<Info>(u).pos == new Vector2(i, j))
                                            {
                                                tinfo.RemoveAt(u);
                                            }
                                        }
                                    } // end of if eats
                                    foreach (Info infoo in tinfo)
                                        if (infoo.pos == inf.pos)
                                            infoo.pos = new Vector2(i, j);
                                    // Start Calc. Points
                                    int point = 0;
                                    check("b", ttable);
                                    check("w", ttable);
                                    foreach (Info infofK in tinfo) // Find Kings
                                    {
                                        if (infofK.piecename == "king" && infofK.color == color)
                                        {
                                            King color_k = new King(null, new Rectangle(), color, infofK.pos);
                                            if (color_k.ischeckmate(check(color, ttable), ttable)) point += 9999999;
                                            if (color_k.ischeck(check(color, ttable), ttable) == true) point += 1;
                                        }
                                        else if (infofK.piecename == "king" && infofK.color == enemcolor)
                                        {
                                            King color_k = new King(null, new Rectangle(), color, infofK.pos);
                                            if (color_k.ischeckmate(check(color, ttable), ttable)) point += 9999999;
                                            if (color_k.ischeck(check(color, ttable), ttable) == true) point += 1;
                                        }
                                    } // End Of Find Kings
                                    point += aipickbestmovestep(currstep - 1, ttable, tinfo);
                                    if (currtable[i, j] != " ") // if piece Eats A Piece
                                        if (color == "b") point += 2 * valof(piecename);
                                        else point -= 2 * valof(piecename);
                                    if (max < point)
                                        max = point;
                                    // end of point calc.

                                } //end of if inf can move i,j
                    }
                    if (piecename == "queen")
                    {
                        Queen temp = new Queen(null, new Rectangle(), "color", new Vector2());
                        for (int i = 0; i < 8; i++)
                            for (int j = 0; j < 8; j++)
                                if (temp.canplay(new Vector2(i, j), currtable, currinfo, whitem) == true) // if inf piece can move i,j
                                {
                                    List<Info> tinfo = currinfo; // new info when piece plays
                                    string[,] ttable = new string[8, 8]; // new table " "
                                    ttable[(int)inf.pos.X, (int)inf.pos.Y] = " ";
                                    ttable[i, j] = color + piecename;
                                    if (currtable[i, j] != " ") // if eats init table and info list
                                    {
                                        for (int u = 0; u < tinfo.Count; u++)
                                        {
                                            if (tinfo.ElementAt<Info>(u).pos == new Vector2(i, j))
                                            {
                                                tinfo.RemoveAt(u);
                                            }
                                        }
                                    } // end of if eats
                                    foreach (Info infoo in tinfo)
                                        if (infoo.pos == inf.pos)
                                            infoo.pos = new Vector2(i, j);
                                    // Start Calc. Points
                                    int point = 0;
                                    check("b", ttable);
                                    check("w", ttable);
                                    foreach (Info infofK in tinfo) // Find Kings
                                    {
                                        if (infofK.piecename == "king" && infofK.color == color)
                                        {
                                            King color_k = new King(null, new Rectangle(), color, infofK.pos);
                                            if (color_k.ischeckmate(check(color, ttable), ttable)) point += 9999999;
                                            if (color_k.ischeck(check(color, ttable), ttable) == true) point += 1;
                                        }
                                        else if (infofK.piecename == "king" && infofK.color == enemcolor)
                                        {
                                            King color_k = new King(null, new Rectangle(), color, infofK.pos);
                                            if (color_k.ischeckmate(check(color, ttable), ttable)) point += 9999999;
                                            if (color_k.ischeck(check(color, ttable), ttable) == true) point += 1;
                                        }
                                    } // End Of Find Kings
                                    point += aipickbestmovestep(currstep - 1, ttable, tinfo);
                                    if (currtable[i, j] != " ") // if piece Eats A Piece
                                        if (color == "b") point += 2 * valof(piecename);
                                        else point -= 2 * valof(piecename);
                                    if (max < point)
                                        max = point;
                                    // end of point calc.

                                } //end of if inf can move i,j
                    }
                    if (piecename == "king")
                    {
                        King temp = new King(null, new Rectangle(), "color", new Vector2());
                        for (int i = 0; i < 8; i++)
                            for (int j = 0; j < 8; j++)
                                if (temp.canplay(new Vector2(i, j), currtable, currinfo, whitem) == true) // if inf piece can move i,j
                                {
                                    List<Info> tinfo = currinfo; // new info when piece plays
                                    string[,] ttable = new string[8, 8]; // new table " "
                                    ttable[(int)inf.pos.X, (int)inf.pos.Y] = " ";
                                    ttable[i, j] = color + piecename;
                                    if (currtable[i, j] != " ") // if eats init table and info list
                                    {
                                        for (int u = 0; u < tinfo.Count; u++)
                                        {
                                            if (tinfo.ElementAt<Info>(u).pos == new Vector2(i, j))
                                            {
                                                tinfo.RemoveAt(u);
                                            }
                                        }
                                    } // end of if eats
                                    foreach (Info infoo in tinfo)
                                        if (infoo.pos == inf.pos)
                                            infoo.pos = new Vector2(i, j);
                                    // Start Calc. Points
                                    int point = 0;
                                    check("b", ttable);
                                    check("w", ttable);
                                    foreach (Info infofK in tinfo) // Find Kings
                                    {
                                        if (infofK.piecename == "king" && infofK.color == color)
                                        {
                                            King color_k = new King(null, new Rectangle(), color, infofK.pos);
                                            if (color_k.ischeckmate(check(color, ttable), ttable)) point += 9999999;
                                            if (color_k.ischeck(check(color, ttable), ttable) == true) point += 1;
                                        }
                                        else if (infofK.piecename == "king" && infofK.color == enemcolor)
                                        {
                                            King color_k = new King(null, new Rectangle(), color, infofK.pos);
                                            if (color_k.ischeckmate(check(color, ttable), ttable)) point += 9999999;
                                            if (color_k.ischeck(check(color, ttable), ttable) == true) point += 1;
                                        }
                                    } // End Of Find Kings
                                    point += aipickbestmovestep(currstep - 1, ttable, tinfo);
                                    if (currtable[i, j] != " ") // if piece Eats A Piece
                                        if (color == "b") point += 2 * valof(piecename);
                                        else point -= 2 * valof(piecename);
                                    if (max < point)
                                        max = point;
                                    // end of point calc.

                                } //end of if inf can move i,j
                    }
                }
            }
            return max;
        }

        protected int valof(string s)
        {
            if (s == "pawn") return (int)PieceVals.Pawn;
            else if (s == "rook") return (int)PieceVals.Rook;
            else if (s == "knight") return (int)PieceVals.Knight;
            else if (s == "bishop") return (int)PieceVals.Bishop;
            else if (s == "queen") return (int)PieceVals.Queen;
            else if (s == "king") return (int)PieceVals.King;
            return 0;
        }

        protected void play()
        {
            MouseState ms = Mouse.GetState();
            int newx, newy;
                    newx = (ms.X) / 90;
                    newy = (ms.Y) / 90;
                    for (int s = 0; s < Pawns.Count;s++ )
                    {
                        Pawn p = Pawns[s];
                        if (p.curpos == new Vector2(x, y))
                        {
                            if (p.canplay(new Vector2(newx, newy), table, info, blackm) == true)
                            {
                                string[,] ntable = table;
                                List<Info> ninfo = info;
                                List<Pawn> npawns = Pawns;
                                ntable[x, y] = " ";
                                ntable[newx, newy] = "wpawn";
                                p.curpos = new Vector2(newx, newy);
                                p.rec = new Rectangle(90 * newx, 90 * newy, 90, 90);
                                for (int i = 0; i < info.Count; i++)
                                {
                                    if (ninfo.ElementAt<Info>(i).pos == new Vector2(x, y))
                                    {
                                        ninfo.ElementAt<Info>(i).pos = new Vector2(newx, newy);

                                        if (WhiteKing.ischeck(check("w", table), ntable) == true)
                                        {
                                            Pawns = npawns;
                                            isp1sturn = true;
                                        }
                                        else
                                        {
                                            foreach (Info inf in info)
                                            {
                                                if (inf.pos == new Vector2(newx, newy) && inf.color != "w")
                                                {
                                                    delete(newx, newy);
                                                    break;
                                                }
                                            }
                                            table = ntable;
                                            info = ninfo;
                                            table[newx, newy] = "wpawn";
                                            p.curpos = new Vector2(newx, newy);
                                            p.rec = new Rectangle(90 * newx, 90 * newy, 90, 90);
                                        }
                                        goto end;
                                    }
                                }
                            }
                        }
                    }
                    for (int s = 0; s < Rooks.Count; s++)
                    {
                        Rook p = Rooks[s];
                        if (p.curpos == new Vector2(x, y))
                        {
                            if (p.canplay(new Vector2(newx, newy), table, info, blackm) == true)
                            {
                                string[,] ntable = table;
                                List<Info> ninfo = info;
                                List<Rook> nRooks = Rooks;
                                ntable[x, y] = " ";
                                ntable[newx, newy] = "wpawn";
                                p.curpos = new Vector2(newx, newy);
                                p.rec = new Rectangle(90 * newx, 90 * newy, 90, 90);
                                for (int i = 0; i < info.Count; i++)
                                {
                                    if (ninfo.ElementAt<Info>(i).pos == new Vector2(x, y))
                                    {
                                        ninfo.ElementAt<Info>(i).pos = new Vector2(newx, newy);

                                        if (WhiteKing.ischeck(check("w", table), ntable) == true)
                                        {
                                            Rooks = nRooks;
                                            isp1sturn = true;
                                        }
                                        else
                                        {
                                            foreach (Info inf in info)
                                            {
                                                if (inf.pos == new Vector2(newx, newy) && inf.color != "w")
                                                {
                                                    delete(newx, newy);
                                                    break;
                                                }
                                            }
                                            table = ntable;
                                            info = ninfo;
                                            table[newx, newy] = "wrook";
                                            p.curpos = new Vector2(newx, newy);
                                            p.rec = new Rectangle(90 * newx, 90 * newy, 90, 90);
                                        }
                                        goto end;
                                    }
                                }
                            }
                        }
                    }
                    for (int s = 0; s < Knights.Count; s++)
                    {
                        Knight p = Knights[s];
                        if (p.curpos == new Vector2(x, y))
                        {
                            if (p.canplay(new Vector2(newx, newy), table, info, blackm) == true)
                            {
                                string[,] ntable = table;
                                List<Info> ninfo = info;
                                List<Knight> nknights = Knights;
                                ntable[x, y] = " ";
                                ntable[newx, newy] = "wknight";
                                p.curpos = new Vector2(newx, newy);
                                p.rec = new Rectangle(90 * newx, 90 * newy, 90, 90);
                                for (int i = 0; i < info.Count; i++)
                                {
                                    if (ninfo.ElementAt<Info>(i).pos == new Vector2(x, y))
                                    {
                                        ninfo.ElementAt<Info>(i).pos = new Vector2(newx, newy);

                                        if (WhiteKing.ischeck(check("w", table), ntable) == true)
                                        {
                                            Knights = nknights;
                                            isp1sturn = true;
                                        }
                                        else
                                        {
                                            foreach (Info inf in info)
                                            {
                                                if (inf.pos == new Vector2(newx, newy) && inf.color != "w")
                                                {
                                                    delete(newx, newy);
                                                    break;
                                                }
                                            }
                                            table = ntable;
                                            info = ninfo;
                                            table[newx, newy] = "wknight";
                                            p.curpos = new Vector2(newx, newy);
                                            p.rec = new Rectangle(90 * newx, 90 * newy, 90, 90);
                                        }
                                        goto end;
                                    }
                                }
                            }
                        }
                    }
                    for (int s = 0; s < Bishops.Count; s++)
                    {
                        Bishop p = Bishops[s];
                        if (p.curpos == new Vector2(x, y))
                        {
                            if (p.canplay(new Vector2(newx, newy), table, info, blackm) == true)
                            {
                                string[,] ntable = table;
                                List<Info> ninfo = info;
                                List<Bishop> nbishops = Bishops;
                                ntable[x, y] = " ";
                                ntable[newx, newy] = "wbishop";
                                p.curpos = new Vector2(newx, newy);
                                p.rec = new Rectangle(90 * newx, 90 * newy, 90, 90);
                                for (int i = 0; i < info.Count; i++)
                                {
                                    if (ninfo.ElementAt<Info>(i).pos == new Vector2(x, y))
                                    {
                                        ninfo.ElementAt<Info>(i).pos = new Vector2(newx, newy);
                                        if (WhiteKing.ischeck(check("w", table), ntable) == true)
                                        {
                                            Bishops = nbishops;
                                            isp1sturn = true;
                                        }
                                        else
                                        {
                                            foreach (Info inf in info)
                                            {
                                                if (inf.pos == new Vector2(newx, newy) && inf.color != "w")
                                                {
                                                    delete(newx, newy);
                                                    break;
                                                }
                                            }
                                            table = ntable;
                                            info = ninfo;
                                            table[newx, newy] = "wbishop";
                                            p.curpos = new Vector2(newx, newy);
                                            p.rec = new Rectangle(90 * newx, 90 * newy, 90, 90);
                                        }
                                        goto end;
                                    }
                                }
                            }
                        }
                    }
                    for (int s = 0; s < Queens.Count; s++)
                    {
                        Queen p = Queens[s];
                        if (p.curpos == new Vector2(x, y))
                        {
                            if (p.canplay(new Vector2(newx, newy), table, info, blackm) == true)
                            {
                                string[,] ntable = table;
                                List<Info> ninfo = info;
                                List<Queen> nqueens = Queens;
                                ntable[x, y] = " ";
                                ntable[newx, newy] = "wqueen";
                                p.curpos = new Vector2(newx, newy);
                                p.rec = new Rectangle(90 * newx, 90 * newy, 90, 90);
                                for (int i = 0; i < info.Count; i++)
                                {
                                    if (ninfo.ElementAt<Info>(i).pos == new Vector2(x, y))
                                    {
                                        ninfo.ElementAt<Info>(i).pos = new Vector2(newx, newy);
                                        if (WhiteKing.ischeck(check("w", table), ntable) == true)
                                        {
                                            Queens = nqueens;
                                            isp1sturn = true;
                                        }
                                        else
                                        {
                                            foreach (Info inf in info)
                                            {
                                                if (inf.pos == new Vector2(newx, newy) && inf.color != "w")
                                                {
                                                    delete(newx, newy);
                                                    break;
                                                }
                                            }
                                            table = ntable;
                                            info = ninfo;
                                            table[newx, newy] = "wqueen";
                                            p.curpos = new Vector2(newx, newy);
                                            p.rec = new Rectangle(90 * newx, 90 * newy, 90, 90);
                                        }
                                        goto end;
                                    }
                                }
                            }
                        }
                    }
                    for (int s = 0; s < Kings.Count; s++)
                    {
                        King p = Kings[s];
                        if (p.curpos == new Vector2(x, y))
                        {
                            if (p.canplay(new Vector2(newx, newy), table, info, blackm) == true)
                            {
                                string[,] ntable = table;
                                List<Info> ninfo = info;
                                List<King> nkings = Kings;
                                ntable[x, y] = " ";
                                ntable[newx, newy] = "wking";
                                p.curpos = new Vector2(newx, newy);
                                p.rec = new Rectangle(90 * newx, 90 * newy, 90, 90);
                                for (int i = 0; i < info.Count; i++)
                                {
                                    if (ninfo.ElementAt<Info>(i).pos == new Vector2(x, y))
                                    {
                                        ninfo.ElementAt<Info>(i).pos = new Vector2(newx, newy);
                                        if (WhiteKing.ischeck(check("w", table), ntable) == true)
                                        {
                                            Kings = nkings;
                                            isp1sturn = true;
                                        }
                                        else
                                        {
                                            foreach (Info inf in info)
                                            {
                                                if (inf.pos == new Vector2(newx, newy) && inf.color != "w")
                                                {
                                                    delete(newx, newy);
                                                    break;
                                                }
                                            }
                                            table = ntable;
                                            info = ninfo;
                                            table[newx, newy] = "wking";
                                            p.curpos = new Vector2(newx, newy);
                                            p.rec = new Rectangle(90 * newx, 90 * newy, 90, 90);
                                        }
                                        goto end;
                                    }
                                }
                            }
                        }
                    }
                end:
                    x = x;
        }
        
        protected char[,] check(string color,string[,] table )
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if(color == "w") whitem[i, j] = ' ';
                    if(color == "b") blackm[i, j] = ' ';
                }
            }
            foreach (Pawn p in Pawns)
            {
                if (p.color == "w")
                    p.manace_area(ref blackm, info, table);
                else
                    p.manace_area(ref whitem,info,table);
            }
            foreach (Rook p in Rooks)
            {
                if (p.color == "w")
                    p.manace_area(ref blackm, info, table);
                else
                    p.manace_area(ref whitem,info,table);
            }
            foreach (Knight p in Knights)
            {
                if (p.color == "w")
                    p.manace_area(ref blackm, info, table);
                else
                    p.manace_area(ref whitem,info,table);
            }
            foreach (Bishop p in Bishops)
            {
                if (p.color == "w")
                    p.manace_area(ref blackm, info, table);
                else
                    p.manace_area(ref whitem,info,table);
            }
            foreach (Queen p in Queens)
            {
                if (p.color == "w")
                    p.manace_area(ref blackm, info, table);
                else
                    p.manace_area(ref whitem,info,table);
            }
            foreach (King p in Kings)
            {
                if (p.color == "w")
                    p.manace_area(ref blackm, info, table);
                else
                    p.manace_area(ref whitem,info,table);
            }
            if (color == "w") return blackm;
            else return whitem;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if ((i + j) % 2 == 1)
                        spriteBatch.Draw(this.Content.Load<Texture2D>("Pieces/Empty"), new Rectangle(i * 90, j * 90, 90, 90), Color.WhiteSmoke);
                    else
                        spriteBatch.Draw(this.Content.Load<Texture2D>("Pieces/Empty"), new Rectangle(i * 90, j * 90, 90, 90), Color.DarkGreen);

            spriteBatch.End();

            foreach (Pawn p in Pawns)
            {
                p.draw(spriteBatch);
            }
            foreach (Rook p in Rooks)
            {
                p.draw(spriteBatch);
            }
            foreach (Queen p in Queens)
            {
                p.draw(spriteBatch);
            }
            foreach (King p in Kings)
            {
                p.draw(spriteBatch);
            }
            foreach (Knight p in Knights)
            {
                p.draw(spriteBatch);
            }
            foreach (Bishop p in Bishops)
            {
                p.draw(spriteBatch);
            }
            /*
            if (message != " ")
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(this.Content.Load<SpriteFont>("SpriteFont1"), "Cant Move", new Vector2(30, 720), Color.Black);
                spriteBatch.End();
                message = " ";
            }
             * */

            base.Draw(gameTime);

        }
    }
}
