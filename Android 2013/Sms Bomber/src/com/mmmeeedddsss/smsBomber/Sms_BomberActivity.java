package com.mmmeeedddsss.smsBomber;

import android.app.Activity;
import android.app.PendingIntent;
import android.content.Intent;
import android.os.Bundle;
import android.telephony.SmsManager;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class Sms_BomberActivity extends Activity {
    
	Button btnBomb;
    EditText txtPhoneNo;
    EditText txtMessage;
    EditText howManyTimes;
	
    @Override
    public void onCreate(Bundle savedInstanceState) {
        setContentView(R.layout.main);
        Button bsend = (Button) findViewById(R.id.btnSendSMS);
        txtPhoneNo = (EditText) findViewById(R.id.txtPhoneNo);
        txtMessage = (EditText) findViewById(R.id.txtMessage);
        howManyTimes = (EditText) findViewById(R.id.txtHowMany);
        btnBomb = (Button) findViewById(R.id.btnBomb);
        btnBomb.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				try
				{
					int hmt = Integer.parseInt(howManyTimes.getText().toString());
					for( int i=0;i<hmt;i++ )
					{
						send();
					}
				}
				catch( Exception e )
				{
					Toast.makeText(getBaseContext(), "Error! :S", Toast.LENGTH_SHORT).show();
				}
			}
		});
        bsend.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				String phoneNo = txtPhoneNo.getText().toString();
                String message = txtMessage.getText().toString();                 
                if (phoneNo.length()>0 && message.length()>0)                
                	send();
                else
                    Toast.makeText(getBaseContext(), 
                        "Please enter both phone number and message.", 
                        Toast.LENGTH_SHORT).show();
			}

        });
        super.onCreate(savedInstanceState);
    }
    
    
    public void send()
    {
    	// get the phone number from the phone number text field
        String phoneNumber = txtPhoneNo.getText().toString();
        // get the message from the message text box
        String msg = txtMessage.getText().toString();  

        // make sure the fields are not empty
        if (phoneNumber.length()>0 && msg.length()>0)
        {
        	// call the sms manager
            PendingIntent pi = PendingIntent.getActivity(this, 0,
                    new Intent(this, Sms_BomberActivity.class), 0);
                SmsManager sms = SmsManager.getDefault();
                // this is the function that does all the magic
                sms.sendTextMessage(phoneNumber, null, msg, pi, null);
        }
        else
        {
        	// display message if text fields are empty
            Toast.makeText(getBaseContext(),"All field are required",Toast.LENGTH_SHORT).show();
        }

    }
}