using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Picture_Edit
{
    class Program
    {
        static Point res;
        static void Main(string[] args)
        {
            Console.WriteLine("Import Location : ");
            String s = Console.ReadLine();
            Bitmap bm = (Bitmap)Bitmap.FromFile(s, true);
            res = new Point(bm.Width,bm.Height);
            Color[,] clrs = updateColorArray(bm);

            Console.WriteLine("What do you want to do?");
            Console.WriteLine("g for Grayscale, b for Blur, r for Rotate90");
            Console.WriteLine("Query as String : ");
            s = Console.ReadLine();
            for (int i = 0; i < s.Length; i++ )
            {
                switch(s[i])
                {
                    case 'g':
                        clrs = Grayscale(clrs);
                        break;
                    case 'b':
                        clrs = Blur(clrs);
                        break;
                    case 'r':
                        clrs = rotate90(clrs);
                        break;
                }
            }

            bm = updateBitmap(clrs);
            Console.WriteLine("Save Location : ");
            s = Console.ReadLine();
            Console.WriteLine("Saving !");
            bm.Save(s);
        }

        static Color[,] Grayscale(Color[,] clrs)
        {
            Color[,] newColors = new Color[res.X,res.Y];
            Console.WriteLine("Starting Grayscaling...");
            for (int i = 0; i < res.X ; i++)
            {
                for (int j = 0; j < res.Y; j++)
                {
                    Color originalColor = clrs[i, j];
                    int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .59)
                    + (originalColor.B * .11));
                    newColors[i, j] = Color.FromArgb(grayScale, grayScale, grayScale);
                }
            }
            Console.WriteLine("Grayscaling Done !");
            return newColors;
        }

        static Bitmap updateBitmap(Color[,] clrs)
        {
            Console.WriteLine("Writing Color Array to Bitmap..");
            Bitmap bm = new Bitmap(res.X, res.Y);
            for (int i = 0; i < bm.Width; i++)
            {
                for (int j = 0; j < bm.Height; j++)
                {
                    bm.SetPixel(i, j, clrs[i, j]);
                }
            }
            Console.WriteLine("Writing Done !");
            return bm;
        }

        static Color[,] updateColorArray(Bitmap bm)
        {
            Console.WriteLine("Importing Bitmap...");
            Color[,] clrs = new Color[bm.Width, bm.Height];
            for (int i = 0; i < bm.Width; i++)
            {
                for (int j = 0; j < bm.Height; j++)
                {
                    clrs[i, j] = bm.GetPixel(i, j);
                }
            }
            Console.WriteLine("Import Done !");
            return clrs;
        }

        static Color[,] rotate90(Color[,] clrs)
        {
            Console.WriteLine("Rotating Image...");
            Color[,] newColors = new Color[res.Y,res.X];
            res = new Point(res.Y,res.X);
            for (int i = 0; i < res.X; i++)
            {
                for (int j = 0; j < res.Y; j++)
                {
                    newColors[i, j] = clrs[res.Y - j - 1, i];
                }
            }
            Console.WriteLine("Rotate Done !");
            return newColors;
        }

        static Color[,] Blur(Color[,] clrs)
        {
            Color[,] newColors = new Color[res.X,res.Y];
            Console.WriteLine("Starting Blur...");
            for (int i = 0; i < res.X; i++)
            {
                newColors[i, 0] = clrs[i, 0];
                newColors[i, res.Y - 1] = clrs[i, res.Y - 1];
            } 
            for (int i = 0; i < res.Y; i++)
            {
                newColors[0, i] = clrs[0, i];
                newColors[res.X - 1, i] = clrs[res.X - 1,i];
            }
            for (int i = 1; i < res.X - 1; i++)
            {
                for (int j = 1; j < res.Y - 1; j++)
                {
                    int r = 0, g = 0, b = 0;
                    Color p = clrs[i + 1, j];
                    r += p.R;
                    g += p.G;
                    b += p.B;
                    p = clrs[i - 1, j];
                    r += p.R;
                    g += p.G;
                    b += p.B;
                    p = clrs[i, j + 1];
                    r += p.R;
                    g += p.G;
                    b += p.B;
                    p = clrs[i, j - 1];
                    r += p.R;
                    g += p.G;
                    b += p.B;
                    newColors[i,j] = Color.FromArgb(255,r/4,g/4,b/4);
                }
            }
            Console.WriteLine("Blur Done !");
            return newColors;
        }
    }
}
