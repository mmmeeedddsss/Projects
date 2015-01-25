using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ImageCrater
{
    class Program
    {
        static void Main(string[] args)
        {
            //Write
            String msg = Console.ReadLine();
            int l;
            l  = (int)Math.Sqrt(msg.Length);
            if ( l * l < msg.Length)
                l = (++l);
            Bitmap bm = new Bitmap(l, l);

            for (int i = 0; i < l; i++) // y
                for (int j = 0; j < l; j++) // x
                    if (msg.Length > (i * l) + j)
                        bm.SetPixel(j, i, Color.FromArgb(0, 0, (int)msg[(i * l) + j]));
                    else
                        bm.SetPixel(j, i, Color.FromArgb(0,1,(int)msg[j]));
            bm.Save("img.png", System.Drawing.Imaging.ImageFormat.Png);

            // Read
            Console.WriteLine();
            bm = new Bitmap("img.png");
            for (int i = 0; i < bm.Height; i++)
                for (int j = 0; j < bm.Width; j++)
                    if (bm.GetPixel(j, i).G != 1)
                        Console.Write((char)bm.GetPixel(j, i).B);
                    else
                    {
                        i = bm.Height;
                        j = bm.Width;
                    }
            Console.Read();
        }
    }
}
