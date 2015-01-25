package com.mmmeeedddsss.musicPlayer;

import java.io.File;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

public class Menu extends Activity {
    /** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main);
        
        Button openListV = (Button) findViewById(R.id.btn);
        openListV.setOnClickListener( new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				File dir = new File("/");
				String[] ff = dir.list();
				String[] files = new String[ff.length+1];
				files[0] = "Set Directory";
				for(int i=1;i<ff.length+1;i++)
				{
					files[i] = ff[i-1];
				}
				
				Intent i = new Intent(getApplicationContext(), ListView.class);
				Bundle b = new Bundle();
				b.putStringArray("items",files);
				b.putString("currdir", "/");
				i.putExtras(b);
				startActivity(i);
			}
        });
    }
    
}