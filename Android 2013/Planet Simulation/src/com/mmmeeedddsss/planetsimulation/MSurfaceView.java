package com.mmmeeedddsss.planetsimulation;

import android.content.Context;
import android.graphics.Canvas;
import android.os.Bundle;
import android.view.MotionEvent;
import android.view.SurfaceHolder;
import android.view.SurfaceView;
import android.view.View;
import android.view.View.OnTouchListener;

public class MSurfaceView extends SurfaceView implements Runnable,OnTouchListener{

	boolean isrunning;
	SurfaceHolder holder;
	GameEngine game;

	public MSurfaceView(final Context context) {
		super(context);
		holder = getHolder();
		isrunning = true;
		game = new GameEngine(getResources());
		
		this.setOnTouchListener( game );
	}
	
	public void setBundle( Bundle b )
	{
		game.setBundle(b);
	}
	
	public void loadBundle()
	{
		game.loadBundle();
	}

	private void update() 
	{
		game.update();
	}
	private void Init() 
	{
		game.Init(holder.getSurfaceFrame().width(),holder.getSurfaceFrame().height());
	}
	public void draw( Canvas c )
	{
		c.drawARGB(50, 0, 0, 0); // Clear Screen
		game.draw(c);
	}
	@Override
	public void run() {
		while( !holder.getSurface().isValid() ) try {
				Thread.sleep(50);
			} catch (InterruptedException e) {}
			
		Init();
		while( isrunning )
		{
			if( !holder.getSurface().isValid() )
				continue;
			Canvas c = holder.lockCanvas();
			update();
			draw(c);
			holder.unlockCanvasAndPost(c);
		}
	}
	public void pause()
	{
		isrunning = false;
	}
	public void resume()
	{
		isrunning = true;
		new Thread(this).start();
	}

	@Override
	public boolean onTouch(View v, MotionEvent event) {
		isrunning = true;
		loadBundle();
		new Thread(this).start();
		return false;
	}
}