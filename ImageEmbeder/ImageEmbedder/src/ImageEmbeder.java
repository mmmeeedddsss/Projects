import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import java.util.Scanner;

import javax.imageio.ImageIO;
import javax.imageio.ImageTypeSpecifier;


public class ImageEmbeder {

	static class imageEngine{
		
		BufferedImage simg;
		int h,w;
		
		public imageEngine( String s ) throws IOException
		{
			simg = ImageIO.read( new File(s) );
			h = simg.getHeight();
			w = simg.getWidth();
		}
		
		public void insert( BufferedImage iimg )
		{
			BufferedImage nimg = new BufferedImage(w, h, BufferedImage.TYPE_INT_RGB);
			for( int i=0; i<h; i++ )
				for( int j=0; j<w; j++ )
				{
					int irgb = iimg.getRGB(j, i);
					irgb = irgb & 0xFFFFFF;
					int ir = (irgb & 0x00FF0000) >> 16; // 111000 -> 000111
					int ig = (irgb & 0x0000FF00) >> 8;
					int ib = (irgb & 0x000000FF);
					
					
					int srgb = simg.getRGB(j, i);
					srgb = srgb & 0xFFFFFF;
					int sr = (srgb & 0x00FF0000) >> 16; // 111001 -> 111000
					int sg = (srgb & 0x0000FF00) >> 8;
					int sb = (srgb & 0x000000FF);
					
					int nrgb = ( (0xFF)<<24 );
					nrgb += ((( (sr & 0b11110000) ) + ((ir & 0b11110000) >> 4 ))<<16);
					nrgb += ((( (sg & 0b11110000) ) + ((ig & 0b11110000) >> 4 ))<<8);
					nrgb += (( (sb & 0b11110000) ) + ((ib & 0b11110000) >> 4 ));
					
					
					nimg.setRGB(j, i, nrgb);
					
					//System.out.println(nrgb+" "+irgb+" "+srgb);
				}
			try {
				ImageIO.write(nimg, "bmp", new File("o.bmp"));
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		
	}
	
	
	public static void main(String[] args) {
		Scanner sc = new Scanner(System.in);
		
		System.out.println("Takes input files as i1 and i2 from Current Dir, gives output as file \"o\" with jpg format\nPress c to continue");
		
		sc.next();
		
		try {
			imageEngine ie = new imageEngine("i1.bmp");
			ie.insert(ImageIO.read(new File("i2.bmp")));
			
			
			
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

}
