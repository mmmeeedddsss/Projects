package com.mmmeeedddsss.nineGag;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.URL;
import java.util.regex.*;

import android.app.Activity;
import android.os.Bundle;
import android.widget.Toast;

public class NineGagPostVieverActivity extends Activity {
	
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main);
        
        String htmlCode = "";

        try {
        URL url = new URL("http://www.google.com/");
        BufferedReader in = new BufferedReader(new InputStreamReader(url.openStream()));

        String inputLine;

        while ((inputLine = in.readLine()) != null)
            htmlCode += inputLine;

        Toast.makeText(getApplicationContext(),inputLine, Toast.LENGTH_LONG).show();
        in.close();
        
        Pattern p = Pattern.compile("<(.*?)>");
        Matcher m = p.matcher(inputLine);
        while(!m.hitEnd())
        {
        	m.find();
        	Toast.makeText(getApplicationContext(),m.group(), Toast.LENGTH_LONG).show();
        }
        
        } catch (Exception e) {
            Toast.makeText(getApplicationContext(),e.getMessage(), Toast.LENGTH_SHORT).show();
        }
        
    }
}