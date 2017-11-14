package com.rtsmsproject.www1;

import com.rtsmsproject.www1.HomeStatus.MyLocationListener;
import com.rtsmsproject.www1.classes.AppTabs;

import android.app.AlertDialog;
import android.app.TabActivity;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.widget.TabHost;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.TabHost.TabSpec;

public class HomePage extends TabActivity {
    // All objects are from android.context.Context
    public static final String PREFS_NAME = "MyPrefsFile";
	TabHost tabHost;	

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
            				//	Intent intent = new Intent(this, com.rtsmsproject.www1.HomeStatus.class);
            					//Toast.makeText(getApplicationContext(), "Home Status is Loading...", 1).show();
            				//	startActivity(intent);
            				}	break;
            case R.id.maps:     
            				{
            					Intent intent = new Intent(this, com.rtsmsproject.www1.MapsTagView.class);
            				//	Toast.makeText(getApplicationContext(), "Maps are Loading...", 1).show();
            					startActivity(intent);
            				}	break;
            case R.id.help:
            				{
            					Intent intent = new Intent(this, com.rtsmsproject.www1.Help.class);
            					//Toast.makeText(getApplicationContext(), "Help is Loading...", 1).show();
            					startActivity(intent);
            				}	break;
            case R.id.settingsopt : 
            						{
            				        	Intent intent = new Intent(this, com.rtsmsproject.www1.SettingsPanel.class);
       						//Toast.makeText(getApplicationContext(), "Settings Panel Loading...", 1).show();
            				        	startActivity(intent);

            						}	break;
            case R.id.logout: {
            						Intent intent = new Intent(this,com.rtsmsproject.www1.MainPage.class);
            						//Toast.makeText(getApplicationContext(), "You Have Successfully Logouts.", 1).show();
            						this.finish();
            						startActivity(intent);
            				  } break;
        }
        return true;
    }

	protected void onCreate(Bundle savedInstanceState) {
	        super.onCreate(savedInstanceState);
	        setContentView(R.layout.homepage);

	        /* Use the LocationManager class to obtain GPS locations */
			LocationManager mlocManager = (LocationManager)getSystemService(Context.LOCATION_SERVICE);
			LocationListener mlocListener = new MyLocationListener();
			mlocManager.requestLocationUpdates( LocationManager.NETWORK_PROVIDER, 0, 0, mlocListener);

	        
	        /* TabHost will have Tabs */
	       // TabHost tabHost1 = (TabHost)findViewById(android.R.id.tabhost);
	        tabHost = (TabHost)findViewById(android.R.id.tabhost);
	        AppTabs.setMyTabs(tabHost, this);

/*	        tabHost1.setup();
	        /* TabSpec used to create a new tab. 
	         * By using TabSpec only we can able to setContent to the tab.
	         * By using TabSpec setIndicator() we can set name to tab. */
	        /*
	        /* tid1 is firstTabSpec Id. Its used to access outside. */
	  /*      TabSpec firstTabSpec = tabHost1.newTabSpec("Tab 1");
	        firstTabSpec.setIndicator("Today Event",getResources().getDrawable(R.drawable.present)).setContent(new Intent(this,TodayEvent.class));
	        TabSpec secondTabSpec = tabHost1.newTabSpec("Tab 2");
	        secondTabSpec.setContent(new Intent(this,HistoryEvent.class));
	        secondTabSpec.setIndicator("Historys Event",getResources().getDrawable(R.drawable.history));
	        TabSpec thirdTabSpec = tabHost1.newTabSpec("Tab 3");
	        thirdTabSpec.setContent(new Intent(this,FutureEvent.class));
	        thirdTabSpec.setIndicator("Future Event",getResources().getDrawable(R.drawable.future));
	        /* Add tabSpec to the TabHost to display. */
	        
	/*        tabHost1.addTab(firstTabSpec);
	        tabHost1.addTab(secondTabSpec);
	        tabHost1.addTab(thirdTabSpec);
*/
	 }
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

	/* Class My Location Listener */
	public class MyLocationListener implements LocationListener
	{
		public void onLocationChanged(Location loc)
		{
			   SharedPreferences settings = getSharedPreferences(PREFS_NAME, 0);
		       SharedPreferences.Editor editor = settings.edit();
		       editor.putString("Lattitude", String.valueOf(loc.getLatitude()));
		       editor.putString("Longitude", String.valueOf(loc.getLongitude()));
		       // Commit the edits!
		       editor.commit();/*
			String Text = "My current location is: " +
			"Latitud = " + loc.getLatitude() +
			"Longitud = " + loc.getLongitude(); */
//			CurrentLocation = Text;
	//		Toast.makeText( getApplicationContext(),CurrentLocation,5).show();
			//TextView txt = (TextView) findViewById(R.id.homestatus_status);
		//	txt.setText(CurrentLocation);
		}
		

		public void onProviderDisabled(String provider)
		{
			showGPSDisabledAlertToUser();
//			Toast.makeText( getApplicationContext(),"Gps Disabled",Toast.LENGTH_SHORT ).show();
		}
	
	
		public void onProviderEnabled(String provider)
		{
			//Toast.makeText( getApplicationContext(),"AGps Enabled",	Toast.LENGTH_SHORT).show();
		}
	
	
		public void onStatusChanged(String provider, int status, Bundle extras)
		{
		}
	}/* End of Class MyLocationListener */


}
