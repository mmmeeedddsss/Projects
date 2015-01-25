package com.mmmeeedddsss.planetsimulation;

import android.app.Activity;
import android.os.Bundle;
import android.view.Window;
import android.view.WindowManager;
import android.widget.Toast;

public class PhysicsActivity extends Activity {
    
	MSurfaceView sw;
	
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        try
        {
        	this.requestWindowFeature(Window.FEATURE_NO_TITLE);
 	        this.getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);
	        sw = new MSurfaceView( getApplicationContext() );
	        sw.loadBundle();
	        setContentView(sw);
        }
        catch( Exception e )
        {
        	Toast.makeText(getApplicationContext(), "Error : "+e.getMessage(), 5000).show();
        }
    }

	@Override
	protected void onPause() {
		super.onPause();
		sw.pause();
	}

	@Override
	protected void onResume() {
		super.onResume();
		sw.resume();
	}
	
}