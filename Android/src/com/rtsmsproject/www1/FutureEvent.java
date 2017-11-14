package com.rtsmsproject.www1;

import java.io.InputStreamReader;
import java.io.Reader;
import java.util.ArrayList;

import com.google.gson.Gson;
import com.rtsmsproject.www.webservice.EventAggregator;
import com.rtsmsproject.www1.TodayEvent.MenuItemAdapter;
import com.rtsmsproject.www1.classes.CollectionFutureEvent;
import com.rtsmsproject.www1.classes.CollectionMyNearBy;
import com.rtsmsproject.www1.classes.CollectionTodayEvent;
import com.rtsmsproject.www1.classes.DownloadJson;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Toast;
import android.widget.AdapterView.OnItemClickListener;

public class FutureEvent extends Activity {

    // All objects are from android.context.Context
    public static final String PREFS_NAME = "MyPrefsFile";

    /** Called when the activity is first created. */
	public ArrayList<MenuItems> items = new ArrayList<MenuItems>();
	private MenuItemAdapter adapter = null;
    public String urlnew = null;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.futureevent);
        // Restore preferences
		SharedPreferences prefs = getSharedPreferences(PREFS_NAME, 0);
		Double latt = Double.valueOf(prefs.getString("Lattitude","0.0"));
		Double lngt = Double.valueOf(prefs.getString("Longitude","0.0"));
        try{
            EventAggregator ea = new EventAggregator(this,latt,lngt);
            items = ea.GetEvent(SettingUrls.ScreenName_FutureEvent);
		}
		catch(Exception ex){
			ex.printStackTrace();
		}
	
		adapter = new MenuItemAdapter();
		ListView MenuBar1 = (ListView) findViewById(android.R.id.list);
		MenuBar1.setAdapter(adapter);
		MenuBar1.setTextFilterEnabled(true);
		MenuBar1.setOnItemClickListener(new OnItemClickListener() {

		public void onItemClick(AdapterView<?>parent, View view, int position, long arg3) {

				MenuItems selectedMenuItem = items.get((int) position);
				int selectedMenuItemType = items.get((int) position).getType();
				Intent scan = new Intent(view.getContext(),DetailView.class);

				scan.putExtra("desc", items.get((int) position).getDescription());
				scan.putExtra("distance", items.get((int) position).getDistance());
				scan.putExtra("eventicon", items.get((int) position).getEventIconUrl());
				scan.putExtra("eventname", items.get((int) position).getEventName());
				scan.putExtra("helddate", items.get((int) position).getHeldDate());
				scan.putExtra("locationname", items.get((int) position).getLocationName());
				scan.putExtra("releasedate", items.get((int) position).getReleaseDate());
				scan.putExtra("reportedby", items.get((int) position).getReportedBy());
				scan.putExtra("rssitemid", items.get((int) position).getRSSItemID());
				scan.putExtra("screentype", items.get((int) position).getScreenType());
				scan.putExtra("weburl", items.get((int) position).getWebUrl());
				scan.putExtra("alertlevel", items.get((int) position).getAlertLevel());
				scan.putExtra("isalert", items.get((int) position).getIsAlert());
				scan.putExtra("lattitude", items.get((int) position).getLattitude());
				scan.putExtra("longitude", items.get((int) position).getLongitude());
				scan.putExtra("type", items.get((int) position).getType());
						
				view.getContext().startActivity(scan);
			}
		});
    }
    public void refreshbtnhandler(View target)
    {
        // Restore preferences
		SharedPreferences prefs = getSharedPreferences(PREFS_NAME, 0);
		Double latt = Double.valueOf(prefs.getString("Lattitude","0.0"));
		Double lngt = Double.valueOf(prefs.getString("Longitude","0.0"));
        try{
            EventAggregator ea = new EventAggregator(this,latt,lngt);
            items = ea.GetEvent(SettingUrls.ScreenName_FutureEvent);
        } 		catch(Exception ex){
			ex.printStackTrace();
		}
    }
    public class MenuItemAdapter extends ArrayAdapter<MenuItems> {
	//private static final ArrayList<MenuItems> items;
	MenuItemAdapter() {
		super(FutureEvent.this , android.R.layout.simple_list_item_1,items);
	}


    	public View getView(int position, View convertView, ViewGroup parent) {
    			View row = convertView;
    			MenuItemWrapper wrapper=null;
    			if (row==null) {
    				LayoutInflater inflater = (LayoutInflater)FutureEvent.this.getSystemService(Context.LAYOUT_INFLATER_SERVICE);//getLayoutInflater(); 
    				row=inflater.inflate(R.layout.eventlisttemplate,parent,false);
    				wrapper=new MenuItemWrapper(row);
    				row.setTag(wrapper);
    			}
    			else {
    				wrapper=(MenuItemWrapper)row.getTag();
    			}
    			wrapper.populateFrom(items.get(position));
    			return(row);
    			}

    }
        
    	private String GetLocationNames(double latti,double longi)
    	{
    		String result = null;
    		String url = "http://192.168.1.102/RTSMSWebService/WebService.svc/GetLocationName/?longitude="+longi+"&lattitude="+latti;
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

    public void clickbtnHandler(View v)
    {
    	//Toast.makeText(getApplicationContext(), "hello world", 1).show();
  	 //  Intent question = new Intent(this ,com.project.Second.class);
  	 //  this.startActivity(question);
    }
}
