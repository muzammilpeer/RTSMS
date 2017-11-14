package com.rtsmsproject.www1;

import java.io.IOException;
import java.io.InputStream;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.Calendar;

import android.app.Activity;
import android.content.ContentResolver;
import android.content.ContentUris;
import android.content.ContentValues;
import android.content.Context;
import android.content.Intent;
import android.database.Cursor;
import android.graphics.drawable.Drawable;
import android.net.Uri;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

public class DetailView extends Activity {

	private String weburllink = null;
	private String descstr = null;
	private String distancestr = null;
	private String eventiconstr = null;
	private String eventnamestr = null;
	private String helddatestr = null;
	private String locationnamestr = null;
	private String releasedatestr = null;
	private String reportedbystr = null;
	private String rssitemidstr = null;
	private String screentypestr = null;
	private String weburlstr = null;
	private Integer alertlevelstr = 0;
	private Boolean isalertstr = false;
	private Double lattitudestr = 0.0;
	private Double langitudestre = 0.0;
	private Integer typestr = 0;

	
	@Override
	    public boolean onCreateOptionsMenu(Menu menu) {
	        MenuInflater inflater = getMenuInflater();
	        if ( screentypestr.equals("futureevent"))
	        inflater.inflate(R.menu.detailviewmenu, menu);
	        else  inflater.inflate(R.menu.homemenu, menu);
	        return true;
	    }
	    private String getCalendarUriBase(Activity act) {

		    String calendarUriBase = null;
		    Uri calendars = Uri.parse("content://calendar/calendars");
		    Cursor managedCursor = null;
		    try {
		        managedCursor = act.managedQuery(calendars, null, null, null, null);
		    } catch (Exception e) {
		    }
		    if (managedCursor != null) {
		        calendarUriBase = "content://calendar/";
		    } else {
		        calendars = Uri.parse("content://com.android.calendar/calendars");
		        try {
		            managedCursor = act.managedQuery(calendars, null, null, null, null);
		        } catch (Exception e) {
		        }
		        if (managedCursor != null) {
		            calendarUriBase = "content://com.android.calendar/";
		        }
		    }
		    return calendarUriBase;
		}
	    @Override
	    public boolean onOptionsItemSelected(MenuItem item) {
	        switch (item.getItemId()) {
	            case R.id.reminderadd:             
	            				{
	            					Toast.makeText(getApplicationContext(), "Added the Reminder", 1).show();
	            					// get calendar
	            				    Calendar cal = Calendar.getInstance();     
	            				    Uri EVENTS_URI = Uri.parse(getCalendarUriBase(this) + "events");
	            				    ContentResolver cr = getContentResolver();

	            				    // event insert
	            				    ContentValues values = new ContentValues();
	            				    values.put("calendar_id", 1);
	            				    values.put("title", eventnamestr + "," +locationnamestr);
	            				    values.put("allDay", 0);
	            				    values.put("dtstart", cal.getTimeInMillis() + 11*60*1000); // event starts at 11 minutes from now
	            				    values.put("dtend", cal.getTimeInMillis()+60*60*1000); // ends 60 minutes from now
	            				    values.put("description", eventnamestr + "," +locationnamestr + ";");
	            				    values.put("visibility", 0);
	            				    values.put("hasAlarm", 1);
	            				    Uri event = cr.insert(EVENTS_URI, values);

	            				    // reminder insert
	            				    Uri REMINDERS_URI = Uri.parse(getCalendarUriBase(this) + "reminders");
	            				    values = new ContentValues();
	            				    values.put( "event_id", Long.parseLong(event.getLastPathSegment()));
	            				    values.put( "method", 1 );
	            				    values.put( "minutes", 10 );
	            				    cr.insert( REMINDERS_URI, values );
	            					
	            				}	break;
	            case R.id.maps:     
	            				{
	            					Intent intent = new Intent(this, com.rtsmsproject.www1.MapsTagView.class);
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
	                				//		Toast.makeText(getApplicationContext(), "Settings Panel Loading...", 1).show();
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
        setContentView(R.layout.detailview);
        // Get the Query string posted by th
        //e previous screen.
        // OnCreate -> On Intializing the screen we get the query string 
        // then parse that data from it.
        Intent nt = getIntent();
      
        descstr = nt.getCharSequenceExtra("desc").toString();
        distancestr = nt.getCharSequenceExtra("distance").toString();
        eventiconstr = nt.getCharSequenceExtra("eventicon").toString();
        eventnamestr = nt.getCharSequenceExtra("eventname").toString();
        helddatestr = nt.getCharSequenceExtra("helddate").toString();
        locationnamestr = nt.getCharSequenceExtra("locationname").toString();
        releasedatestr = nt.getCharSequenceExtra("releasedate").toString();
        reportedbystr = nt.getCharSequenceExtra("reportedby").toString();
        rssitemidstr = nt.getCharSequenceExtra("rssitemid").toString();
        screentypestr = nt.getCharSequenceExtra("screentype").toString();
        weburlstr = nt.getCharSequenceExtra("weburl").toString();
        alertlevelstr = nt.getIntExtra("alertlevel",1);
        isalertstr = nt.getBooleanExtra("isalert",false);
        lattitudestr = nt.getDoubleExtra("lattitude",0.0);
        langitudestre = nt.getDoubleExtra("longitude",0.0);
        typestr = nt.getIntExtra("type",MenuItems.BombBlast);
       
        weburllink = weburlstr;
        
        TextView descriptiontxt = (TextView) findViewById(R.id.description);
        TextView locationnametxt = (TextView) findViewById(R.id.locationname);
        TextView eventnametxt = (TextView) findViewById(R.id.eventname);
        TextView screennametxt = (TextView) findViewById(R.id.screenname);
        TextView riskleveltxt = (TextView) findViewById(R.id.risklevel);
        TextView distancestxt = (TextView) findViewById(R.id.distance);
        TextView timeagostxt = (TextView) findViewById(R.id.timeago);
        ImageView alerttypecheck  = (ImageView)findViewById(R.id.alert_type);
        // Initiazing of local boxes and items object and then fill them. as normal filling of textbox by the data we got from previoius screen.
        // That's all.
        if ( isalertstr == true)
        	alerttypecheck.setVisibility(View.VISIBLE);
      

        Drawable image = ImageOperations(this.getApplicationContext(),SettingUrls.webserverurl1+eventiconstr,"logo3.jpg");
		ImageView imgView = new ImageView(this.getApplicationContext());
		imgView = (ImageView)findViewById(R.id.eventicon);
		imgView.setImageDrawable(image);

		descriptiontxt.setText(descstr);
		locationnametxt.setText(locationnamestr);
		eventnametxt.setText(eventnamestr);
		screennametxt.setText(screentypestr);
		riskleveltxt.setText(String.valueOf(alertlevelstr));
		distancestxt.setText(distancestr);
		timeagostxt.setText(releasedatestr);
        
	}
//////////////////////Download the Image/////////////////////////////////
	private Drawable ImageOperations(Context ctx, String url, String saveFilename) {
		try {
			InputStream is = (InputStream) this.fetch(url);
			Drawable d = Drawable.createFromStream(is, "src");
			return d;
		} catch (MalformedURLException e) {
			e.printStackTrace();
			return null;
		} catch (IOException e) {
			e.printStackTrace();
			return null;
		}
	}

	public Object fetch(String address) throws MalformedURLException,IOException {
		URL url = new URL(address);
		Object content = url.getContent();
		return content;
	}
/////////////////////////////////////////////////////////////////////
	
	public void mapsbtnClickHandler(View target)
	{
	      	Intent question = new Intent(this, com.rtsmsproject.www1.Webview.class);
	      	question.putExtra("weburl", weburllink);
	       	this.startActivity(question);
	}
	public void previousbtnClickHandler(View target)
	{
		this.finish();
	}
	public void nextbtnClickHandler(View target)
	{
	      	Intent question = new Intent(this, com.rtsmsproject.www1.DetailView.class);
	       	this.startActivity(question);
	}

}
