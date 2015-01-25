package com.mmmeeedddsss;

import java.io.File;
import java.io.FileOutputStream;
import java.net.URL;
import java.net.URLConnection;
import java.nio.channels.Channels;
import java.nio.channels.ReadableByteChannel;
import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class PostDownloader {
	
	public static void main(String[] args)
	{
		String content = null;
		String query = "";
		URLConnection connection = null;
		Scanner scanner = new Scanner(System.in);
		System.out.println("Type the url which do you want to download posts without www\n(-ex 9gag.com or 9gag.com/trending-) ");
		String siteurl = scanner.nextLine();
		System.out.println("How many pages do you want to download?");
		int a = scanner.nextInt();
		
		if( a <= 0 ){ System.out.println("Zaaa iks de."); System.exit(0); }
		File f = new File(System.getProperty("user.dir")+ "\\Posts");
		if( !f.exists() )
			f.mkdir();
		for(int m=0;m<a;m++)
		{
			try {
			  connection =  new URL("http://" + siteurl + query).openConnection();
			  System.out.println(siteurl + query);
			  scanner = new Scanner(connection.getInputStream());
			  scanner.useDelimiter("\\Z");
			  content = scanner.next();
			 
			  //content = "<i>asadsad</i>";
			  
			  Pattern pattern = Pattern.compile("<img src=\"(.*?)\" alt=\"(.*?)\"/>");
			  Matcher matcher = pattern.matcher(content);
			  
			  Pattern pattern2 = Pattern.compile("<a id=\"jump_next\" class=\'next\' href=\"(.*?)\">");
			  Matcher nextpage = pattern2.matcher(content);
			  
			  nextpage.find();
			  query = nextpage.group(1);
			  siteurl = "9gag.com";
			  
			  while (matcher.find()) {
				  try {
			             URL dl = null;
			             String filename = matcher.group(2);
			             
			             if( filename.equals("NSFW") || filename.contains("NSFW") || filename.compareTo("NSWF") == 0 )
			             {
			            	 System.out.println("NSFW    -    Aborted");
			            	 continue;
			             }
			             else if(filename.equals("Quantcast") || filename.contains("Quantcast") || filename.compareTo("Quantcast") == 0)
			             {
			            	 continue;
			             }
			             
			             StringBuffer temp = new StringBuffer(filename);
			             for( int i=0;i<temp.length();i++ )
			             {
			            	 if(     temp.charAt(i) == '?' ||
			            			 temp.charAt(i) == '\\' ||
			            			 temp.charAt(i) == '/' ||
			            			 temp.charAt(i) == '*' ||
			            			 temp.charAt(i) == '|' ||
			            			 temp.charAt(i) == '<' ||
			            			 temp.charAt(i) == '>' ||
			            			 temp.charAt(i) == '"' ||
			            			 temp.charAt(i) == ':'
			            		)
			            		 temp.setCharAt(i, '.');
			             }
			             filename = temp.toString();
			             
			             System.out.print(filename);
			             dl = new URL(matcher.group(1));
			             ReadableByteChannel rbc = Channels.newChannel(dl.openStream());
			             FileOutputStream fos = new FileOutputStream(f.getAbsoluteFile() + "\\" + filename + ".jpg");
			             fos.getChannel().transferFrom(rbc, 0, 1 << 24);
			             fos.flush();
			             fos.close();
			             rbc.close();
			             System.out.print("    -    Ok!\n");
			         } catch (Exception e) {
			        	 System.out.print("    -    Error :(\n");
			       }
				}
			  
			}catch ( Exception ex ) {
			    ex.printStackTrace();
			}
		}
		System.out.println("Download Completed!");
		System.exit(0);
	}
	
}

