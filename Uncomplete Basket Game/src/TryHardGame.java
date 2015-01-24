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


public class TryHardGame extends JPanel implements MouseListener {

	static int szx = 1400;
	static int szy = 850;
	
	int imagew;
	int imageh;
	
	int fps = 0;
	long lt = 0;
	
	double ballx, bally;
	int ballw, ballh;
	
	
	double vx=0,vy=0;
	double m=1;
	double g=2;
	
	long t;
	
	Image basket;
	BufferedImage borders;
	BufferedImage ball;
	
	public BufferedImage toBufferedImage(Image img)
	{
	    if (img instanceof BufferedImage)
	    {
	        return (BufferedImage) img;
	    }

	    while( img.getHeight(this) == -1 ) sleep(5);
	    // Create a buffered image with transparency
	    BufferedImage bimage = new BufferedImage(img.getWidth(this), img.getHeight(this), BufferedImage.TYPE_INT_ARGB);

	    // Draw the image on to the buffered image
	    Graphics2D bGr = bimage.createGraphics();
	    bGr.drawImage(img, 0, 0, null);
	    bGr.dispose();

	    // Return the buffered image
	    return bimage;
	}
	
	private void init()
	{
		basket = Toolkit.getDefaultToolkit().getImage("Images/basket.jpg");
		borders = toBufferedImage( Toolkit.getDefaultToolkit().getImage("Images/borders.png") );
		ball = toBufferedImage( Toolkit.getDefaultToolkit().getImage("Images/ball.png") );
		
		imagew = basket.getWidth(this)*2;
		imageh = basket.getHeight(this)*2;
		
		ballw = ball.getWidth(this);
		ballh = ball.getHeight(this);
		
		ballx = this.getWidth()*2/5;
		bally = this.getHeight()*1/20;
		
		vx = -300;
		
	}
	
	private void addGravity( int deltat )
	{
		vy += g*deltat;
		//ballx -=2;
	}
	
	private void lookForColl( int deltat ) // doesnt covers multiple colliding spots
	{
		long sumx=0,sumy=0,c = 0;
		for( int i=0; i<ballw; i++ ) // x
			for( int j=0; j<ballh; j++ ) // y
			{
				if( ball.getRGB(i, j) != 0 && ball.getRGB(i, j) != Color.white.getRGB() && ballx + i - 20 >= 0 && ballx + i -20 < borders.getWidth() && bally + j - 20 >= 0 && bally + j - 20 < borders.getHeight() && borders.getRGB((int)ballx + i - 20, (int)bally + j - 20) != -1 && borders.getRGB((int)ballx + i - 20, (int)bally + j - 20) != -65281  )
				{
					sumx += (long)ballx + i - 20;
					sumy += (long)bally + j - 20;
					c++;
				}
			}
		if( c == 0 )
			flag = false;
		if( c != 0 && flag == false )
		{
			lx = (int)(sumx/c);
			ly = (int)(sumy/c);
			
			double ballcx = ballx + ballw/2;
			double ballcy = bally + ballh/2;
			
			double tanalfa = ( ly - ballcy ) / ( ballcx - lx );
			
			double hip = Math.sqrt(( vx*vx ) + ( vy*vy ));
			
			double sinalfa = Math.sin( Math.atan( tanalfa ) );
			double cosalfa = Math.cos( Math.atan( tanalfa ) );
			
			vx = -hip*sinalfa;
			vy = -hip*cosalfa;
			
			flag = true;
		}
		else if( bally + ballh > this.getHeight() - 20 )
		{
			vy = -vy*4/5;
			bally = this.getHeight() - ballh - 20;
		}
	}
	
	boolean flag = false;
	
	private void UpdateCoords( int deltat )
	{
		ballx += vx*deltat/1000;
		bally += vy*deltat/1000;
	}
	
	private void updateGame()
	{	
		int deltat = (int) (((new Date()).getTime() - t ) / 1000);
		t += deltat;
		addGravity( deltat ); // updates ball mv and coordinates
		lookForColl( deltat ); // looks for coll and if founds, updates mv
		UpdateCoords( deltat ); // Updates Coordinates with curr mv
		
	}
	
	
	@Override
	public void paint(Graphics g) {
		super.paint(g);
		
		Graphics2D g2d = (Graphics2D) g;
		g2d.setRenderingHint(RenderingHints.KEY_ANTIALIASING,
				RenderingHints.VALUE_ANTIALIAS_ON);
		
		
		g2d.drawString("" + fps, this.getWidth()-150, 50);
		g2d.drawString("" + "meme", this.getWidth()-150, 150);
		
		
		
		
		g2d.drawImage(basket, 20, 20, imagew, imageh,this);
		g2d.drawImage(ball, (int)ballx, (int)bally, ballw, ballh, this);
		
		g2d.fillOval(lx, ly, 20, 20);
		
		g2d.finalize();
	}
	
	int lx,ly;
	
	
	public static void main( String[] args )
	{
		TryHardGame game = new TryHardGame();
		
		JFrame frame = new JFrame("Basket Score'er");
		frame.setSize(szx, szy);
		frame.setVisible(true);
		frame.add(game);
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		
		int loopCounter = 0;
		
		game.t = (new Date()).getTime();
		game.init();
		game.repaint();
		game.sleep(500);
		game.init();
		while( true )
		{
			
			game.updateGame();
			game.repaint();
			loopCounter++;
			if( game.lt + 1000 < (new Date()).getTime() )
			{
				game.fps = loopCounter;
				loopCounter = 0;
				game.lt = (new Date()).getTime();
			}
			game.sleep(2);
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
	
	boolean isClicked = false;
	double x, y;

	@Override
	public void mouseClicked(MouseEvent e) {
		if( isClicked == false )
		{
			PointerInfo a = MouseInfo.getPointerInfo();
			Point b = a.getLocation();
			x = b.getX();
			y = b.getY();
			
			isClicked = true;
		}
		else
		{
			PointerInfo a = MouseInfo.getPointerInfo();
			Point b = a.getLocation();
			vx = x - b.getX();
			vy = y - b.getY();
			
			isClicked = false;
		}
		
	}

	@Override
	public void mouseEntered(MouseEvent e) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void mouseExited(MouseEvent e) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void mousePressed(MouseEvent e) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void mouseReleased(MouseEvent e) {
		// TODO Auto-generated method stub
		
	}
	
	
}
