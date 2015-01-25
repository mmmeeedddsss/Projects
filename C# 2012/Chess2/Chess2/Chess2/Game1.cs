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

namespace Chess2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        string[,] table;
        List<Info> info;
        bool ispiecedestroyed;
        Info temp;
        char[,] manaceAreaBlack;
        char[,] manaceAreaWhite;
        char[,] drawTable;
        int x, y;
        bool isrelased,isselected;
        int selecteditemindex;
        Info aibest;
        Vector2 aimove;

        bool p1sturn;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            p1sturn = true;
            isselected = false;
            isrelased = true;

            info = new List<Info>();
            table = new string[8, 8];
            manaceAreaBlack = new char[8,8];
            manaceAreaWhite = new char[8, 8];
            drawTable = new char[8,8];

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferHeight = 750;
            graphics.PreferredBackBufferWidth = 720;
            IsMouseVisible = true;

            graphics.ApplyChanges();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    table[i, j] = " ";
                    drawTable[i, j] = ' ';
                    manaceAreaWhite[i,j] = ' ';
                    manaceAreaBlack[i,j] = ' ';
                }
            }

            for (int i = 0; i < 8; i++)
            {
                table[i, 1] = "spiyon";
                table[i, 6] = "bpiyon";
            }
            table[0, 0] = "skale";
            table[1, 0] = "sat";
            table[2, 0] = "sfil";
            table[3, 0] = "svezir";
            table[4, 0] = "skral";
            table[5, 0] = "sfil";
            table[6, 0] = "sat";
            table[7, 0] = "skale";

            table[0, 7] = "bkale";
            table[1, 7] = "bat";
            table[2, 7] = "bfil";
            table[3, 7] = "bvezir";
            table[4, 7] = "bkral";
            table[5, 7] = "bfil";
            table[6, 7] = "bat";
            table[7, 7] = "bkale";

            this.Content.Load<Texture2D>("Pieces/sfil");

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8;j++ )
                {
                    if( table[i,j] != " " )
                    {
                        string s = table[i, j];
                        char color = s[0];
                        string piecename = "";
                        for (int m = 1; m < s.Length; m++)
                            piecename += s[m];
                            info.Add(new Info((Texture2D)(this.Content.Load<Texture2D>("Pieces/" + color + piecename)), piecename, "" + color, new Vector2(i, j)));
                    }
                }
            
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (p1sturn)
            {
                MouseState ms = Mouse.GetState();
                if (ms.LeftButton == ButtonState.Released && isrelased == false)
                {
                    isrelased = true;
                }
                if (ms.LeftButton == ButtonState.Pressed && isrelased == true && isselected == false)
                {
                    isrelased = false;
                    x = ms.X / 90;
                    y = ms.Y / 90;

                    for (int i = 0; i < info.Count;i++ )
                    {
                        if (info[i].curpos == new Vector2(x, y) && info[i].color == "b")
                        {
                            isselected = true;
                            selecteditemindex = i;
                        }
                    }
                }
                else if (ms.LeftButton == ButtonState.Pressed && isrelased == true && isselected == true )
                {
                    isselected = false;
                    ispiecedestroyed = false;
                    int newx, newy;
                    newx = ms.X / 90;
                    newy = ms.Y / 90;
                    calcmanaceArea("b",info,table);

                    if (info[selecteditemindex].canplay(new Vector2(newx, newy), table, info, manaceAreaBlack,findking("b",info)))
                    {
                        info[selecteditemindex].curpos = new Vector2(newx, newy);
                        if (table[newx, newy] != " ") // Taş Alındıysa
                        {
                            ispiecedestroyed = true;
                            for (int i = 0; i < info.Count; i++) // Alından Taşı Info listinden sil
                                if (info[i].curpos == new Vector2(newx, newy) && info[i].color == "s")
                                {
                                    temp = info[i];
                                    info.RemoveAt(i);
                                    break;
                                }
                        }
                        table[x, y] = " ";
                        table[newx, newy] = "" + info[selecteditemindex].color + info[selecteditemindex].piecename;
                        if (isc("b",info,table)) // Tüm Hamleleri Geri Al
                        {
                            for(int i=0;i<info.Count;i++)
                                if (info[i].curpos == new Vector2(newx, newy))
                                    selecteditemindex = i;
                            table[x, y] = "" + info[selecteditemindex].color + info[selecteditemindex].piecename;
                            info[selecteditemindex].curpos = new Vector2(x, y);
                            if (ispiecedestroyed)
                            {
                                info.Add(temp);
                                table[newx, newy] = "" + temp.color + temp.piecename;
                                ispiecedestroyed = false;
                            }
                        }
                        else // Başarılı Şekilde Oynandı
                        {
                            if (iscm("s",info,table))
                                System.Windows.Forms.MessageBox.Show("!");
                            p1sturn = false;
                            ispiecedestroyed = false;
                        }
                    }
                }
            }
            else // if p2s turn
            {
                aipickbestmove(2,table,info);
                for (int index = 0; index < info.Count; index++)
                {
                    if (info[index].curpos == aimove && info[index].color != aibest.color)//ai taş yemiş
                    {
                        info.RemoveAt(index);
                    }
                }
                for (int index = 0; index < info.Count; index++)
                    if (info[index].curpos == aibest.curpos && info[index].color == aibest.color)
                    {
                        info[index].curpos = aimove;
                    }

                table[(int)aibest.curpos.X, (int)aibest.curpos.Y] = " ";
                table[(int)aimove.X, (int)aimove.Y] = aibest.color + aibest.piecename;
                p1sturn = true;
            }

            base.Update(gameTime);
        }

        protected int aipickbestmove(int currstep,string[,] currtable,List<Info> currinfo)
        {
            int max = -9999;
            string currcolor,enemcolor;
            if (currstep == 0) return 0;
            if (currstep % 2 == 0) { currcolor = "s"; enemcolor = "b";  }
            else{ currcolor = "b"; enemcolor = "s";}

            for (int index = 0; index < currinfo.Count; index++)
            {
                if (currinfo[index].color == currcolor)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            calcmanaceArea(currcolor, currinfo, currtable);
                            bool canplay;
                            if (currcolor == "s") canplay = currinfo[index].canplay(new Vector2(i, j), currtable, currinfo, manaceAreaWhite, findking(currcolor, currinfo));
                            else canplay = currinfo[index].canplay(new Vector2(i, j), currtable, currinfo, manaceAreaBlack, findking(currcolor, currinfo));
                            if (canplay)
                            {
                                int currpoint = 0;
                                Vector2 oldpos = currinfo[index].curpos;
                                string ttable_string = currinfo[index].color + currinfo[index].piecename;
                                string[,] ttable = ( string[,] )currtable.Clone();
                                List<Info> tinfo = new List<Info>();
                                for (int a = 0; a < currinfo.Count; a++)
                                    tinfo.Add((Info)currinfo[a].Clone());

                                    //tinfo[index].curpos = new Vector2(i, j);
                                    if (ttable[i, j] != " ") // Taş Yedi
                                    {
                                        for (int f = 0; f < currinfo.Count; f++) // Alından Taşı Info listinden sil
                                            if (currinfo[i].curpos == new Vector2(i, j) && currinfo[i].color == enemcolor)
                                            {
                                                temp = tinfo[i];
                                                currpoint += returnvalof(temp.piecename); // Alınan Taşın Puanını Ekle 
                                                tinfo.RemoveAt(i);
                                                break;
                                            }
                                    } //Taş Yedi Son
                                ttable[(int)oldpos.X, (int)oldpos.Y] = " ";
                                ttable[i, j] = ttable_string;
                                if (isc(currcolor, tinfo, ttable)) // Tüm Hamleleri Geri Al
                                {
                                    continue;
                                }
                                else // Hamle Yapmak Serbest- ki zaten yaptık -
                                {
                                    int kalanadimlar;
                                    if (currcolor == "s") kalanadimlar = currpoint + aipickbestmove(currstep - 1, ttable, tinfo);
                                    else kalanadimlar = -currpoint + aipickbestmove(currstep - 1, ttable, tinfo);
                                    if (max < kalanadimlar)
                                    {
                                        max = kalanadimlar;
                                        if (currstep == 2)
                                        {
                                            aibest = info[index];
                                            aimove = new Vector2(i, j);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return max;
        }

        protected int returnvalof(string piecename)
        {
            if (piecename == "piyon") return 1;
            if (piecename == "kale") return 5;
            if (piecename == "at") return 3;
            if (piecename == "fil") return 3;
            if (piecename == "vezir") return 9;
            if (piecename == "kral") return 1000;
            return 0;
        }

        protected bool iscm(string color, List<Info> currinfo, string[,] currtable)
        {
            if (color == "b") calcmanaceArea("s",currinfo,currtable);
            else calcmanaceArea("b",currinfo,currtable);
            foreach (Info inf in currinfo)
            {
                if (inf.color == color && inf.piecename == "kral")
                    if (color == "b")
                        return inf.ischeckmate(manaceAreaBlack, currtable);
                    else
                        return inf.ischeckmate(manaceAreaWhite, currtable);
            }
            return true;
        }

        protected bool isc(string color, List<Info> currinfo, string[,] currtable)
        {
            if (color == "b") calcmanaceArea("s",currinfo,currtable);
            else calcmanaceArea("b",currinfo,currtable);
            foreach (Info inf in currinfo)
            {
                if (inf.color == color && inf.piecename == "kral")
                    if (color == "b")
                        return inf.ischeck(manaceAreaBlack, currtable);
                    else
                        return inf.ischeck(manaceAreaWhite, currtable);
            }
            return true;
        }

        protected Vector2 findking(string color, List<Info> currinfo)
        {
            foreach (Info inf in currinfo)
            {
                if (inf.color == color && inf.piecename == "kral")
                    return inf.curpos;
            }
            return Vector2.Zero;
        }

        protected void calcmanaceArea( string colorOfManaceArea,List<Info> currinfo,string[,] currtable)
        {
            char[,] newtable = new char[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0;j < 8;j++ )
                {
                    newtable[i, j] = ' ';
                }
            if (colorOfManaceArea == "b") manaceAreaWhite = newtable;
            else manaceAreaBlack = newtable;

            foreach (Info inf in currinfo)
            {
                if (colorOfManaceArea == inf.color)
                {
                    if (colorOfManaceArea == "b") inf.manaceArea(ref manaceAreaWhite, currinfo, currtable);
                    else inf.manaceArea(ref manaceAreaBlack, currinfo, currtable);
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);
            spriteBatch.Begin();
            Color temp;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    /*
                    if (manaceAreaWhite[i, j] != ' ')
                        temp = Color.Red;
                    else if (manaceAreaBlack[i, j] != ' ')
                        temp = Color.Black;
                    */
                    if ((i + j) % 2 == 1)
                        temp = Color.WhiteSmoke;
                    else
                        temp = Color.DarkGreen;

                    if ((i + j) % 2 == 1)
                        spriteBatch.Draw(this.Content.Load<Texture2D>("Pieces/Empty"), new Rectangle(90 * i, 90 * j, 90, 90), temp);
                    else
                        spriteBatch.Draw(this.Content.Load<Texture2D>("Pieces/Empty"), new Rectangle(90 * i, 90 * j, 90, 90), temp);
                }
            spriteBatch.End();

            foreach (Info i in info)
                i.draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
