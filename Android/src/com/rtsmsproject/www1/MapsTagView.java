package com.rtsmsproject.www1;

import java.io.InputStreamReader;
import java.io.Reader;
import java.util.List;

import com.google.android.maps.GeoPoint;
import com.google.android.maps.MapActivity;
import com.google.android.maps.MapController;
import com.google.android.maps.MapView;
import com.google.android.maps.Overlay;
import com.google.android.maps.OverlayItem;
import com.google.gson.Gson;
import com.rtsmsproject.www1.HomeStatus.MyLocationListener;
import com.rtsmsproject.www1.classes.CollectionFutureEvent;
import com.rtsmsproject.www1.classes.CollectionTodayEvent;
import com.rtsmsproject.www1.classes.DownloadJson;

import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteException;
import android.graphics.drawable.Drawable;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.util.Log;
import android.view.KeyEvent;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.widget.TextView;
import android.widget.Toast;

public class MapsTagView extends MapActivity {

	private final String SAMPLE_DB_NAME = "rtsmsDB";
	private final String SAMPLE_TABLE_NAME = "Current_Location";
    private SQLiteDatabase sampleDB = null;
	private Double Longitude = 0.0;
	private Double Lattitude = 0.0;
    // We need an Editor object to make preference changes.
    // All objects are from android.context.Context
    public static final String PREFS_NAME = "MyPrefsFile";

	MapView mapView;
	List<Overlay> mapOverlays;
	Drawable drawable;
	Drawable drawable2;
	Drawable drawable3;
	Drawable drawable4;
	MyItemizedOverlay itemizedOverlay;
	MyItemizedOverlay itemizedOverlay2;
	MyItemizedOverlay itemizedOverlay3;
	MyItemizedOverlay itemizedOverlay4;
	float distance = 0;
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.todayeventmenu, menu);
        return true;
    }
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case R.id.home:             
            				{
            					Intent intent = new Intent(this, com.rtsmsproject.www1.HomeStatus.class);
            					Toast.makeText(getApplicationContext(), "Home Status is Loading...", 1).show();
            					startActivity(intent);
            				}	break;
            case R.id.maps:     
            				{
            					/*
            					Intent intent = new Intent(this, com.rtsmsproject.www1.MapsTagView.class);
            					Toast.makeText(getApplicationContext(), "Maps are Loading...", 1).show();
            					startActivity(intent);
            				*/}	break;
            case R.id.help:
            				{
            					Intent intent = new Intent(this, com.rtsmsproject.www1.Help.class);
            					startActivity(intent);
            				}	break;
            case R.id.settingsopt : 
            						{
            				        	Intent intent = new Intent(this, com.rtsmsproject.www1.SettingsPanel.class);
            				        	startActivity(intent);
            						}	break;
            case R.id.logout: {
            						Intent intent = new Intent(this,com.rtsmsproject.www1.MainPage.class);
            						Toast.makeText(getApplicationContext(), "You Have Successfully Logouts.", 1).show();
            						startActivity(intent);
            				  } break;
        }
        return true;
    }


	
	@Override
    public void onCreate(Bundle savedInstanceState) {
		
        super.onCreate(savedInstanceState);
        setContentView(R.layout.mapstagview);
    	LocationManager mlocManager = (LocationManager)getSystemService(Context.LOCATION_SERVICE);
    	Location ls = mlocManager.getLastKnownLocation(LocationManager.NETWORK_PROVIDER);

    	LocationListener mlocListener = new MyLocationListener();
    	mlocManager.requestLocationUpdates( LocationManager.NETWORK_PROVIDER, 0, 0, mlocListener);
        
        mapView = (MapView) findViewById(R.id.mapview);
		mapView.setBuiltInZoomControls(true);
	   
		mapOverlays = mapView.getOverlays();
		
		mapView.setSatellite(false);
		mapView.setTraffic(false);
	/*	// first overlay
		drawable = getResources().getDrawable(R.drawable.marker);
		itemizedOverlay = new MyItemizedOverlay(drawable, mapView);
		
		//GeoPoint point = new GeoPoint((int)(24.8602083*1E6),(int)(67.2162619*1E6));
        /*
		String url = "http://192.168.1.102/RTSMSWebService/WebService.svc/GetTodayEventTable/Karachi,Malir";
		try
		{
 			Gson  json = new Gson();
			Reader r = new InputStreamReader(DownloadJson.getJSONData(url));
			CollectionTodayEvent obj = json.fromJson(r, CollectionTodayEvent.class);
	//        Toast.makeText(getApplicationContext(),obj.GetTodayUpdatesResult.get(1).getTitle().toString() , 1).show();
	        for ( int i = 0; i < obj.GetTodayUpdatesResult.size();i++)
	        {
	        	GeoPoint points = new GeoPoint((int)(Double.valueOf(obj.GetTodayUpdatesResult.get(i).getLattitude())*1E6),(int)(Double.valueOf(obj.GetTodayUpdatesResult.get(i).getLongitude())*1E6));
	        	OverlayItem overlayItem = new OverlayItem(points, obj.GetTodayUpdatesResult.get(i).getTitle(), 
				obj.GetTodayUpdatesResult.get(i).getShortMessage());
	    		itemizedOverlay.addOverlay(overlayItem);
	    		mapOverlays.add(itemizedOverlay);
	        }
	        url = "http://192.168.1.102/RTSMSWebService/WebService.svc/GetFutureEventTable/karachi";
 			json = new Gson();
			r = new InputStreamReader(DownloadJson.getJSONData(url));
			CollectionFutureEvent objs = json.fromJson(r, CollectionFutureEvent.class);
//	        Toast.makeText(getApplicationContext(),String.valueOf(objs.GetFutureEventTableResult.size()) , 1).show();
	        drawable3 = getResources().getDrawable(R.drawable.circle);
			itemizedOverlay3 = new MyItemizedOverlay(drawable3, mapView);
	        
			for ( int i = 0; i < obj.GetTodayUpdatesResult.size();i++)
	        {
	        	GeoPoint points = new GeoPoint((int)(Double.valueOf(obj.GetTodayUpdatesResult.get(i).getLattitude())*1E6),(int)(Double.valueOf(obj.GetTodayUpdatesResult.get(i).getLongitude())*1E6));
	        	OverlayItem overlayItem = new OverlayItem(points, obj.GetTodayUpdatesResult.get(i).getTitle(), 
				obj.GetTodayUpdatesResult.get(i).getShortMessage());
	    		itemizedOverlay3.addOverlay(overlayItem);
	    		mapOverlays.add(itemizedOverlay3);
	        }
			// second overlay
			drawable2 = getResources().getDrawable(R.drawable.marker2);
			itemizedOverlay2 = new MyItemizedOverlay(drawable2, mapView);
		
	        for ( int i = 0; i < objs.GetFutureEventTableResult.size();i++)
	        {
	        	GeoPoint points = new GeoPoint((int)(Double.valueOf(objs.GetFutureEventTableResult.get(i).getLattitude())*1E6),(int)(Double.valueOf(objs.GetFutureEventTableResult.get(i).getLongitude())*1E6));
	        	OverlayItem overlayItem = new OverlayItem(points, objs.GetFutureEventTableResult.get(i).getTitle(), 
				objs.GetFutureEventTableResult.get(i).getShortMessage());
	    		itemizedOverlay2.addOverlay(overlayItem);
	    		mapOverlays.add(itemizedOverlay2);
	    	   
	        }

		

		}
		catch(Exception ex){
			ex.printStackTrace();
		}
		mapOverlays.add(itemizedOverlay);
        */

		//Lattitude = ls.getLatitude();
//		if (ls.getLatitude() == 24.8601083)
	//	Toast.makeText(getApplicationContext(), String.valueOf(ls.getLatitude()), 10).show();
		//Toast.makeText(getApplicationContext(), String.valueOf(ls.getLongitude()), 5).show();
		//GetLocationName();
//		GeoPoint point4 = new GeoPoint((int)(24.8822664*1E6),(int)(67.1823501*1E6));


		// second overlay
		drawable4 = getResources().getDrawable(R.drawable.green_marker );
		itemizedOverlay4 = new MyItemizedOverlay(drawable4, mapView);
		GeoPoint point4 = new GeoPoint((int)(ls.getLatitude()*1E6),(int)(ls.getLongitude()*1E6));
    	OverlayItem overlayItem = new OverlayItem(point4, "I am here", "my current location");
		
    	itemizedOverlay4.addOverlay(overlayItem);
		mapOverlays.add(itemizedOverlay4);

		final MapController mc = mapView.getController();
		itemizedOverlay4.enableCompass();
		mc.animateTo(point4);
		mc.setZoom(16);

/*		Location location = new Location("gps");
		location.setLatitude(point4.getLatitudeE6() / 1E6);
		location.setLongitude(point4.getLongitudeE6() / 1E6);
		Location location2 = new Location("gps");
		location2.setLatitude(point5.getLatitudeE6() / 1E6);
		location2.setLongitude(point5.getLongitudeE6() / 1E6);
		float dist = location.distanceTo(location2);
		distance = dist;
  */  }
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
			double latti = loc.getLatitude();
			double longi = loc.getLongitude();
			//SetCurrentLocation(latti,longi);
			// update the location in Shared Preferencecs or in database.
		}
		

		public void onProviderDisabled(String provider)
		{
			showGPSDisabledAlertToUser();
	    }
	
	
		public void onProviderEnabled(String provider)
		{
			Toast.makeText( getApplicationContext(),"AGps Enabled",	Toast.LENGTH_SHORT).show();
		}
	
	
		public void onStatusChanged(String provider, int status, Bundle extras)
		{
		}
	}/* End of Class MyLocationListener */

	
	@Override
	protected boolean isRouteDisplayed() {
		return false;
	}
	public void GetLocationName()
	{
		String result  = null;
	    try {
        	sampleDB =  this.openOrCreateDatabase(SAMPLE_DB_NAME, MODE_PRIVATE, null);
        	Cursor c = sampleDB.rawQuery("SELECT * FROM " +
        			SAMPLE_TABLE_NAME +
        			";", null);
        	if (c != null ) {
        		if  (c.moveToFirst()) {
        			do {
        				Longitude = Double.valueOf(c.getString(c.getColumnIndex("Longitude")));
        				Lattitude = Double.valueOf(c.getString(c.getColumnIndex("Lattitude")));
        				//Toast.makeText(getApplicationContext(), String.valueOf(Longitude), 1).show();
        			} while (c.moveToNext());
        		} 
        	}
        } catch (SQLiteException se ) {
        	Log.e(getClass().getSimpleName(), "Could not create or Open the database");
        } finally {
        	if (sampleDB != null) 
        		sampleDB.execSQL("DELETE FROM " + SAMPLE_TABLE_NAME);
        		sampleDB.close();
        }
//        return result;
	}

}
