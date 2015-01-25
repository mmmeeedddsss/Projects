package com.mmmeeedddsss.planetsimulation;

import java.util.LinkedList;
import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Paint;
import android.graphics.Point;
import android.graphics.PointF;
import android.os.Bundle;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;

public class GameEngine implements OnTouchListener {

	Point screen;
	LinkedList<Planet> planets;
	Bitmap planetTexture;
	Bundle b;
	Resources res;
	private int time;
	private Paint line;
	
	public GameEngine(Resources r) {
		res = r;
		line = new Paint();
		line.setARGB(255, 0, 0, 255);
		line.setStrokeWidth(5);
	}

	public void Init(int x, int y) {
		screen = new Point(x,y);
		planets = new LinkedList<Planet>();
		planetTexture = BitmapFactory.decodeResource(res, R.drawable.ball);
	}
	
	public void update() {
		for( int i=0; i<planets.size(); i++ )
		{
			LinkedList<PointF> vectors = new LinkedList<PointF>();
			for( int j=0; j<planets.size(); j++ )
			{
				if( i != j )
				{
					vectors.add(Planet.CalcVelocityVector(planets.get(i),planets.get(j),2*30));
				}
			}
			planets.get(i).addVelocity(Planet.SumVectors(vectors));
		}
		time += 0.2;
		for( int i=0; i<planets.size(); i++ )
			planets.get(i).update();
	}
	
	public void draw(Canvas c) {
		for(int i=0; i<planets.size(); i++)
			planets.get(i).draw(c);
		if(spdLineFlag)
			c.drawLine(npx, npy, tonpx, tonpy, line);
	}
	
	public void setBundle(Bundle b) {
		//this.b = b;
	}

	int npx,npy;
	int tonpx,tonpy;
	private boolean spdLineFlag = false;
	@Override
	public boolean onTouch(View v, MotionEvent e) {
		switch(e.getAction())
		{
			case MotionEvent.ACTION_DOWN:
				spdLineFlag  = true;
				npy = (int)e.getY();
				npx = (int)e.getX();
				break;
			case MotionEvent.ACTION_MOVE:
				tonpy = (int)e.getY();
				tonpx = (int)e.getX();
				break;
			case MotionEvent.ACTION_UP:
				spdLineFlag = false;
				planets.add(new Planet(npx, npy, 100,new Point (((int)e.getX() - npx)*25,((int) e.getY() - npy)*25), planetTexture , screen));
				break;
		}
		return true;
	}

	public void loadBundle() {
		// TODO Auto-generated method stub
		
	}

}
