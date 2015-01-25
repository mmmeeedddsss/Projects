package com.mmmeeedddsss.musicPlayer;

import java.util.Random;
import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.MediaController;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.VideoView;


public class musicPlayer extends Activity {

	
	Button pp;
	TextView trackname;
	TextView lenght;
	Bundle b;
	VideoView videoView;
	MediaController mediaController;
	Random r;
	boolean onpause;
	int returnpoint;
	Button next;
	Button seek;
	EditText te;
	String[] files;
	
	
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		
		setContentView(R.layout.musicplayer);
		try
		{ // adamýn seçtii directorydeki müzikleri random oynatýyo kendisi
			pp = (Button) findViewById(R.id.pp);
			next = (Button) findViewById(R.id.next);
			seek = (Button) findViewById(R.id.seek);
			trackname = (TextView) findViewById(R.id.trackname);
			trackname = (TextView) findViewById(R.id.trackname);
			te = (EditText) findViewById(R.id.to);
			b = getIntent().getExtras();
			r = new Random();
			files = b.getStringArray("files"); // hata bu files de ama neden hata var anlamýyorum :D Yada Burda Diil Ben Malým :D
			
			Toast.makeText(getApplicationContext(), files.length, Toast.LENGTH_SHORT).show();
			
			pp.setOnClickListener(new View.OnClickListener() {
				@Override
				public void onClick(View v) {
					if( videoView.isPlaying() == true )
					{
						videoView.pause();
						onpause = true;
						returnpoint = videoView.getCurrentPosition();
					}
					else if( onpause )
					{
						videoView.start();
						videoView.seekTo(returnpoint);
					}
					else
						playrand();
				}
			});
			
			next.setOnClickListener(new View.OnClickListener() {
				
				@Override
				public void onClick(View v) {
					playrand();
				}
			});
			
			playrand();
			
		}
		catch(Exception ex)
		{
			Toast.makeText(getApplicationContext(), "a", Toast.LENGTH_SHORT).show();
		}
		super.onCreate(savedInstanceState);
	}
	
	protected void playrand()
	{
		try
		{ // oynatmaya baþlýyo
			onpause = false;
			int id = r.nextInt(files.length);
			String TrackName = files[id];
			trackname.setText(TrackName);
			videoView = (VideoView) findViewById(R.id.vv);
	        mediaController = (MediaController) findViewById(R.id.mc);
	        mediaController.setMediaPlayer(videoView);
	        videoView.setVideoPath(b.getString("dir") + "/" + files[id]);
	        videoView.setMediaController(mediaController);
	        videoView.requestFocus();
	        videoView.start();
	        mediaController.show();
		}
		catch(Exception ex)
		{
			//Toast.makeText(getApplicationContext(), Cant Play", Toast.LENGTH_SHORT).show();
			//playrand();
		}
	}
	
}
