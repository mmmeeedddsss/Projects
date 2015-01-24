import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import java.util.Scanner;

import javax.imageio.ImageIO;


public class ImageDecrypter {

	
	public static class ImageDecrypterEngine{
		ImageDecrypterEngine( )
		{
		}
		
		static public void Decrypt( BufferedImage img )
		{
			BufferedImage nimg = new BufferedImage(img.getWidth(), img.getHeight(), BufferedImage.TYPE_INT_RGB);
			for( int i=0; i<img.getHeight(); i++ )
				for( int j=0; j<img.getWidth(); j++ )
				{
					int rgb = img.getRGB(j, i);
					int r = (rgb & 0xFF0000) >> 16;
					int g = (rgb & 0x00FF00) >> 8;
					int b = (rgb & 0x0000FF);
					
					int nr = (( r & 0b00001111 ) << 4);
					int ng = (( g & 0b00001111 ) << 4);
					int nb = (( b & 0b00001111 ) << 4);
					
					nimg.setRGB(j, i,( (nr<<16)+(ng<<8)+(nb) ) );
				}
			try {
				ImageIO.write(nimg, "bmp", new File("dco.bmp"));
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
	}
	
	
	public static void main(String[] args) {
		Scanner sc = new Scanner(System.in);
		
		System.out.println("Takes input files o from Current Dir, gives output as file \"dco\" with jpg format\nPress c to continue");
		
		sc.next();
		
		try {
			ImageDecrypterEngine.Decrypt( ImageIO.read( new File( "o.bmp" ) ) );
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

}
