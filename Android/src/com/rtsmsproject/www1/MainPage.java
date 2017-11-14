package com.rtsmsproject.www1;


import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.Reader;
import java.net.URI;

import org.apache.http.HttpResponse;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;

import com.google.gson.Gson;
import com.rtsmsproject.www1.Login;
import com.rtsmsproject.www1.classes.AppTabs;
import com.rtsmsproject.www1.classes.UserProfile;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.view.View;
import android.widget.EditText;
import android.widget.TabHost;
import android.widget.TextView;
import android.widget.Toast;

public class MainPage extends Activity {
	private static final String NULL = null;

	static final private int CHOOSE_COLOR=0;
	  // All objects are from android.context.Context
    public static final String PREFS_NAME = "MyPrefsFile";
  
	public boolean check_user;
    /** Called when the activity is first created. */
	
	public void onBackPressed()
	{
		this.finish();
	}
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main);
       
//        final ProgressDialog dialog = ProgressDialog.show(this,"", "Loading Application");
        
    	Handler handler = new Handler(); 
	      handler.postDelayed(new Runnable() { 
	           public void run() {
	        	  CallNextPage();
	           //dialog.dismiss();
	     	        } 
	      }, 1000);


    }
    private void CallNextPage()
    {
        Intent next = new Intent(this,com.rtsmsproject.www1.HomeStatus.class);
        this.startActivity(next);
        this.finish();
    }
    // Helper function to reset the label colors
    private void resetColors()
    {
/*    	TextView tv;
    	
    	// Go through all labels and set their color
    	// back to a neutral gray
    	tv = (TextView)findViewById(R.id.labelRed);
    	tv.setTextColor(Color.parseColor("#666666"));
    	tv = (TextView)findViewById(R.id.labelGreen);
    	tv.setTextColor(Color.parseColor("#666666"));
    	tv = (TextView)findViewById(R.id.labelBlue);
    	tv.setTextColor(Color.parseColor("#666666"));
  */  }
    
    // Implementation of the button click handler defined in main.xml
    public void loginClickHandler(View target)
    {
    /*	Intent question = new Intent(this,com.rtsmsproject.www1.Current.class);
    	startActivity(question);
   */ 	
  //  	LoginValidate();
    	Intent question = new Intent(this, com.rtsmsproject.www1.HomeStatus.class);
    	// Start ColorPicker as a new activity and wait for the result 
    	this.startActivity(question);
    /*
    	if (check_user == true)
    	{
    		EditText USERNAME_LOGIN = (EditText)findViewById(R.id.login_username);
    		//DownloadUserProfile(USERNAME_LOGIN.getText().toString());
        	// Create new intent object and tell it to call the ColorPicker class
        	Intent question = new Intent(this, com.rtsmsproject.www1.HomeStatus.class);
        	// Start ColorPicker as a new activity and wait for the result 
        	this.startActivity(question);
        	//startActivityForResult(question, CHOOSE_COLOR);
    	} else if ( check_user == false)
    	{
    		Toast.makeText(getApplicationContext(), "Invalid UserName or Password Combination !", 5).show();
    	}*/
    }

	public InputStream getJSONData(String url){
        DefaultHttpClient httpClient = new DefaultHttpClient();
        URI uri;
        InputStream data = null;
        try {
            uri = new URI(url);
            HttpGet method = new HttpGet(uri);
            HttpResponse response = httpClient.execute(method);
            data = response.getEntity().getContent();
        } catch (Exception e) {
            e.printStackTrace();
        }
        httpClient.removeRequestInterceptorByClass(null);
        return data;
    }

	private void LoginValidate()
	{
		//EditText USERNAME_LOGIN = (EditText)findViewById(R.id.login_username);
		//EditText PASSWORD_LOGIN = (EditText)findViewById(R.id.login_password);
		
		//String url = "http://rtsms.somee.com/WebService.svc/Login/?usr="+USERNAME_LOGIN.getText().toString()+"&pass="+PASSWORD_LOGIN.getText().toString();
		try
		{
			Gson  json = new Gson();
		/*	Reader r = new InputStreamReader(getJSONData(url));
			Login obj = json.fromJson(r, Login.class);
			   
			if (obj.getLoginResult() == "true")
			{
				check_user = true;
			} else 
			if ( obj.getLoginResult() == "false")
			{
				check_user = false;
			}*/
			check_user = true;
		}
		catch(Exception ex){
			ex.printStackTrace();
		}
	}
	public void DownloadUserProfile(String uname)
	{
		String url = "http://rtsms.somee.com/WebService.svc/GetUserProfile/"+uname;
		try
		{
			Gson  json = new Gson();
			Reader r = new InputStreamReader(getJSONData(url));
			UserProfile obj = json.fromJson(r, UserProfile.class);

			SharedPreferences settings = getSharedPreferences(PREFS_NAME, 0);
		    SharedPreferences.Editor editor = settings.edit();
		    editor.putString("UserID", "0");
		    editor.putString("UserName", obj.getUserName());
		    editor.putString("FirstName", obj.getFirstName());
		    editor.putString("LastName", obj.getLastName());
		    editor.putString("Password", obj.getPassword());
		    editor.putString("Email", obj.getEmail());
		    editor.putString("HomeLocation", obj.getHomeLocation());
		    editor.putString("OfficeLocation", obj.getOfficeLocation());
			// Commit the edits!
		    editor.commit();
			   
		}
		catch(Exception ex){
			ex.printStackTrace();
		}
	}

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
    	// Check which activity returned data to us
    	switch(requestCode)
    	{
    	case CHOOSE_COLOR:
    		// only proceed if the result was RESULT_OK
    		if(resultCode == RESULT_OK)
    		{
    			
    			// Extract the data from the returned object and store it
    			// into variables
    /*			String color = data.getExtras().getString("color");
    			int label = data.getExtras().getInt("label");
    			
    			// Use the extracted data to set a new color
    			// on the corresponding label
    			TextView tv = (TextView)findViewById(label);
    			tv.setTextColor(Color.parseColor(color));*/
    		}
    		break;
    	
    	default:
    		break;
    	}
    }
}