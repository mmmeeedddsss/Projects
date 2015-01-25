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
using System.Threading;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D empty;
        List<IPiece> pieces;
        List<move> randmoves;
        Random rand;
        string[,] table;
        string[,] prevtable;
        const int wh = 80;
        enum Poins { kral = 99999, vezir = 9, kale = 5, at = 3, fil = 3, piyon = 1 };

        Vector2 selected;
        bool isselected;
        bool ism1pressed;
        bool isaiplaying;
        bool aivs;
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

            rand = new Random();
            isaiplaying = false;

            System.Windows.Forms.DialogResult x = System.Windows.Forms.MessageBox.Show("body","title",System.Windows.Forms.MessageBoxButtons.YesNo);
            if (x == System.Windows.Forms.DialogResult.Yes)
                aivs = true;
            else aivs = false;
        }

        protected override void UnloadContent()
        {

        }

        private List<move> getMoves(IPiece p, string[,] currtable, List<IPiece> currpieces)
        {
            List<move> moves = new List<move>();
            for( int i=0; i<8; i++ )
                for (int j = 0; j < 8; j++)
                {
                    if( p.canPlayTo(new Vector2(i,j),currtable) )
                    {
                        move m = new move();
                        m.to = new Vector2(i, j);
                        m.from = new Vector2( p.rect.X, p.rect.Y );
                        if (currtable[i, j] != "") // bişey yedik
                        {
                            m.iseated = true;
                            m.index = getPieceid(new Vector2(i, j),currpieces);
                            m.eatedpiece = currpieces[m.index];
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


        private void call()
        {
            //selectBestMove(4,copytable(table),copylist(pieces));
            //selectBestMove(4, table, pieces);
            string clr = currplayer;
            isaiplaying = true;
            randmoves = new List<move>();
            selectBestMove(4,clr);
            isaiplaying = false;
        }

        //private void selectBestMove(int depth,string[,] currtable, List<IPiece> currpieces ) // tek sayı derinlik = siyah
        private void selectBestMove(int depth,string clr) // tek sayı derinlik = siyah
        {
            //string clr = depth % 2 == 0 ? "s" : "b";
            long maxPoint = -99999;
            if (depth > 0)
                //for(int i=0;i<pieces.Count;i++)
                Parallel.ForEach<IPiece>(pieces.AsEnumerable<IPiece>(), p1 =>
                {
                    //IPiece p = pieces[i];
                    if (p1.color == clr)
                    {
                        string[,] ct = table; // copytable(table);
                        List<IPiece> cp = pieces;//copylist(pieces);
                        IPiece p = cp[getPieceid(new Vector2(p1.rect.X, p1.rect.Y), cp)];
                        List<move> moves = getMoves(p, ct, cp);
                        //foreach (move m in moves)
                        Parallel.ForEach<move>(moves.AsEnumerable<move>(), m =>
                        {
                            List<IPiece> cpi = copylist(cp);
                            IPiece pi = cpi[getPieceid(new Vector2(p1.rect.X, p1.rect.Y), cpi)];
                            string[,] cti = copytable(ct);

                            doMove(m, pi, cti, cpi);
                            if (!ischeck(clr, findking(clr, cpi), cti, cpi)) // legal bi hamle ise
                            {
                                long currScore = (long)alphabeta(depth - 1,clr, -99999, +99999, cti, cpi) + ((m.iseated == true) ? returnPoint(m.eatedpiece.pieceName) : 0);
                                //long currScore = alphaBetaMax(-99999, 99999, clr, depth, cti, cpi);
                                //long currScore = alphaBeta(-99999, 99999, clr, depth - 1, cti, cpi);
                                if (maxPoint <= currScore)
                                {
                                    if (maxPoint < currScore)
                                        randmoves.Clear();
                                    randmoves.Add(m);
                                }
                                //redo(mi, pi, cti, cpi);
                            }
                        });
                    }
                });
            int index = rand.Next(randmoves.Count);
            doMove(randmoves[index], pieces[getPieceid(randmoves[index].from, pieces)], table, pieces);
        }

        private int minimax(int depth, string[,] currtable, List<IPiece> currpieces)
        {
            string clr = depth % 2 == 1 ? "s" : "b";
            if (ischeckmate(clr,currtable,currpieces))
            {
                return 999; // ?
            }
            if (depth == 0)
                return 0;
            int point = -9999;
            for (int i = 0; i < currpieces.Count; i++)
            {
                IPiece p = currpieces[i];
                if (p.color == clr)
                {
                    List<move> moves = getMoves(p,currtable,currpieces);
                    foreach (move m in moves)
                    {
                        doMove(m, p,currtable,currpieces);
                        if (ischeck(clr,findking(clr,currpieces),currtable,currpieces))
                        {
                            redo(m, p,currtable,currpieces);
                            break;
                        }
                        point = Math.Max(point, -(minimax(depth - 1,currtable,currpieces) - ((m.iseated == true) ? returnPoint(m.eatedpiece.pieceName) : 0)));
                        redo(m, p,currtable,currpieces);
                    }
                }
            }
            return point;
        }

        private int alphaBeta(int alpha, int beta, string clr, int depthleft, string[,] currtable, List<IPiece> currpieces)
        {
            int bestscore = -99999;
            clr = (clr == "b") ? "s" : "b";
            if (depthleft == 0)
            {
                return -evulate();
            }
            for (int i = 0; i < currpieces.Count; i++)
            {
                IPiece p = currpieces[i];
                if (p.color == clr)
                {
                    List<move> moves = getMoves(p, currtable, currpieces);
                    foreach (move m in moves)
                    {
                        doMove(m, p, currtable, currpieces);
                        if (ischeck(clr, findking(clr, currpieces), currtable, currpieces))
                        {
                            redo(m, p, currtable, currpieces);
                            continue;
                        }
                        int score = score = -alphaBeta(-beta, -alpha,clr, depthleft - 1,currtable,currpieces);
                        if (score >= beta)
                        {
                            redo(m, p, currtable, currpieces);
                            return score;  // fail-soft beta-cutoff
                        }
                        if (score > bestscore)
                        {
                            bestscore = score;
                            if (score > alpha)
                                alpha = score;
                        }
                        redo(m, p, currtable, currpieces);
                    }
                }
            }
            return bestscore;
        }

        private int alphaBetaMax(int a, int b,string clr ,int depthleft, string[,] currtable, List<IPiece> currpieces)
        {
            clr = (clr == "b") ? "s" : "b";
            if( ischeckmate(clr,currtable,currpieces) )
                return -999;
            if (depthleft == 0) return evulate();
            for (int i = 0; i < currpieces.Count; i++)
            {
                IPiece p = currpieces[i];
                if (p.color == clr)
                {
                    List<move> moves = getMoves(p, currtable, currpieces);
                    foreach (move m in moves)
                    {
                        doMove(m, p, currtable, currpieces);
                        if (ischeck(clr, findking(clr, currpieces), currtable, currpieces))
                        {
                            redo(m, p, currtable, currpieces);
                            continue;
                        }
                        int score = alphaBetaMin(a, b, clr, depthleft - 1, currtable, currpieces);
                        if (score >= b)
                        {
                            redo(m, p, currtable, currpieces);
                            return b;   // beta-cutoff
                        }
                        if (score > a)
                            a = score; // alpha acts like max in MiniMax
                        redo(m, p, currtable, currpieces);
                    }
                }
            }
            return a;
        }

        private int alphaBetaMin(int a, int b, string clr, int depthleft, string[,] currtable, List<IPiece> currpieces)
        {
            clr = (clr == "b") ? "s" : "b";
            if (ischeckmate(clr, currtable, currpieces))
                return +999;
            if (depthleft == 0) return -evulate();
            for (int i = 0; i < currpieces.Count; i++)
            {
                IPiece p = currpieces[i];
                if (p.color == clr)
                {
                    List<move> moves = getMoves(p, currtable, currpieces);
                    foreach (move m in moves)
                    {
                        doMove(m, p, currtable, currpieces);
                        if (ischeck(clr, findking(clr, currpieces), currtable, currpieces))
                        {
                            redo(m, p, currtable, currpieces);
                            continue;
                        }
                        int score = alphaBetaMax(a, b, clr, depthleft - 1, currtable, currpieces);
                        if (score <= a)
                        {
                            redo(m, p, currtable, currpieces);
                            return a; // alpha-cutoff
                        }
                        if (score < b)
                            b = score; // beta acts like min in MiniMax
                        redo(m, p, currtable, currpieces);
                    }
                }
            }
            return b;

        }

        private int alphabeta( int depth,string clr, int a, int b,string[,] currtable, List<IPiece> currpieces)
        {
            clr = (clr == "b") ? "s" : "b";
            if (depth == 0)
                return 0;
            if (clr == "s") // ?
            {
                if (ischeckmate(clr,currtable,currpieces))
                    return -999;
                for (int i = 0; i < currpieces.Count; i++)
                {
                    IPiece p = currpieces[i];
                    if (p.color == clr)
                    {
                        List<move> moves = getMoves(p,currtable,currpieces);
                        foreach (move m in moves)
                        {
                            doMove(m, p,currtable,currpieces);
                            if (ischeck(clr, findking(clr,currpieces),currtable,currpieces))
                            {
                                redo(m, p,currtable,currpieces);
                                continue;
                            }
                            a = Math.Max(a, alphabeta(depth - 1,clr, a, b,currtable,currpieces) + ((m.iseated == true) ? returnPoint(m.eatedpiece.pieceName) : 0));
                            if (b < a)
                            {
                                redo(m, p, currtable, currpieces);
                                return a;
                            }
                            redo(m, p,currtable,currpieces);
                        }
                    }
                }
                return a;
            }
            else // b
            {
                if (ischeckmate(clr,currtable,currpieces))
                    return +999;
                for (int i = 0; i < currpieces.Count; i++)
                {
                    IPiece p = currpieces[i];
                    if (p.color == clr)
                    {
                        List<move> moves = getMoves(p,currtable,currpieces);
                        foreach (move m in moves)
                        {
                            doMove(m, p,currtable,currpieces);
                            if (ischeck(clr, findking(clr,currpieces),currtable,currpieces))
                            {
                                redo(m, p, currtable, currpieces);
                                continue;
                            }
                            b = Math.Min(b, alphabeta(depth - 1,clr, a, b, currtable, currpieces) - ((m.iseated == true) ? returnPoint(m.eatedpiece.pieceName) : 0));
                            if (b < a)
                            {
                                redo(m, p, currtable, currpieces);
                                return b;
                            }
                            redo(m, p, currtable, currpieces);
                        }
                    }
                }
                return b;
            }
        }

        protected void doMove(move m, IPiece p,string[,] currtable, List<IPiece> currpieces)
        {
            if ( m.iseated ) // taş yemişiz
            {
                currpieces.RemoveAt(m.index);
            }
            currpieces[getPieceid(m.from,currpieces)].rect = new Rectangle((int)m.to.X,(int)m.to.Y,wh,wh);
            currtable[(int)m.from.X, (int)m.from.Y] = "";
            currtable[(int)m.to.X, (int)m.to.Y] = p.toString(); 
        }

        protected void aivsUpdate(GameTime gameTime)
        {
            if (!isaiplaying)
            {
                new Thread(call).Start();
                Thread.Sleep(300);
                currplayer = (currplayer == "s") ? "b" : "s";
                gameoverControll();
            }
        }

        protected void gameoverControll()
        {
            if (ischeckmate(currplayer, table, pieces))
            {
                if (ischeck(currplayer, findking(currplayer, pieces), table, pieces)) //mat
                {
                    LoadContent();
                }
                else //pat
                {
                    LoadContent();
                }
            }
        }

        protected int evulate()
        {
            int score = 0;
            foreach (IPiece p in pieces)
                score += returnPoint(p.pieceName);
            return score;
        }

        protected void aivsPlayerUpdate(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Released && ism1pressed == true)
            {
                ism1pressed = false;
            }
            if ((currplayer == "s" && IsActive) && isaiplaying == false)
            {
                new System.Threading.Thread(call).Start();
                Thread.Sleep(300);
                currplayer = (currplayer == "s") ? "b" : "s";
                gameoverControll();
            }
            if (isaiplaying == false && currplayer == "b" && ms.LeftButton == ButtonState.Pressed && ism1pressed == false && IsActive) // selecting piece
            {
                ism1pressed = true;
                if (isselected == true) // Want to move
                {
                    isselected = false;
                    if (getPieceid(selected, pieces) == -1) // yok öyle bi taş
                    {
                        isselected = false;
                        ism1pressed = false;
                    }
                    else if (pieces[getPieceid(selected, pieces)].color != currplayer) // kendi taşını seç
                    {
                        isselected = false;
                        ism1pressed = false;
                    }
                    else if (pieces[getPieceid(selected, pieces)].canPlayTo(new Vector2(ms.X / wh, ms.Y / wh), table) && IsActive && new Rectangle(ms.X / wh, ms.Y / wh, 1, 1).Intersects(new Rectangle(0, 0, wh * 8, wh * 8)))
                    {
                        int index = getPieceid(selected, pieces);
                        move m = moveTo(pieces[index], new Vector2(ms.X / wh, ms.Y / wh), table, pieces);
                        if (ischeck(currplayer, findking(currplayer, pieces), table, pieces))
                        {
                            redo(m, pieces[index], table, pieces);
                        }
                        else
                        {
                            currplayer = (currplayer == "s") ? "b" : "s";
                            gameoverControll();
                        }
                    }
                }
                else // Want to select
                {
                    selected = new Vector2(ms.X / wh, ms.Y / wh);
                    if (getPieceid(selected, pieces) != -1 && pieces[getPieceid(selected, pieces)].color == currplayer)
                        isselected = true;
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (aivs)
                aivsUpdate(gameTime);
            else
                aivsPlayerUpdate(gameTime);

            base.Update(gameTime);
        }

        protected List<IPiece> copylist(List<IPiece> currpieces)
        {
            List<IPiece> newlist = new List<IPiece>();
            for (int i = 0; i < currpieces.Count; i++)
                newlist.Add(currpieces[i].Clone());
            return newlist;
        }

        protected string[,] copytable(string[,] currtable)
        {
            string[,] newtable = new string[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    newtable[i, j] = currtable[i, j].Substring(0);
            return newtable;
        }

        protected void redo(move m, IPiece p,string[,] currtable, List<IPiece> currpieces)
        {
            // undo table changes
            currtable[(int)m.from.X, (int)m.from.Y] = p.toString();
            currtable[(int)m.to.X, (int)m.to.Y] = "";
            currpieces[getPieceid(new Vector2(p.rect.X, p.rect.Y), currpieces)].rect = new Rectangle((int)m.from.X, (int)m.from.Y, wh, wh);
            if (m.iseated == true)
            {
                currtable[(int)m.to.X, (int)m.to.Y] = m.eatedpiece.toString();
                currpieces.Insert(m.index, m.eatedpiece);
            }
            //undo pieces changes
            return;
        }

        protected bool ischeck(string currcolor, Vector2 king,string[,] currtable, List<IPiece> currpieces) // colora şah çekiliyor mu
        {
            if (king == new Vector2(-1, -1))
                return true;
            foreach (IPiece p in currpieces)
                if (p.color != currcolor)
                    if (p.canPlayTo(king, currtable))
                        return true;
            return false;
        }

        protected bool ischeckmate(string currcolor, string[,] currtable, List<IPiece> currpieces)
        {
            for (int x = 0; x < currpieces.Count; x++)
            {
                IPiece p = currpieces[x];
                if (p.color == currcolor)
                {
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            if (p.canPlayTo(new Vector2(i, j), currtable)) // seçili taş i,j ye oynayabiliyorsa
                            {
                                move m = moveTo(p, new Vector2(i, j),currtable,currpieces); // oynat
                                if (ischeck(currplayer, findking(currplayer,currpieces), currtable,currpieces)) // şah Çekiliyo,burdan bişey çıkmaz
                                {
                                    redo(m, p,currtable,currpieces);
                                    continue;
                                }
                                else//mat olmamış, oynicak yeri var.
                                {
                                    redo(m, p,currtable,currpieces);
                                    return false;
                                }
                            }
                        }
                }
            } // buraya geldiine göre mat olmuşuz.
            return true;
        } // color mat oldu mu

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

        protected Vector2 findking(string color, List<IPiece> currpieces)
        {
            foreach (IPiece p in currpieces)
                if (p.color == color)
                    if (p.pieceName == "kral")
                        return new Vector2(p.rect.X, p.rect.Y);
            return new Vector2(-1, -1);
        }

        protected move moveTo(IPiece p, Vector2 to, string[,] currtable, List<IPiece> currpieces)
        {
            move m = new move();
            int index = -2; // -1 -> getpieceid cant found, -2 -> table[tox,toy] is empty
            m.to = to;
            m.iseated = false;
            m.eatedpiece = null;
            m.from = new Vector2(p.rect.X, p.rect.Y);
            m.index = -1;
            if (currtable[(int)to.X, (int)to.Y] != "") // table[i,j] isnt empty, means eating a piece
            {
                m.iseated = true;  // add removed items into to move struct
                index = getPieceid(new Vector2(to.X, to.Y),currpieces);
                if (index == -1)
                    return m;
                m.eatedpiece = currpieces[index];
                currpieces.RemoveAt(index);
                m.index = index;
            }
            currtable[p.rect.X, p.rect.Y] = ""; // changing table
            currtable[(int)to.X, (int)to.Y] = p.toString();
            currpieces[getPieceid(new Vector2(p.rect.X, p.rect.Y), currpieces)].rect = new Rectangle((int)to.X, (int)to.Y, wh, wh); //changing p. pieces coords

            return m;
        }

        protected int getPieceid(Vector2 coords, List<IPiece> currpieces)
        {
            for (int i = 0; i < currpieces.Count; i++)
                if (currpieces[i].rect.X == coords.X && currpieces[i].rect.Y == coords.Y)
                    return i;
            return -1;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);

            spriteBatch.Begin();

            if (isselected)
            {
                IPiece p = pieces[getPieceid(new Vector2(selected.X, selected.Y),pieces)];
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
                IPiece pi = pieces[getPieceid(selected,pieces)];
                    List<move> moves = getMoves(pi,table,pieces);
                    for (int i = 0; i < moves.Count;i++ )
                        spriteBatch.Draw(empty, new Rectangle((int)moves[i].to.X * wh, (int)moves[i].to.Y * wh, wh, wh), Color.Blue);   
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
