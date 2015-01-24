import java.awt.image.*;
import java.io.File;
import java.io.IOException;

import javax.imageio.ImageIO;


public class RecSolutionGenerater {
	
	public static void main(String[] args)  {
		BufferedImage imgg;
		
		if( args.length != 5 )
		System.out.println("Args : \n" +
				"\t0 -> X of Start Point\n" +
				"\t1 -> Y of Start Point\n" +
				"\t2 -> X of Final Point\n" +
				"\t3 -> Y of Final Point\n" +
				"\t4 -> Sensitivity, a number which will be used in detaching white from black, \n" +
				"\t     If the pixels sum of its rgb is lover than that number, pixel will be defined as a wall, otherwise as a clear way\n" +
				"\t5 -> Location of input, as jpg,bmp or png format\n");
		else
		{
			try {
				imgg = ImageIO.read( new File(args[5]) );
	
				Solution s = new Solution( imgg, imgg.getWidth(), imgg.getHeight()
						, Integer.parseInt(args[0]), Integer.parseInt(args[1]), 
						Integer.parseInt(args[2]), Integer.parseInt(args[3]), Integer.parseInt(args[4]) );
				
				s.start();
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			try {
				imgg = ImageIO.read( new File(args[5]) );
	
				Solution s = new Solution( imgg, imgg.getWidth(), imgg.getHeight()
						, Integer.parseInt(args[0]), Integer.parseInt(args[1]), 
						Integer.parseInt(args[2]), Integer.parseInt(args[3]), Integer.parseInt(args[4]) );
				
				s.start();
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			try {
				imgg = ImageIO.read( new File(args[5]) );
	
				Solution s = new Solution( imgg, imgg.getWidth(), imgg.getHeight()
						, Integer.parseInt(args[0]), Integer.parseInt(args[1]), 
						Integer.parseInt(args[2]), Integer.parseInt(args[3]), Integer.parseInt(args[4]) );
				
				s.start();
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
	}

}
