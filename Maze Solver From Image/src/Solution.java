import java.awt.image.BufferedImage;
import java.awt.image.DataBufferByte;
import java.io.File;
import java.io.IOException;

import javax.imageio.ImageIO;

import org.omg.CORBA.Environment;


public class Solution {

	int MAX_COLOR_SUM = 400;
	final int PEN = 0xff0000;
	
	private int[][] pixels;
	private boolean[][] takenPath;
	private int w, h;
	private int sx,sy;
	private int ex,ey;
	
	public Solution( BufferedImage imgg, int w, int h, int sx, int sy, int ex, int ey, int mcs ) // todo : init 
	{
		MAX_COLOR_SUM = mcs;
		takenPath = new boolean[h][w];
		
		pixels = convertTo2DWithoutUsingGetRGB( imgg );
		for( int i=0; i<h; i++ )
			for( int j=0; j<w; j++ )
			{
				boolean val = ( ( ( (pixels[i][j]&0xff) + ((pixels[i][j]&(0xff<<8))>>8) + ((pixels[i][j]&(0xff<<16))>>16) )<=MAX_COLOR_SUM)?true:false );
				
				takenPath[i][j] = val;
				
				if( val == true ) // for filling the bits that is next to (j,i)
				{
					try{
					takenPath[i-1][j] = val;
					
					takenPath[i+1][j] = val;
					takenPath[i-1][j-1] = val;
					takenPath[i][j-1] = val;
					takenPath[i+1][j-1] = val;
					takenPath[i-1][j+1] = val;
					takenPath[i][j+1] = val;
					takenPath[i+1][j+1] = val;
					}catch( Exception e ){}
				}
			}
		this.h = h;
		this.w = w;
		this.sx = sx;
		this.sy = sy;
		this.ex = ex;
		this.ey = ey;
	}
	
	public void start()
	{
		int penSize = 1 + h/250;
		penSize = 2;
		flag = false;
		System.out.println("Rec starting..");
		rec( sx,sy,penSize );
	}
	
	boolean flag = false;
	
	private void rec( int x, int y, int sz )
	{
		//System.out.println(""+x+","+y);
		takenPath[y][x] = true;
		
		int t1 = 0xffffff,t2 = 0xffffff,t3 = 0xffffff,t4 = 0xffffff,t5 = 0xffffff;
		
		if( flag == true )
			return;
		
		if( Math.abs(x-ex) < sz && Math.abs(y-ey) < sz )
		{
			System.out.println("Starting to Create Output..");
			BufferedImage img = new BufferedImage(w, h, BufferedImage.TYPE_INT_RGB);
			for( int i=0; i<h; i++ )
				for( int j=0; j<w; j++ )
					img.setRGB(j, i, pixels[i][j]);
			try {
				ImageIO.write( img, "bmp", new File(System.getProperty("user.dir")+"o.bmp"));
				
				flag = true;
				
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		
		if( x-sz >= 0 && takenPath[y][x-sz] == false )
		{
			try{ t1 = pixels[y][x]; }catch( Exception e ){}
			try{ t2 = pixels[y+sz][x+sz]; }catch( Exception e ){}
			try{ t3 = pixels[y+sz][x-sz]; }catch( Exception e ){}
			try{ t4 = pixels[y-sz][x+sz]; }catch( Exception e ){}
			try{ t5 = pixels[y-sz][x-sz]; }catch( Exception e ){}
			
			try{ pixels[y][x] = PEN; }catch( Exception e ){}
			try{ pixels[y+sz][x+sz] = PEN; }catch( Exception e ){}
			try{ pixels[y+sz][x-sz] = PEN; }catch( Exception e ){}
			try{ pixels[y-sz][x+sz] = PEN; }catch( Exception e ){}
			try{ pixels[y-sz][x-sz] = PEN; }catch( Exception e ){}
			
			
			rec( x-sz,y,sz );
			
			try{ pixels[y][x] = t1; }catch( Exception e ){}
			try{ pixels[y+sz][x+sz] = t2; }catch( Exception e ){}
			try{ pixels[y+sz][x-sz] = t3; }catch( Exception e ){}
			try{ pixels[y-sz][x+sz] = t4; }catch( Exception e ){}
			try{ pixels[y-sz][x-sz] = t5; }catch( Exception e ){}
		}
		if( x+sz < w && takenPath[y][x+sz] == false )
		{
			try{ t1 = pixels[y][x]; }catch( Exception e ){}
			try{ t2 = pixels[y+sz][x+sz]; }catch( Exception e ){}
			try{ t3 = pixels[y+sz][x-sz]; }catch( Exception e ){}
			try{ t4 = pixels[y-sz][x+sz]; }catch( Exception e ){}
			try{ t5 = pixels[y-sz][x-sz]; }catch( Exception e ){}
			
			try{ pixels[y][x] = PEN; }catch( Exception e ){}
			try{ pixels[y+sz][x+sz] = PEN; }catch( Exception e ){}
			try{ pixels[y+sz][x-sz] = PEN; }catch( Exception e ){}
			try{ pixels[y-sz][x+sz] = PEN; }catch( Exception e ){}
			try{ pixels[y-sz][x-sz] = PEN; }catch( Exception e ){}
			
			
			rec( x+sz,y,sz );
			
			try{ pixels[y][x] = t1; }catch( Exception e ){}
			try{ pixels[y+sz][x+sz] = t2; }catch( Exception e ){}
			try{ pixels[y+sz][x-sz] = t3; }catch( Exception e ){}
			try{ pixels[y-sz][x+sz] = t4; }catch( Exception e ){}
			try{ pixels[y-sz][x-sz] = t5; }catch( Exception e ){}
		}
		if( y-sz >= 0 && takenPath[y-sz][x] == false )
		{
			try{ t1 = pixels[y][x]; }catch( Exception e ){}
			try{ t2 = pixels[y+sz][x+sz]; }catch( Exception e ){}
			try{ t3 = pixels[y+sz][x-sz]; }catch( Exception e ){}
			try{ t4 = pixels[y-sz][x+sz]; }catch( Exception e ){}
			try{ t5 = pixels[y-sz][x-sz]; }catch( Exception e ){}
			
			try{ pixels[y][x] = PEN; }catch( Exception e ){}
			try{ pixels[y+sz][x+sz] = PEN; }catch( Exception e ){}
			try{ pixels[y+sz][x-sz] = PEN; }catch( Exception e ){}
			try{ pixels[y-sz][x+sz] = PEN; }catch( Exception e ){}
			try{ pixels[y-sz][x-sz] = PEN; }catch( Exception e ){}
			
			
			rec( x,y-sz,sz );
			
			try{ pixels[y][x] = t1; }catch( Exception e ){}
			try{ pixels[y+sz][x+sz] = t2; }catch( Exception e ){}
			try{ pixels[y+sz][x-sz] = t3; }catch( Exception e ){}
			try{ pixels[y-sz][x+sz] = t4; }catch( Exception e ){}
			try{ pixels[y-sz][x-sz] = t5; }catch( Exception e ){}
		}
		if( y+sz < h && takenPath[y+sz][x] == false )
		{
			try{ t1 = pixels[y][x]; }catch( Exception e ){}
			try{ t2 = pixels[y+sz][x+sz]; }catch( Exception e ){}
			try{ t3 = pixels[y+sz][x-sz]; }catch( Exception e ){}
			try{ t4 = pixels[y-sz][x+sz]; }catch( Exception e ){}
			try{ t5 = pixels[y-sz][x-sz]; }catch( Exception e ){}
			
			try{ pixels[y][x] = PEN; }catch( Exception e ){}
			try{ pixels[y+sz][x+sz] = PEN; }catch( Exception e ){}
			try{ pixels[y+sz][x-sz] = PEN; }catch( Exception e ){}
			try{ pixels[y-sz][x+sz] = PEN; }catch( Exception e ){}
			try{ pixels[y-sz][x-sz] = PEN; }catch( Exception e ){}
			
			
			rec( x,y+sz,sz );
			
			try{ pixels[y][x] = t1; }catch( Exception e ){}
			try{ pixels[y+sz][x+sz] = t2; }catch( Exception e ){}
			try{ pixels[y+sz][x-sz] = t3; }catch( Exception e ){}
			try{ pixels[y-sz][x+sz] = t4; }catch( Exception e ){}
			try{ pixels[y-sz][x-sz] = t5; }catch( Exception e ){}
		}
			
			
	}
	
	
	public int[][] convertTo2DWithoutUsingGetRGB( BufferedImage image) {

	      final byte[] pixels = ((DataBufferByte) image.getRaster().getDataBuffer()).getData();
	      final int width = image.getWidth();
	      final int height = image.getHeight();
	      final boolean hasAlphaChannel = image.getAlphaRaster() != null;

	      int[][] result = new int[height][width];
	      if (hasAlphaChannel) {
	         final int pixelLength = 4;
	         for (int pixel = 0, row = 0, col = 0; pixel < pixels.length; pixel += pixelLength) {
	            int argb = 0;
	            //argb += (((int) pixels[pixel] & 0xff) << 24); // alpha
	            argb += ((int) pixels[pixel + 1] & 0xff); // blue
	            argb += (((int) pixels[pixel + 2] & 0xff) << 8); // green
	            argb += (((int) pixels[pixel + 3] & 0xff) << 16); // red
	            result[row][col] = argb;
	            col++;
	            if (col == width) {
	               col = 0;
	               row++;
	            }
	         }
	      } else {
	         final int pixelLength = 3;
	         for (int pixel = 0, row = 0, col = 0; pixel < pixels.length; pixel += pixelLength) {
	            int argb = 0;
	            //argb += -16777216; // 255 alpha
	            argb += ((int) pixels[pixel] & 0xff); // blue
	            argb += (((int) pixels[pixel + 1] & 0xff) << 8); // green
	            argb += (((int) pixels[pixel + 2] & 0xff) << 16); // red
	            result[row][col] = argb;
	            col++;
	            if (col == width) {
	               col = 0;
	               row++;
	            }
	         }
	      }

	      return result;
	}

}
