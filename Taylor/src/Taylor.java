import java.awt.Color;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.Image;
import java.awt.MouseInfo;
import java.awt.Point;
import java.awt.PointerInfo;
import java.awt.RenderingHints;
import java.awt.Toolkit;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.awt.image.BufferedImage;
import java.util.Date;
import javax.swing.JFrame;
import javax.swing.JPanel;


public class Taylor extends JPanel {

	static int szx = 1400;
	static int szy = 900;
	
	
	int fps = 0;
	long lt = 0;
	
	long t;
	
	private void init()
	{
	}
	
	private long fact( double n )
	{
		int s = 1;
		while( n>1 )
		{
			if( Integer.MAX_VALUE/n < s )
				sec=6;
			s*=(n--);
		}
		return s;
	}
	
	private void update( double n )
	{	
	}
	
	double getSinT( double r, int n )
	{
		double val = 0;
		for( int i=0; i<n; i++ )
		{
			double nom = Math.pow(-1, i)*Math.pow(r, 2*i+1);
			double den = fact(2*i+1);
			val += nom/den;
		}
		return val;
	}
	
	private void drawL( double x1,double y1, double x2, double y2, Graphics2D g2d )
	{
		g2d.drawLine((int)x1+szx/2, (int)y1+szy/2, (int)x2+szx/2, (int)y2+szy/2);
	}
	
	
	double rad( double ang )
	{
		return ang / 180.0*Math.PI;
	}
	
	@Override
	public void paint(Graphics g) {
		super.paint(g);
		
		Graphics2D g2d = (Graphics2D) g;
		g2d.setRenderingHint(RenderingHints.KEY_ANTIALIASING,
				RenderingHints.VALUE_ANTIALIAS_ON);
		
		
		
		g.setColor( new Color( 50,50,50 ) );
		drawL( 0,szy/2,0,-szy/2, g2d );
		drawL( -szx/2, 0, szx/2, 0 , g2d);
		
		g.setColor( new Color( 150,150,150 ) );
		drawL( 350,szy/2, 350 ,-szy/2, g2d );
		drawL( -350,szy/2, -350 ,-szy/2, g2d );
		
		drawL( -szx/2, Math.sin(Math.PI/2)*300, szx/2, Math.sin(Math.PI/2)*300 , g2d);
		drawL( -szx/2, -Math.sin(Math.PI/2)*300, szx/2, -Math.sin(Math.PI/2)*300 , g2d);
		
		g.setColor(new Color(0,0,0));
		for( int i=-szx/2; i<szx/2; i++ )
			try{drawL( i,Math.sin(Math.toRadians(i/((double)szx/2)*180))*300,i+1,Math.sin(Math.toRadians(i/((double)szx/2)*180))*300+1,g2d );} catch( Exception e ){ continue; }
		g2d.drawString("Sinx on [-360,360]", szx-250, 30);
		
		
		g.setColor(new Color(255,0,0));
		for( int i=-szx/2; i<szx/2; i++ )// for all x values, from -700 to 700, turn this to radian
			try{drawL( i,getSinT( rad(i/((double)szx/2)*180),sec )*300,i+1,getSinT( rad(i/((double)szx/2)*180),sec )*300+1,g2d );} catch( Exception e ){ continue; }
		g2d.drawString("Taylor with sinx on [-360,360], with n goes "+sec, szx-350, 60);
		
		g2d.finalize();
	}
	
	int sec = 0;
	
	public static void main( String[] args )
	{
		Taylor game = new Taylor();
		
		JFrame frame = new JFrame("Taylor");
		frame.setSize(szx, szy);
		frame.setVisible(true);
		frame.add(game);
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		
		
		game.t = (new Date()).getTime();
		game.init();
		game.repaint();
		game.sleep(500);
		game.init();
		game.repaint();
		while( true )
		{
			game.update(game.sec++);
			game.repaint();
			game.sleep(2000);
		}
		
	}
	
	private void sleep( int t )
	{
		try {
			Thread.sleep( t );
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	
}
