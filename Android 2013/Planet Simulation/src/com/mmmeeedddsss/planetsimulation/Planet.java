package com.mmmeeedddsss.planetsimulation;

import java.util.LinkedList;

import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Point;
import android.graphics.PointF;
import android.graphics.RectF;

public class Planet {
	public static final int w = 35;
	public static final int h = 35;
	private float x;
	private float y;
	private PointF spd;
	private int m;
	private Bitmap planetTexture;
	//private Point screen;
	
	public Planet(int x,int y,int m,Point spd,Bitmap b,Point screen)
	{
		this.x = x;
		this.y = y;
		this.m = m;
		this.spd = new PointF(spd);
		planetTexture = b;
		//this.screen = screen;
	}

	public void addVelocity(PointF sumVector) {
		spd.x += sumVector.x;
		spd.y += sumVector.y;
		//Log.w("vel", sumVector.x + ", " + sumVector.y);
	}

	public RectF getDrawRect() {
		return new RectF(x,y,x+w,y+h);
	}

	public void draw(Canvas c) {
		c.drawBitmap(planetTexture, null, getDrawRect(), null);
	}

	public void update() {
		x += ((spd.x / 2500));
		y += ((spd.y / 2500));
		/*
		if( x < 0 )
		{
			spd.x = -spd.x;
			x = 0;
		}
		if( x + w> screen.x )
		{
			spd.x = -spd.x;
			x = screen.x - w;
		}
		if( y < 0 )
		{
			spd.y = -spd.y;
			y = 0;
		}
		if( y + h > screen.y )
		{
			spd.y = -spd.y;
			y = screen.y - h;
		}
		*/
	}
	
	public static PointF CalcVelocityVector(Planet p1, Planet p2, int time) {
		try{
			PointF vector = new PointF(0,0);
			float xw = Math.abs(p1.x - p2.x);
			float xh = Math.abs(p1.y - p2.y);
			float normHip = (float) Math.sqrt(xh*xh + xw*xw);
			float hip = ((p1.m*p2.m) / normHip*normHip );
			
			vector.x = (( hip / normHip ) * xw) / time;
			vector.y = (( hip / normHip ) * xh) / time;
			if( p1.x > p2.x ) // p1 saðda
				vector.x = -vector.x;
			if( p1.y > p2.y )
				vector.y = -vector.y;
			
			return vector;
		}
		catch(Exception e)
		{
			return new PointF(0,0);
		}
	}

	public static PointF SumVectors(LinkedList<PointF> vectors) {
		PointF sum = new PointF(0,0);
		for( int i=0; i<vectors.size(); i++ )
		{
			PointF temp = vectors.get(i);
			sum.x += temp.x;
			sum.y += temp.y;
		}
		return sum;
	}
	
}
