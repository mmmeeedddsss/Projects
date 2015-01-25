package com.mmmeeedddsss;

import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;
import java.nio.channels.Channels;
import java.nio.channels.ReadableByteChannel;
import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import android.app.Activity;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

public class nineGagPostViewerActivity extends Activity {
    

	Button next;
	Button prev;
	ImageView image;
	TextView tv;
	
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.viewer);
        image = ( ImageView ) findViewById(R.id.image);
        tv = (TextView) findViewById(R.id.tv);
        
		try {
			download(5);
		} catch (MalformedURLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
        
        File f = new File("//sdcard/wallpaper-50420.jpg");
        if( f.exists() )
        {
        	//Toast.makeText(getApplicationContext(), "a", Toast.LENGTH_SHORT).show();
        	Bitmap Img = BitmapFactory.decodeFile("//sdcard/wallpaper-50420.jpg");
        	image.setImageBitmap(Img);
        	image.setMaxHeight(1000);
        	image.setMinimumHeight(1000);
        }
        
    }
    
    public void download(int n) throws MalformedURLException, IOException
    {
    	String content = null;
		String query = "";
		Scanner scanner;
		int postcount = 0;
		
		File f = new File("//sdcard/Posts");
		if( !f.exists() )
			f.mkdir();
		//while(postcount < n)
		if(true)
		{
			try {
			  //connection =  new URL("http://9gag.com" + query).openConnection();
			  URL url = new URL("http://9gag.com" + query);
			  URLConnection urlConnection = url.openConnection();
			  

			  final BufferedReader rd = new BufferedReader(new InputStreamReader(urlConnection.getInputStream()));

			  final StringBuffer stringBuffer = new StringBuffer();
			  String line;
			  while ((line = rd.readLine()) != null) {
				  stringBuffer.append(line);
			  }
			  rd.close();
			  
			  //tv.setText(stringBuffer.toString());
			  
			  
			  Pattern pattern = Pattern.compile("<img src=\"(.*?)\" alt=\"(.*?)\"/>");
			  Matcher matcher = pattern.matcher(content);
			  
			  Pattern pattern2 = Pattern.compile("<a id=\"jump_next\" class=\'next\' href=\"(.*?)\">");
			  Matcher nextpage = pattern2.matcher(content);
			  
			  
			  nextpage.find();
			  query = nextpage.group(1);
			  
			  while (matcher.find()) {
				  try {
			             URL dl = null;
			             dl = new URL(matcher.group(1));
			             
			             ReadableByteChannel rbc = Channels.newChannel(dl.openStream());
			             FileOutputStream fos = new FileOutputStream("//sdcard/Posts/" + postcount + ".jpg");
			             fos.getChannel().transferFrom(rbc, 0, 1 << 24);
			             fos.flush();
			             fos.close();
			             rbc.close();
			             postcount++;
			             if( postcount == n )
			            	 break;
			         } catch (Exception ex) {
			        	 Toast.makeText(getApplicationContext(), ex.getMessage(), Toast.LENGTH_SHORT).show();
			       }
				} 
			  
			}catch ( Exception ex ) {
				Toast.makeText(getApplicationContext(), ex.getMessage(), Toast.LENGTH_LONG).show();
			}
			
		} 
    }
}