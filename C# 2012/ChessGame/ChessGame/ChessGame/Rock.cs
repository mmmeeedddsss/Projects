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
	class Rock : IPiece
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

		public Rock( Texture2D tex, string pieceName, string color, Rectangle rect )
		{
			this.tex = tex;
			this.pieceName = pieceName;
			this.color = color;
			this.rect = rect;
		}

		public IPiece Clone()
		{
            return new Rock(this.tex, this.pieceName.Substring(0), this.color.Substring(0), new Rectangle(rect.X, rect.Y, rect.Width, rect.Height));
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
			if (x.Equals(new Vector2(rect.X, rect.Y))) return false;
			char oppentcolor = 'b';
			if (color == "b")
				oppentcolor = 's';
			if (table[(int)x.X, (int)x.Y] != "" && !table[(int)x.X, (int)x.Y].StartsWith("" + oppentcolor) ) return false;
			if (x.X == rect.X)
			{
				int addy = 1;
				if (x.Y < rect.Y)
					addy = -1;
				int currx = rect.X, curry = rect.Y;
				curry += addy;
				while (curry != x.Y)
				{
					if (table[currx, curry] != "") return false; // yolumda engel var
					curry += addy;
				}
				return true;
			}
			if (x.Y == rect.Y)
			{
				int addx = 1;
				if (x.X < rect.X)
					addx = -1;
				int currx = rect.X, curry = rect.Y;
				currx += addx;
				while (currx != x.X)
				{
					if (table[currx, curry] != "") return false; // yolumda engel var
					currx += addx;
				}
				return true;
			}
			return false;
		}
		public void calcManArea(ref char[,] manacearea,string[,] table)
		{
			int currx = rect.X, curry = rect.Y;
			char oppentcolor = 'b';
			if (color == "b")
				oppentcolor = 's';
			int x = currx;
			int y = curry;

			x++;
			while (table[x, y] == "")
			{
				manacearea[x, y] = color.ElementAt<char>(0);
				x++;
                if (!inrange(x)) break;
			}
			if (table[x, y].StartsWith("" + oppentcolor))
				manacearea[x, y] = color.ElementAt<char>(0);
			//------------------------------------------------------
			x = currx;
			y = curry;
			x--;
			while (table[x, y] == "")
			{
				manacearea[x, y] = color.ElementAt<char>(0);
				x--;
                if (!inrange(x)) break;
			}
			if (table[x, y].StartsWith("" + oppentcolor))
				manacearea[x, y] = color.ElementAt<char>(0);
			//------------------------------------------------------
			x = currx;
			y = curry;
			y--;
			while (table[x, y] == "")
			{
				manacearea[x, y] = color.ElementAt<char>(0);
				y--;
                if (!inrange(y)) break;
			}
			if (table[x, y].StartsWith("" + oppentcolor))
				manacearea[x, y] = color.ElementAt<char>(0);
			//------------------------------------------------------
			x = currx;
			y = curry;
			y++;
			while (table[x, y] == "")
			{
				manacearea[x, y] = color.ElementAt<char>(0);
				y++;
                if (!inrange(y)) break;
			}
			if (table[x, y].StartsWith("" + oppentcolor))
				manacearea[x, y] = color.ElementAt<char>(0);
		}
        public bool inrange(int x)
        {
            if (x >= 0 && x < 8)
                return true;
            return false;
        }
	}
}
