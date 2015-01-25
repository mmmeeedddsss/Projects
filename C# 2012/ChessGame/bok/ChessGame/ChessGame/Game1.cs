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

namespace ChessGame
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D empty;
        List<IPiece> pieces;
        List<IPiece> prevlist;
        string[,] table;
        string[,] prevtable;
        const int wh = 80;
        enum Poins { kral = 9999, vezir = 9, kale = 5, at = 3, fil = 3, piyon = 1 };

        Vector2 selected;
        bool isselected;
        bool ism1pressed;
        bool isaiplaying;
        string currplayer;
        public struct move
        {
            public Vector2 from;
            public Vector2 to;
            public bool iseated;
            public IPiece eatedpiece;
            public int index;
        };

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 640;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pieces = new List<IPiece>();
            prevlist = new List<IPiece>();
            prevtable = new string[8, 8];
            table = new string[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    table[i, j] = "";

            currplayer = "b";

            empty = this.Content.Load<Texture2D>("empty");

            pieces.Add(new Rock(this.Content.Load<Texture2D>("bkale"), "kale", "b", new Rectangle(0, 7, wh, wh)));
            table[0, 7] = "bkale";
            pieces.Add(new Knight(this.Content.Load<Texture2D>("bat"), "at", "b", new Rectangle(1, 7, wh, wh)));
            table[1, 7] = "bat";
            pieces.Add(new Bishop(this.Content.Load<Texture2D>("bfil"), "fil", "b", new Rectangle(2, 7, wh, wh)));
            table[2, 7] = "bfil";
            pieces.Add(new Queen(this.Content.Load<Texture2D>("bvezir"), "vezir", "b", new Rectangle(3, 7, wh, wh)));
            table[3, 7] = "bvezir";
            pieces.Add(new King(this.Content.Load<Texture2D>("bkral"), "kral", "b", new Rectangle(4, 7, wh, wh)));
            table[4, 7] = "bkral";
            pieces.Add(new Bishop(this.Content.Load<Texture2D>("bfil"), "fil", "b", new Rectangle(5, 7, wh, wh)));
            table[5, 7] = "bfil";
            pieces.Add(new Knight(this.Content.Load<Texture2D>("bat"), "at", "b", new Rectangle(6, 7, wh, wh)));
            table[6, 7] = "bat";
            pieces.Add(new Rock(this.Content.Load<Texture2D>("bkale"), "kale", "b", new Rectangle(7, 7, wh, wh)));
            table[7, 7] = "bkale";
            //-------------------------
            pieces.Add(new Rock(this.Content.Load<Texture2D>("skale"), "kale", "s", new Rectangle(0, 0, wh, wh)));
            table[0, 0] = "skale";
            pieces.Add(new Knight(this.Content.Load<Texture2D>("sat"), "at", "s", new Rectangle(1, 0, wh, wh)));
            table[1, 0] = "sat";
            pieces.Add(new Bishop(this.Content.Load<Texture2D>("sfil"), "fil", "s", new Rectangle(2, 0, wh, wh)));
            table[2, 0] = "sfil";
            pieces.Add(new Queen(this.Content.Load<Texture2D>("svezir"), "vezir", "s", new Rectangle(3, 0, wh, wh)));
            table[3, 0] = "svezir";
            pieces.Add(new King(this.Content.Load<Texture2D>("skral"), "kral", "s", new Rectangle(4, 0, wh, wh)));
            table[4, 0] = "skral";
            pieces.Add(new Bishop(this.Content.Load<Texture2D>("sfil"), "fil", "s", new Rectangle(5, 0, wh, wh)));
            table[5, 0] = "sfil";
            pieces.Add(new Knight(this.Content.Load<Texture2D>("sat"), "at", "s", new Rectangle(6, 0, wh, wh)));
            table[6, 0] = "sat";
            pieces.Add(new Rock(this.Content.Load<Texture2D>("skale"), "kale", "s", new Rectangle(7, 0, wh, wh)));
            table[7, 0] = "skale";

            for (int i = 0; i < 8; i++)
            {
                pieces.Add(new Pawn(this.Content.Load<Texture2D>("bpiyon"), "piyon", "b", new Rectangle(i, 6, wh, wh)));
                pieces.Add(new Pawn(this.Content.Load<Texture2D>("spiyon"), "piyon", "s", new Rectangle(i, 1, wh, wh)));
                table[i, 6] = "bpiyon";
                table[i, 1] = "spiyon";
            }
        }

        protected override void UnloadContent()
        {

        }

        private List<move> getMoves(IPiece p)
        {
            List<move> moves = new List<move>();
            for( int i=0; i<8; i++ )
                for (int j = 0; j < 8; j++)
                {
                    if( p.canPlayTo(new Vector2(i,j),table) )
                    {
                        move m = new move();
                        m.to = new Vector2(i, j);
                        m.from = new Vector2( p.rect.X, p.rect.Y );
                        if (table[i, j] != "") // bişey yedik
                        {
                            m.iseated = true;
                            m.index = getPieceid(new Vector2(i, j));
                            m.eatedpiece = pieces[m.index];
                        }
                        moves.Add(m);
                    }
                }
            return moves;
        }

        /*private int simulateMove(move m, IPiece p, int depth, int point)
        {
            doMove(m, p);
            string clr = depth % 2 == 0 ? "b" : "s";
            if(clr=="s")
            {
                if (ischeck(clr, findking(clr)))
                {
                    redo(m, p);
                    return -999;
                }

                if (m.iseated)
                {
                    point += returnPoint(m.eatedpiece.pieceName);
                }
                if (ischeck(clr == "s" ? "b" : "s", findking(clr == "s" ? "b" : "s")))
                {
                    point += 1;
                    if (ischeckmate(clr))
                    {
                        point += 999;
                    }
                }
                selectBestMove(depth - 1, point);
            }

            redo(m, p);
            return point;
        }*/

        move bestMove;

        private void call()
        {
            selectBestMove(4);
        }

        private void selectBestMove(int depth) // tek sayı derinlik = siyah
        {
            string clr = depth % 2 == 0 ? "s" : "b";
            int maxPoint = -99999;
            bestMove = new move();
            if (depth > 0)
                for (int i = 0; i < pieces.Count; i++)
                {
                    IPiece p = pieces[i];
                    if (p.color == clr)
                    {
                        List<move> moves = getMoves(p);
                        foreach (move m in moves)
                        {
                            doMove(m, p);
                            int currScore = alphabeta(depth-1,-99999,+99999) + ((m.iseated == true) ? returnPoint(m.eatedpiece.pieceName) : 0);
                            if (ischeck(clr,findking(clr))) // legal bi hamle değil ise
                            {
                                redo(m, p);
                                continue;
                            }
                            if( maxPoint < currScore ) // maxı güncelle
                            {
                                maxPoint = currScore;
                                bestMove = m;
                            }
                            redo(m, p);
                        }
                    }
                }
            doMove(bestMove, pieces[getPieceid(bestMove.from)]);
        }

        private int minimax( int depth )
        {
            string clr = depth % 2 == 1 ? "s" : "b";
            if (ischeckmate(clr))
            {
                return 999; // ?
            }
            if (depth == 0)
                return 0;
            int point = -9999;
            for (int i = 0; i < pieces.Count; i++)
            {
                IPiece p = pieces[i];
                if (p.color == clr)
                {
                    List<move> moves = getMoves(p);
                    foreach (move m in moves)
                    {
                        doMove(m, p);
                        if (ischeck(clr,findking(clr)))
                        {
                            redo(m, p);
                            break;
                        }
                        point = Math.Max(point, -(minimax(depth - 1) - ((m.iseated == true) ? returnPoint(m.eatedpiece.pieceName) : 0)));
                        redo(m, p);
                    }
                }
            }
            return point;
        }

        private int alphabeta( int depth, int a, int b )
        {
            string clr = depth % 2 == 0 ? "s" : "b";
            if (depth == 0)
                return 0;
            if (clr == "s") // ?
            {
                if (ischeckmate(clr))
                    return -999;
                for (int i = 0; i < pieces.Count; i++)
                {
                    IPiece p = pieces[i];
                    if (p.color == clr)
                    {
                        List<move> moves = getMoves(p);
                        foreach (move m in moves)
                        {
                            doMove(m, p);
                            if (ischeck(clr, findking(clr)))
                            {
                                redo(m, p);
                                continue;
                            }
                            a = Math.Max(a, alphabeta(depth - 1, a, b) + ((m.iseated == true) ? returnPoint(m.eatedpiece.pieceName) : 0));
                            if (b < a)
                            {
                                redo(m, p);
                                return a;
                            }
                            redo(m, p);
                        }
                    }
                }
                return a;
            }
            else
            {
                if (ischeckmate(clr))
                    return +999;
                for (int i = 0; i < pieces.Count; i++)
                {
                    IPiece p = pieces[i];
                    if (p.color == clr)
                    {
                        List<move> moves = getMoves(p);
                        foreach (move m in moves)
                        {
                            doMove(m, p);
                            if (ischeck(clr, findking(clr)))
                            {
                                redo(m, p);
                                continue;
                            }
                            b = Math.Min(b, alphabeta(depth - 1, a, b) - ((m.iseated == true) ? returnPoint(m.eatedpiece.pieceName) : 0));
                            if (b < a)
                            {
                                redo(m, p);
                                return a;
                            }
                            redo(m, p);
                        }
                    }
                }
                return b;
            }
            return -99999;
        }

        protected void doMove(move m, IPiece p)
        {
            if ( m.iseated ) // taş yemişiz
            {
                pieces.RemoveAt(m.index);
            }
            pieces[getPieceid(m.from)].rect = new Rectangle((int)m.to.X,(int)m.to.Y,wh,wh);
            table[(int)m.from.X, (int)m.from.Y] = "";
            table[(int)m.to.X, (int)m.to.Y] = p.toString(); 
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Released && ism1pressed == true)
            {
                ism1pressed = false;
            }
            if (currplayer == "s" && IsActive)
            {
                new System.Threading.Thread(call).Start();

                if (currplayer == "b")
                    currplayer = "s";
                else
                    currplayer = "b";
                if (ischeckmate(currplayer))
                {
                    if (ischeck(currplayer, findking(currplayer))) //mat
                    {
                        LoadContent();
                    }
                    else //pat
                    {
                        LoadContent();
                    }
                }
            }
            if (ms.LeftButton == ButtonState.Pressed && ism1pressed == false && IsActive) // selecting piece
            {
                ism1pressed = true;
                if (isselected == true) // Want to move
                {
                    isselected = false;
                    if (getPieceid(selected) == -1) // yok öyle bi taş
                    {
                        isselected = false;
                        ism1pressed = false;
                    }
                    else if (pieces[getPieceid(selected)].color != currplayer) // kendi taşını seç
                    {
                        isselected = false;
                        ism1pressed = false;
                    }
                    else if (pieces[getPieceid(selected)].canPlayTo(new Vector2(ms.X / wh, ms.Y / wh), table))
                    {
                        int index = getPieceid(selected);
                        move m = moveTo(pieces[index], new Vector2(ms.X / wh, ms.Y / wh));
                        if (ischeck(currplayer, findking(currplayer)))
                        {
                            redo(m, pieces[index]);
                        }
                        else
                        {
                            if (currplayer == "b")
                                currplayer = "s";
                            else
                                currplayer = "b";
                            if (ischeckmate(currplayer))
                            {
                                if (ischeck(currplayer, findking(currplayer))) //mat
                                {
                                    LoadContent();
                                }
                                else //pat
                                {
                                    LoadContent();
                                }
                            }
                        }
                    }
                }
                else // Want to select
                {
                    selected = new Vector2(ms.X / wh, ms.Y / wh);
                    isselected = true;
                }
            }

            base.Update(gameTime);
        }

        protected Object copylist(Object pieces)
        {
            List<IPiece> newlist = new List<IPiece>();
            //foreach (IPiece p in (List<IPiece>)pieces)
            //newlist.Add((IPiece) p.Clone() );
            List<IPiece> pices = (List<IPiece>)pieces;
            for (int i = 0; i < pices.Count; i++)
                newlist.Add((IPiece)pices[i].Clone());
            return (Object)newlist;
        }

        protected string[,] copytable(string[,] table)
        {
            string[,] newtable = new string[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    newtable[i, j] = table[i, j];
            return newtable;
        }

        protected void redo(move m, Object piecee)
        {
            IPiece p = (IPiece)piecee;
            // undo table changes
            table[(int)m.from.X, (int)m.from.Y] = p.toString();
            table[(int)m.to.X, (int)m.to.Y] = "";
            pieces[getPieceid(new Vector2(p.rect.X, p.rect.Y))].rect = new Rectangle((int)m.from.X, (int)m.from.Y, wh, wh);
            if (m.iseated == true)
            {
                table[(int)m.to.X, (int)m.to.Y] = m.eatedpiece.toString();
                pieces.Insert(m.index, m.eatedpiece);
            }
            //undo pieces changes
            return;
        }

        protected bool ischeck(string currcolor, Vector2 king) // colora şah çekiliyor mu
        {
            if (king == new Vector2(-1, -1))
                return true;
            foreach (IPiece p in pieces)
                if (p.color != currcolor)
                    if (p.canPlayTo(king, table))
                        return true;
            return false;
        }

        protected bool ischeckmate(string currcolor)
        {
            for (int x = 0; x < pieces.Count; x++)
            {
                IPiece p = pieces[x];
                if (p.color == currcolor)
                {
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            if (p.canPlayTo(new Vector2(i, j), table)) // seçili taş i,j ye oynayabiliyorsa
                            {
                                move m = moveTo(p, new Vector2(i, j)); // oynat
                                if (ischeck(currplayer, findking(currplayer))) // şah Çekiliyo,burdan bişey çıkmaz
                                {
                                    redo(m, p);
                                    continue;
                                }
                                else//mat olmamış, oynicak yeri var.
                                {
                                    redo(m, p);
                                    return false;
                                }
                            }
                        }
                }
            } // buraya geldiine göre mat olmuşuz.
            return true;
        } // color mat oldu mu

        protected int calcdengesizlik()
        {
            int x = 0;
            foreach (IPiece p in pieces)
                if (p.color == "s")
                    x += returnPoint(p.pieceName);
                else
                    x -= returnPoint(p.pieceName);
            return x;
        }

        protected int max(int x, int y)
        {
            return x > y ? x : y;
        }

        protected bool blackisloosing()
        {
            int b = 0, s = 0;
            foreach (IPiece p in pieces)
                if (p.color == "b")
                    b += returnPoint(p.pieceName);
                else
                    s += returnPoint(p.pieceName);
            return b > s ? true : false;
        }

        protected int returnPoint(string pieceName)
        {
            int p = -1;
            if (pieceName == "piyon") p = 1;
            else if (pieceName == "at") p = 3;
            else if (pieceName == "fil") p = 3;
            else if (pieceName == "kale") p = 5;
            else if (pieceName == "vezir") p = 9;
            else if (pieceName == "kral") p = 9999;
            return p;
        }

        protected Vector2 findking(string color)
        {
            foreach (IPiece p in pieces)
                if (p.color == color)
                    if (p.pieceName == "kral")
                        return new Vector2(p.rect.X, p.rect.Y);
            return new Vector2(-1, -1);
        }

        protected move moveTo(Object piecee, Vector2 to)
        {
            move m = new move();
            int index = -2; // -1 -> getpieceid cant found, -2 -> table[tox,toy] is empty
            IPiece p = (IPiece)piecee; // playing piece
            m.to = to;
            m.iseated = false;
            m.eatedpiece = null;
            m.from = new Vector2(p.rect.X, p.rect.Y);
            m.index = -1;
            if (table[(int)to.X, (int)to.Y] != "") // table[i,j] isnt empty, means eating a piece
            {
                m.iseated = true;  // add removed items into to move struct
                index = getPieceid(new Vector2(to.X, to.Y));
                if (index == -1)
                    return m;
                m.eatedpiece = pieces[index];
                pieces.RemoveAt(index);
                m.index = index;
            }
            table[p.rect.X, p.rect.Y] = ""; // changing table
            table[(int)to.X, (int)to.Y] = p.toString();
            pieces[getPieceid(new Vector2(p.rect.X, p.rect.Y))].rect = new Rectangle((int)to.X, (int)to.Y, wh, wh); //changing p. pieces coords

            return m;
        }

        protected int getPieceid(Vector2 coords)
        {
            for (int i = 0; i < pieces.Count; i++)
                if (pieces[i].rect.X == coords.X && pieces[i].rect.Y == coords.Y)
                    return i;
            return -1;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);

            spriteBatch.Begin();

            if (isselected)
            {
                IPiece p = pieces[getPieceid(new Vector2(selected.X, selected.Y))];
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        if (p.canPlayTo(new Vector2(i, j), table))
                            spriteBatch.Draw(empty, new Rectangle(i * wh, j * wh, wh, wh), Color.Blue);
                        else if ((i + j) % 2 != 0)
                            spriteBatch.Draw(empty, new Rectangle(i * wh, j * wh, wh, wh), Color.DarkOliveGreen);
                        else
                            spriteBatch.Draw(empty, new Rectangle(i * wh, j * wh, wh, wh), Color.WhiteSmoke);
                for (int i = 0; i < pieces.Count; i++)
                    if (p.canPlayTo(new Vector2(pieces[i].rect.X, pieces[i].rect.Y), table))
                        spriteBatch.Draw(pieces[i].tex, new Rectangle(pieces[i].rect.X * wh, pieces[i].rect.Y * wh , wh ,wh), Color.Blue);
                    else
                        pieces[i].Draw(spriteBatch, wh);
            }
            else
            {
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        if ((i + j) % 2 != 0)
                            spriteBatch.Draw(empty, new Rectangle(i * wh, j * wh, wh, wh), Color.DarkOliveGreen);
                        else
                            spriteBatch.Draw(empty, new Rectangle(i * wh, j * wh, wh, wh), Color.WhiteSmoke);


                for (int i = 0; i < pieces.Count; i++)
                    pieces[i].Draw(spriteBatch, wh);
            }
            if (isselected)
            {
                IPiece pi = pieces[getPieceid(selected)];
                    List<move> moves = getMoves(pi);
                    for (int i = 0; i < moves.Count;i++ )
                        spriteBatch.Draw(empty, new Rectangle((int)moves[i].to.X * wh, (int)moves[i].to.Y * wh, wh, wh), Color.Blue);   
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
