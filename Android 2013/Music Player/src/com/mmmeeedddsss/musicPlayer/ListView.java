package com.mmmeeedddsss.musicPlayer;

import java.io.File;

import android.app.ListActivity;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.TextView;
import android.widget.Toast;

public class ListView extends ListActivity {

	
	String[] items = null;
	String[] itemsf = null;
	
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		try
		{
			//setContentView(R.layout.listview);
			super.onCreate(savedInstanceState);
			Bundle b = getIntent().getExtras();
			items = (String[]) b.get("items");
			final String currDir = b.getString("currdir");
			setTitle(currDir);
			/*
			items = new String[itemsf.length+1];
			items[0] = "Set Directory";
			for(int i=1;i<=items.length;i++)
			{
				items[i] = itemsf[i-1];
			}
			*/
			
			setListAdapter(new ArrayAdapter<String>(this, R.layout.singleitem, items));
			
			android.widget.ListView lw = getListView();
			lw.setTextFilterEnabled(true);
			lw.setOnItemClickListener(new OnItemClickListener(){
	
				@Override
				public void onItemClick(AdapterView<?> arg0, View arg1, int arg2,
						long arg3) {
						if( arg3 == 0 )
						{
							File dir = new File(currDir);
							Intent i = new Intent(getApplicationContext(),musicPlayer.class);
							Bundle b = new Bundle();
							b.putString("dir", currDir);
							b.putStringArray("files",dir.list() );
							Toast.makeText(getApplicationContext(),currDir , Toast.LENGTH_SHORT).show();
							i.putExtras(b);
							startActivity(i);
						}
						else
						{
							String nextdir = currDir+"/"+((TextView)arg1).getText();
							
							File dir = new File(nextdir);
							if( dir == null || dir.list() == null )
							{
								Toast.makeText(getApplicationContext(),"File Isnt a Directory :(" , Toast.LENGTH_SHORT).show();
							}
							else
							{
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
								b.putString("currdir", nextdir);
								i.putExtras(b);
								startActivity(i);
							}
						}
				}
			});
		}
		catch(Exception ex)
		{
			Toast.makeText(getApplicationContext(),"Cant Open File :(" , Toast.LENGTH_SHORT).show();
			finish();
		}
	}
}
