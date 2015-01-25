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
	class Pawn : IPiece, ICloneable
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

		public Pawn( Texture2D tex, string pieceName, string color, Rectangle rect )
		{
			this.tex = tex;
			this.pieceName = pieceName;
			this.color = color;
			this.rect = rect;
		}

		public Object Clone()
		{
			Pawn b = new Pawn(this.tex, this.pieceName, this.color, this.rect);
			return b;
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
			if (table[(int)x.X, (int)x.Y] != "" && !table[(int)x.X, (int)x.Y].StartsWith("" + oppentcolor)) return false;

            if (inrange(rect.X) && inrange(rect.Y-1)) if (table[(int)x.X, (int)x.Y] == "" && table[rect.X, rect.Y - 1] == "")
				if( color == "b" && rect.Y == 6 && x.Y == 4 && rect.X == x.X)
					return true;
            if (inrange(rect.X) && inrange(rect.Y+1)) if (table[(int)x.X, (int)x.Y] == "" && table[rect.X, rect.Y + 1] == "")
				if (color == "s" && rect.Y == 1 && x.Y == 3 && rect.X == x.X)
					return true;
			if (table[(int)x.X, (int)x.Y] == "")
				if (color == "b" && rect.Y - 1 == x.Y && rect.X == x.X)
					return true;
			if (table[(int)x.X, (int)x.Y] == "")
				if (color == "s" && rect.Y + 1 == x.Y && rect.X == x.X)
					return true;
			if (color == "b" && Math.Abs(rect.X - x.X) == 1 && rect.Y - 1 == x.Y && table[(int)x.X, (int)x.Y].StartsWith("" + oppentcolor))
				return true;
			if (color == "s" && Math.Abs(rect.X - x.X) == 1 && rect.Y + 1 == x.Y && table[(int)x.X, (int)x.Y].StartsWith("" + oppentcolor))
				return true;
			return false;

		}
        public bool inrange(int x)
        {
            if (x >= 0 && x < 8)
                return true;
            return false;
        }

		public void calcManArea(ref char[,] manacearea,string[,] table)
		{
			int currx = rect.X, curry = rect.Y;
			char oppentcolor = 'b';
			if (color == "b")
				oppentcolor = 's';
			if (color == "b")
			{
				if( table[currx+1, curry-1] == "" || table[currx+1, curry-1].StartsWith("" + oppentcolor)) manacearea[currx+1, curry-1] = color.ElementAt<char>(0);
				if (table[currx-1, curry-1] == "" || table[currx-1, curry-1].StartsWith("" + oppentcolor)) manacearea[currx-1, curry-1] = color.ElementAt<char>(0);
			}
			else if (color == "s")
			{
				if (table[currx + 1, curry + 1] == "" || table[currx + 1, curry + 1].StartsWith("" + oppentcolor)) manacearea[currx + 1, curry + 1] = color.ElementAt<char>(0);
				if (table[currx - 1, curry + 1] == "" || table[currx - 1, curry + 1].StartsWith("" + oppentcolor)) manacearea[currx - 1, curry + 1] = color.ElementAt<char>(0);
			}
		}

	}
}
