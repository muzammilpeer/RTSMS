package com.rtsmsproject.www1;

import java.util.ArrayList;
import java.util.List;

import android.content.Context;
import android.content.SharedPreferences;
import android.graphics.drawable.Drawable;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.maps.GeoPoint;
import com.google.android.maps.MapActivity;
import com.google.android.maps.MapController;
import com.google.android.maps.MapView;
import com.google.android.maps.Overlay;
import com.google.android.maps.OverlayItem;
import com.rtsmsproject.www1.MapOverlayClass.MarkerOverlay;
import com.rtsmsproject.www1.MapsTagView.MyLocationListener;
import com.rtsmsproject.www1.classes.HelloItemizedOverlay;
//import com.rtsmsproject.www.*;
public class CircleOverlay extends MapActivity {

	@Override
	protected boolean isRouteDisplayed() {
		// TODO Auto-generated method stub
		return false;
	}
	private MapView mapView;
	private List<Overlay> mapOverlays;
	private MarkerOverlay myLocationOverlay;
	private MapController mapController;
	private TextView txtRadius;
	private Button btnSearch;
	private boolean isFound;
	private Double latt;
	private Double lngi;
    public static final String PREFS_NAME = "MyPrefsFile";

	@Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.markeroverlay);
        
        mapView = (MapView) findViewById(R.id.mapview);
        mapView.setBuiltInZoomControls(true);
        mapController = mapView.getController();
        
        btnSearch = (Button) findViewById(R.id.btnSearch);
        btnSearch.setOnClickListener(new SearchListener());
        
        txtRadius = (TextView) findViewById(R.id.txtRadius);

		

    }
    
	private class SearchListener implements android.view.View.OnClickListener {
		public void onClick(View v) {
			mapView.getOverlays().clear();
			mapOverlays = mapView.getOverlays();

			//////////////////////////////////////
/*	    	LocationManager mlocManager = (LocationManager)getSystemService(Context.LOCATION_SERVICE);
	    	final Location ls = mlocManager.getLastKnownLocation(LocationManager.NETWORK_PROVIDER);

	    	LocationListener mlocListener = new MyLocationListener();
	    	mlocManager.requestLocationUpdates( LocationManager.NETWORK_PROVIDER, 0, 0, mlocListener);

	    	latt = ls.getLatitude();
	    	lngi = ls.getLongitude();
			//////////////////////////////////////
	*/		
	        myLocationOverlay = new MarkerOverlay(CircleOverlay.this, mapView);
	        // Set Radius to 1000 meters.
	         // Restore preferences
			SharedPreferences prefs = getSharedPreferences(PREFS_NAME, 0);
			final Double lattitude = Double.valueOf(prefs.getString("Lattitude","0.0"));
			final Double longitude = Double.valueOf(prefs.getString("Longitude","0.0"));
			myLocationOverlay.getMyLocation(lattitude, longitude);

	        myLocationOverlay.setMeters(80000);
	        myLocationOverlay.enableCompass();
	        myLocationOverlay.enableMyLocation();
            mapController.animateTo(myLocationOverlay.getMyLocation(lattitude, longitude));
/*
	        myLocationOverlay.runOnFirstFix(new Runnable() {
	            public void run() {
	            }
	        });*/
	        mapView.getOverlays().add(myLocationOverlay);

			displayResults();
		}

    }

	private void displayResults() {
		// Create dummy list of GeoPoint
		GeoPoint point1 = new GeoPoint((int)(24.8601083*1E6), (int)(67.0162619*1E6));
		GeoPoint point2 = new GeoPoint((int)(24.3601083*1E6), (int)(67.4162619*1E6));
		GeoPoint point3 = new GeoPoint((int)(24.6601083*1E6), (int)(67.3162619*1E6));
		List<GeoPoint> points = new ArrayList<GeoPoint>();
		points.add(point1);
		points.add(point2);
		points.add(point3);
		mapOverlays = mapView.getOverlays();
		Drawable drawable = getResources().getDrawable(R.drawable.pin_marker);
		HelloItemizedOverlay itemizedOverlay = new HelloItemizedOverlay(drawable);
/*
        for(GeoPoint point : points) {
        	// Create a location because Location has a distanceTo method that we can
        	// use for buffering. Notice that distanceTo calculate distance in meter
	        Location gpsLocation = new Location("current location");

	        // Get our current gps point and use it to create a location
	        GeoPoint currentLocation = myLocationOverlay.getMyLocation(latt,lngi);
	        double lat = (double) (currentLocation.getLatitudeE6() / 1000000.0);
	        double lng = (double) (currentLocation.getLongitudeE6() / 1000000.0);
	        gpsLocation.setLatitude(lat);
	        gpsLocation.setLongitude(lng);
	        Location pointLocation = new Location("point");
	        pointLocation.setLatitude(point.getLatitudeE6() / 1000000.0);
	        pointLocation.setLongitude(point.getLongitudeE6() / 1000000.0);

	        // Calculate the distance between current location and point location
	        if(gpsLocation.distanceTo(pointLocation) < Float.parseFloat(txtRadius.getText().toString())) {
		        isFound = true;
	        	OverlayItem overlayitem = new OverlayItem(point, "Hi", "Iam here");
		        itemizedOverlay.addOverlay(overlayitem);
	        }
        }*/
        // If any location found, draw the placemark
        if(isFound)
        	mapOverlays.add(itemizedOverlay);
	}

	/* Class My Location Listener */
	public class MyLocationListener implements LocationListener
	{
		public void onLocationChanged(Location loc)
		{
			 latt = loc.getLatitude();
			 lngi = loc.getLongitude();
			//SetCurrentLocation(latti,longi);
			// update the location in Shared Preferencecs or in database.
		}
		

		public void onProviderDisabled(String provider)
		{
	    }
	
		public void onProviderEnabled(String provider)
		{
			Toast.makeText( getApplicationContext(),"AGps Enabled",	Toast.LENGTH_SHORT).show();
		}
	
		public void onStatusChanged(String provider, int status, Bundle extras)
		{
		}
	}/* End of Class MyLocationListener */

}
