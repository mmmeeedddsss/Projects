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
	class Knight : IPiece, ICloneable
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

		public Knight( Texture2D tex, string pieceName, string color, Rectangle rect )
		{
			this.tex = tex;
			this.pieceName = pieceName;
			this.color = color;
			this.rect = rect;
		}

		public Object Clone()
		{
			Knight b = new Knight(this.tex, this.pieceName, this.color, this.rect);
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
			if( x.Equals(new Vector2( rect.X, rect.Y ))) return false;
			char oppentcolor = 'b';
            if (color == "b")
                oppentcolor = 's';
			int x1, y1;
			x1 = (int)Math.Abs(x.X - rect.X);
			y1 = (int)Math.Abs(x.Y - rect.Y);
			if ((x1 + y1) == 3 && x1 != 0 && y1 != 0)
			{
				if (table[(int)x.X, (int)x.Y] == "" || table[(int)x.X, (int)x.Y].StartsWith("" + oppentcolor))
					return true;
			}
			return false;
		}
		public bool inrange( int x )
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
			if (table[currx + 2, curry + 1] == "" || table[currx + 2, curry + 1].StartsWith("" + oppentcolor)) manacearea[currx + 2, curry + 1] = color.ElementAt<char>(0);
			if (table[currx + 2, curry - 1] == "" || table[currx + 2, curry - 1].StartsWith("" + oppentcolor)) manacearea[currx + 2, curry - 1] = color.ElementAt<char>(0);
			if (table[currx - 2, curry + 1] == "" || table[currx - 2, curry + 1].StartsWith("" + oppentcolor)) manacearea[currx - 2, curry + 1] = color.ElementAt<char>(0);
			if (table[currx - 2, curry - 1] == "" || table[currx - 2, curry - 1].StartsWith("" + oppentcolor)) manacearea[currx - 2, curry - 1] = color.ElementAt<char>(0);
			if (table[currx + 1, curry + 2] == "" || table[currx + 1, curry + 2].StartsWith("" + oppentcolor)) manacearea[currx + 1, curry + 2] = color.ElementAt<char>(0);
			if (table[currx - 1, curry + 2] == "" || table[currx - 1, curry + 2].StartsWith("" + oppentcolor)) manacearea[currx - 1, curry + 2] = color.ElementAt<char>(0);
			if (table[currx + 1, curry - 2] == "" || table[currx + 1, curry - 2].StartsWith("" + oppentcolor)) manacearea[currx + 1, curry - 2] = color.ElementAt<char>(0);
			if (table[currx - 1, curry - 2] == "" || table[currx - 1, curry - 2].StartsWith("" + oppentcolor)) manacearea[currx - 1, curry - 2] = color.ElementAt<char>(0);
		}

	}
}
