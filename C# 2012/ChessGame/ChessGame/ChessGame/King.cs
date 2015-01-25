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
	class King : IPiece
	{
		Texture2D texx;
		public Texture2D tex
		{
			get
			{
				return texx;
			}
			set
			{
				texx = value;
			}
		}
		string colorr;
		public string color
		{
			get
			{
				return colorr;
			}
			set
			{
				colorr = value;
			}
		}
		string pieceNamee;
		public string pieceName
		{
			get
			{
				return pieceNamee;
			}
			set
			{
				pieceNamee = value;
			}
		}
		Rectangle rectt;
		public Rectangle rect
		{
			get
			{
				return rectt;
			}
			set
			{
				rectt = value;
			}
		}

		public King( Texture2D tex, string pieceName, string color, Rectangle rect )
		{
			this.tex = tex;
			this.pieceName = pieceName;
			this.color = color;
			this.rect = rect;
		}

        public IPiece Clone()
        {
            return new King(this.tex, this.pieceName.Substring(0), this.color.Substring(0), new Rectangle(rect.X, rect.Y, rect.Width,rect.Height));
        }

		public void Draw(SpriteBatch spritebatch,int wh)
		{
			if( ( rect.X + rect.Y ) % 2 == 0  )
				spritebatch.Draw(tex, new Rectangle(rect.X * wh, rect.Y * wh, wh, wh), Color.White);
			else
				spritebatch.Draw(tex, new Rectangle(rect.X * wh, rect.Y * wh, wh, wh), Color.DarkOliveGreen);
		}
		public string toString()
		{
			return color+pieceName;
		}
		public bool canPlayTo(Vector2 x, string[,] table)
		{
			if (x.Equals(new Vector2(rect.X,rect.Y))) return false;
			char oppentcolor = 'b';
			if (color == "b")
				oppentcolor = 's';
			if (table[(int)x.X, (int)x.Y] != "" && !table[(int)x.X, (int)x.Y].StartsWith("" + oppentcolor)) return false;
			Vector2 curpos = new Vector2(rect.X,rect.Y);
            if (curpos.X + 1 == x.X && curpos.Y == x.Y) return true;
            if (curpos.X - 1 == x.X && curpos.Y == x.Y) return true;
            if (curpos.X == x.X && curpos.Y - 1 == x.Y) return true;
            if (curpos.X == x.X && curpos.Y + 1 == x.Y) return true;
            if (curpos.X - 1 == x.X && curpos.Y - 1 == x.Y) return true;
            if (curpos.X + 1 == x.X && curpos.Y + 1 == x.Y) return true;
			if (curpos.X + 1 == x.X && curpos.Y - 1 == x.Y) return true;
			if (curpos.X - 1 == x.X && curpos.Y + 1 == x.Y) return true;
			return false;
		}
		public void calcManArea(ref char[,] manacearea,string[,] table)
		{
			int currx = rect.X, curry = rect.Y;
			char oppentcolor = 'b';
			if (color == "b")
				oppentcolor = 's';
			if (table[currx+1, curry+1] == "" || table[currx+1, curry+1].StartsWith("" + oppentcolor)) manacearea[currx+1, curry+1] = color.ElementAt<char>(0);
			if (table[currx+1, curry-1] == "" || table[currx+1, curry-1].StartsWith("" + oppentcolor)) manacearea[currx+1, curry-1] = color.ElementAt<char>(0);
			if (table[currx+1, curry] == "" || table[currx+1, curry].StartsWith("" + oppentcolor)) manacearea[currx+1, curry] = color.ElementAt<char>(0);
			if (table[currx-1, curry] == "" || table[currx-1, curry].StartsWith("" + oppentcolor)) manacearea[currx-1, curry] = color.ElementAt<char>(0);
			if (table[currx, curry+1] == "" || table[currx, curry+1].StartsWith("" + oppentcolor)) manacearea[currx, curry+1] = color.ElementAt<char>(0);
			if (table[currx, curry-1] == "" || table[currx, curry-1].StartsWith("" + oppentcolor)) manacearea[currx, curry-1] = color.ElementAt<char>(0);
			if (table[currx-1, curry+1] == "" || table[currx-1, curry+1].StartsWith("" + oppentcolor)) manacearea[currx-1, curry+1] = color.ElementAt<char>(0);
			if (table[currx-1, curry-1] == "" || table[currx-1, curry-1].StartsWith("" + oppentcolor)) manacearea[currx-1, curry-1] = color.ElementAt<char>(0);
		}

	}
}
