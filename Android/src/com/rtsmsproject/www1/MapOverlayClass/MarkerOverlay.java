package com.rtsmsproject.www1.MapOverlayClass;

import android.content.Context;
import android.content.SharedPreferences;
import android.graphics.Canvas;
import android.graphics.Paint;
import android.graphics.Paint.Style;
import android.graphics.Point;

import com.google.android.maps.GeoPoint;
import com.google.android.maps.MapView;
import com.google.android.maps.Overlay;
import com.google.android.maps.Projection;

public class MarkerOverlay extends Overlay {
	private MapView mapView;
    public static final String PREFS_NAME = "MyPrefsFile";
    private Paint circlePainter;
    private Point screenCurrentPoint;
	private GeoPoint geoCurrentPoint;
    private int meters;

	public MarkerOverlay(Context context, MapView mapView) {
		super();
		this.mapView = mapView;
	}
// 1000 meters is enough for an area to circle
	// This method is used to get user submitted radius from our application
    public void setMeters(int meters) {
    	this.meters = meters;
    }

    @Override
	public synchronized boolean draw(Canvas canvas, MapView mapView,
			boolean shadow, long when) {
    	// Set the painter to paint our circle. setColor = blue, setAlpha = 70 so the background
    	// can still be seen. Feel free to change these settings
    	circlePainter = new Paint();
    	circlePainter.setAntiAlias(true);
    	circlePainter.setStrokeWidth(2.0f);
    	circlePainter.setColor(0xff6666ff);
    	circlePainter.setStyle(Style.FILL_AND_STROKE);
    	circlePainter.setAlpha(70);
    	
    	// Get projection from the mapView. 
    	Projection projection = mapView.getProjection();
    	// Get current location
//    	geoCurrentPoint = getMyLocation();
    	screenCurrentPoint = new Point();
    	// Project the gps coordinate to screen coordinate
    	projection.toPixels(geoCurrentPoint, screenCurrentPoint);
    	
    	int radius = metersToRadius(geoCurrentPoint.getLatitudeE6() /1000000);
    	// draw the blue circle
    	canvas.drawCircle(screenCurrentPoint.x, screenCurrentPoint.y, radius, circlePainter);
        return super.draw(canvas, mapView, shadow, when);
    }
    
    public GeoPoint getMyLocation(Double latt, Double lng) {
    	GeoPoint points = new GeoPoint((int)(latt*1E6),(int)(latt*1E6));
    	geoCurrentPoint = points;
    	// TODO Auto-generated method stub
		return points;
	}

	// hack to get more accurate radius, because the accuracy is changing as the location
    // getting further away from the equator
	public int metersToRadius(double latitude) {
	    return (int) (mapView.getProjection().metersToEquatorPixels(meters) * (1/ Math.cos(Math.toRadians(latitude))));         
	}

	public void enableCompass() {
		// TODO Auto-generated method stub
		
	}

	public void enableMyLocation() {
/*        // Restore preferences
		SharedPreferences prefs = getSharedPreferences(PREFS_NAME, 0);
		final Double lattitude = Double.valueOf(prefs.getString("Lattitude","0.0"));
		final Double longitude = Double.valueOf(prefs.getString("Longitude","0.0"));
    	GeoPoint points = new GeoPoint((int)(lattitude*1E6),(int)(longitude*1E6));

    	geoCurrentPoint = points;*/
    	// TODO Auto-generated method stub
		
	}

	public void runOnFirstFix(Runnable runnable) {
		// TODO Auto-generated method stub
		
	}
}
