package com.rtsmsproject.www1;

import java.io.IOException;
import java.io.InputStreamReader;
import java.io.Reader;
import java.util.List;
import java.util.Locale;

import com.google.gson.Gson;
import com.rtsmsproject.www1.Current.MyLocationListener;
import com.rtsmsproject.www1.MyService;
import com.rtsmsproject.www1.classes.CollectionMyNearBy;
import com.rtsmsproject.www1.classes.DownloadJson;
import com.rtsmsproject.www1.classes.Homestatus;
import com.rtsmsproject.www1.classes.Status;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.ListActivity;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.ActivityInfo;
import android.content.res.Configuration;
import android.content.res.Resources;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteException;
import android.location.Address;
import android.location.Geocoder;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.os.Handler;
import android.preference.PreferenceManager;
import android.util.Log;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

public class HomeStatus extends Activity {
	public static String CurrentLocation = null;
	private final String SAMPLE_DB_NAME = "rtsmsDB";
	private final String SAMPLE_TABLE_NAME = "Current_Location";
    private SQLiteDatabase sampleDB = null;
    // We need an Editor object to make preference changes.
    // All objects are from android.context.Context
    public static final String PREFS_NAME = "MyPrefsFile";
    private List<Address> addresses = null;

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.homemenu, menu);
        return true;
    }
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case R.id.home:             
            				{
/*            					Intent intent = new Intent(this, com.rtsmsproject.www1.HomePage.class);
            					Toast.makeText(getApplicationContext(), "Home Page is Loading...", 1).show();
            					startActivity(intent);
 */           				}	break;
            case R.id.maps:     
            				{
            					Intent intent = new Intent(this, com.rtsmsproject.www1.CircleOverlay.class);
            					startActivity(intent);
            				}	break;
            case R.id.help:
            				{
            					Intent intent = new Intent(this, com.rtsmsproject.www1.ImageActivity.class);
            					//Toast.makeText(getApplicationContext(), "Help is Loading...", 1).show();
            					startActivity(intent);
            				}	break;
            case R.id.settingsopt : 
            						{
            				        	Intent intent = new Intent(this, com.rtsmsproject.www1.SettingsPanel.class);
                				//		Toast.makeText(getApplicationContext(), "Settings Panel Loading...", 1).show();
            				        	startActivity(intent);

            						}	break;
            case R.id.logout: {
            						this.stopService(new Intent(this, MyService.class));
            						this.finish();
            				  } break;
        }
        return true;
    }
    @Override
    public void onConfigurationChanged(Configuration newConfig) {
        super.onConfigurationChanged(newConfig);
        setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
    }

	protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.homestatus);
        
        Resources res = getResources();
        Configuration cfg = res.getConfiguration();
        boolean hor = cfg.orientation == Configuration.ORIENTATION_PORTRAIT;
        // Start the RTSMS Background Service for downloading latest news
        
        this.startService(new Intent(this, MyService.class));

        		
        final Handler handler=new Handler();
        final Runnable r = new Runnable()
        {
            public void run() 
            {
	                GetHomeStatusfromNetwork();
	                handler.postDelayed(this, 5*1000);
            }
        };

        handler.postDelayed(r, 5*1000);

        SharedPreferences settings = getSharedPreferences(PREFS_NAME, 0);
        //Geocoder geocoder = new Geocoder(this, Locale.getDefault());
		/*try {
		//	addresses = geocoder.getFromLocation(Double.valueOf(settings.getString("Lattitude", "0.0")), Double.valueOf(settings.getString("Longitude", "0.0")) , 1);
		} catch (NumberFormatException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}*/
		/* Use the LocationManager class to obtain GPS locations */
		LocationManager mlocManager = (LocationManager)getSystemService(Context.LOCATION_SERVICE);
		LocationListener mlocListener = new MyLocationListener();
		mlocManager.requestLocationUpdates( LocationManager.NETWORK_PROVIDER, 0, 0, mlocListener);
	}
	public void onBackPressed()
	{
		this.finish();
	}
	private void GetHomeStatusfromNetwork()
	{
        // Restore preferences
		SharedPreferences settings = getSharedPreferences(PREFS_NAME, 0);
//////////////////////////for getting settings panel values
		SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(getBaseContext());
        String ListDistance = prefs.getString("listDistance","2");
        // listUnit  == 1 Miles , == 2 Kilometers
        Integer listUnit = Integer.parseInt(prefs.getString("listUnit","1"));
        

		String url = SettingUrls.webserverurl+"/GetHomeStatus/?latt="+Double.valueOf(settings.getString("Lattitude", "0.0"))+"&lngt="+Double.valueOf(settings.getString("Longitude", "0.0"));
		try
		{
 			Gson  json = new Gson();
			Reader r = new InputStreamReader(DownloadJson.getJSONData(url));
			Homestatus obj = json.fromJson(r, Homestatus.class);
	        ImageView safe = (ImageView)findViewById(R.id.safe_bar);
	        ImageView unsafe = (ImageView)findViewById(R.id.unsafe_bar);
			TextView status = (TextView)findViewById(R.id.home_status);
			Boolean val;
			val = obj.GetHomePageStatusResult.getAlertType();
	    	if (val == true)
        	{
        		status.setText("You are Safe");
        		unsafe.setVisibility(View.INVISIBLE);
        		safe.setVisibility(View.VISIBLE);
        	}
        	else {
        		unsafe.setVisibility(View.VISIBLE);
        		safe.setVisibility(View.INVISIBLE);
        		status.setText("You are UnSafe");
        	}
		}
		catch(Exception ex){
			ex.printStackTrace();
			//items.add(new MenuItems("Error", "Roadblock", 0));

		}
	}
	public void refereshbtnhandler(View target)
	{
		GetHomeStatusfromNetwork();
	}
	public void dontgoClickHandler(View target)
	{
		 // Create new intent object and tell it to call the ColorPicker class
	      	Intent question = new Intent(this, com.rtsmsproject.www1.DontGo.class);
	   	// Start ColorPicker as a new activity and wait for the result 
	       	this.startActivity(question);
	}
	public void catesClickHandler(View target)
	{
		 // Create new intent object and tell it to call the ColorPicker class
	      	Intent question = new Intent(this, com.rtsmsproject.www1.LastCates.class);
	   	// Start ColorPicker as a new activity and wait for the result 
	       	this.startActivity(question);
	}
	public void nearbyClickHandler(View target)
	{
		 // Create new intent object and tell it to call the ColorPicker class
	      	Intent question = new Intent(this, com.rtsmsproject.www1.MyNearBy.class);
	   	// Start ColorPicker as a new activity and wait for the result 
	       	this.startActivity(question);
	}
	public void homepageClickHandler(View target)
	{
		 // Create new intent object and tell it to call the ColorPicker class
	      	Intent question = new Intent(this, com.rtsmsproject.www1.HomePage.class);
	   	// Start ColorPicker as a new activity and wait for the result 
	       	this.startActivity(question);
	}

	
	/* Class My Location Listener */
	public class MyLocationListener implements LocationListener
	{
		public void onLocationChanged(Location loc)
		{
			   SharedPreferences settings = getSharedPreferences(PREFS_NAME, 0);
		       SharedPreferences.Editor editor = settings.edit();
		       editor.putString("Lattitude", String.valueOf(loc.getLatitude()));
		       editor.putString("Longitude", String.valueOf(loc.getLongitude()));
		       editor.putString("Alltitude", String.valueOf(loc.getAltitude()));
		       // Commit the edits!
		       editor.commit();
		}
		public void onProviderDisabled(String provider)
		{
			showGPSDisabledAlertToUser();
			Toast.makeText( getApplicationContext(),"AGps Disabled",Toast.LENGTH_SHORT ).show();
		}
		public void onProviderEnabled(String provider)
		{
			Toast.makeText( getApplicationContext(),"AGps Enabled",	Toast.LENGTH_SHORT).show();
		}
		public void onStatusChanged(String provider, int status, Bundle extras)
		{
		}
	}/* End of Class MyLocationListener */
	private void showGPSDisabledAlertToUser(){
		AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
			alertDialogBuilder.setMessage("AGPS is disabled in your device. Would you like to enable it?")
		     .setCancelable(false)
		     .setPositiveButton("Goto Settings Page To Enable AGPS",
		          new DialogInterface.OnClickListener(){
		          public void onClick(DialogInterface dialog, int id){
		        	  Intent callGPSSettingIntent = new Intent(
		  					android.provider.Settings.ACTION_LOCATION_SOURCE_SETTINGS);
		  			startActivity(callGPSSettingIntent);
		          }
		     });
		     alertDialogBuilder.setNegativeButton("Cancel",
		          new DialogInterface.OnClickListener(){
		          public void onClick(DialogInterface dialog, int id){
		               dialog.cancel();
		          }
		     });
		AlertDialog alert = alertDialogBuilder.create();
		alert.show();
		}
	private String GetLocationName(double latti,double longi)
	{
		String result = null;
		String url = "http://localhost/RTSMSWebService/WebService.svc/GetLocationName/?longitude="+longi+"&lattitude="+latti;
		try
		{
			Gson  json = new Gson();
			Reader r = new InputStreamReader(DownloadJson.getJSONData(url));
			ReverseGeoCoding obj = json.fromJson(r, ReverseGeoCoding.class);
			result = obj.GetLocationNameResult();
		}
		catch(Exception ex){
			result = "null";
			ex.printStackTrace();
		}
		return result;
	}


	public void SetCurrentLocation(double latti, double longi)
	{
		String curr = GetLocationName(latti,longi);
		
        try {
        	sampleDB =  this.openOrCreateDatabase(SAMPLE_DB_NAME, MODE_PRIVATE, null);
        	sampleDB.execSQL("CREATE TABLE IF NOT EXISTS " +
        			SAMPLE_TABLE_NAME +
        			" (Location VARCHAR NOT NULL" +
        			" ,Longitude VARCHAR NOT NULL,Lattitude VARCHAR NOT NULL);");
        	
        	sampleDB.execSQL("INSERT INTO " +
        			SAMPLE_TABLE_NAME +
        			" Values ('"+curr+"','"+String.valueOf(longi)+"','"+String.valueOf(latti)+"');");
        } catch (SQLiteException se ) {
        	Log.e(getClass().getSimpleName(), "Could not create or Open the database");
        } finally {
/*        	if (sampleDB != null) 
        		sampleDB.execSQL("DELETE FROM " + SAMPLE_TABLE_NAME);
  */      		sampleDB.close();
        }
	}

}
