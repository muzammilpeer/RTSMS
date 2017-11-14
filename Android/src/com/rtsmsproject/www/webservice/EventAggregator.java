package com.rtsmsproject.www.webservice;

import java.io.InputStreamReader;
import java.io.Reader;
import java.util.ArrayList;

import android.content.Context;
import android.content.SharedPreferences;
import android.preference.PreferenceManager;
import android.util.Log;

import com.google.gson.Gson;
import com.rtsmsproject.www.db.DatabaseManager;
import com.rtsmsproject.www1.MenuItems;
import com.rtsmsproject.www1.SettingUrls;
import com.rtsmsproject.www1.classes.CollectionEventTable;
import com.rtsmsproject.www1.classes.DownloadJson;

public class EventAggregator 
{
	private static Integer count = 0;
	private static final String NULL = null;
	DatabaseManager db;
    public static final String PREFS_NAME = "MyPrefsFile";
    private Double LattitudeVal ;
    private Double LongitudeVal;
    
    
	private void InitialzeDatabase(Context content)
	{
    	try
    	{
	        // create the database manager object
	        db = new DatabaseManager(content);
	     }    
    	catch (Exception e)
    	{
    		Log.e("ERROR", e.toString());
    		e.printStackTrace();
    	}

	}
	private String MakeWebServiceUrl(String screentype)
    {
		//SharedPreferences settings = this.getSharedPreferences(PREFS_NAME, 0);
		//////////////////////////for getting settings panel values
//		SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(getBaseContext());
//		String ListDistance = prefs.getString("listDistance","2");
		// listUnit  == 1 Miles , == 2 Kilometers
	//	Integer listUnit = Integer.parseInt(prefs.getString("listUnit","1"));
		
		
	//	String url = SettingUrls.webserverurl+"/GetHomeStatus/?latt="+Double.valueOf(settings.getString("Lattitude", "0.0"))+"&lngt="+Double.valueOf(settings.getString("Longitude", "0.0"));

    	return SettingUrls.webserverurl+"/GetEvent/?lat="+LattitudeVal+"&lng="+LongitudeVal+"&radius="+SettingUrls.Global_Radius+"&screentype="+screentype+"&clienttype="+SettingUrls.Global_ClientName;
    }
	// Constructor
	public EventAggregator(Context context,Double Latt,Double Longi)
	{
		this.LattitudeVal = Latt;
		this.LongitudeVal = Longi;
		InitialzeDatabase(context);
	}
	public Boolean DownloadEvent(String screentype)
	{
		ArrayList<MenuItems> items = new ArrayList<MenuItems>();
		//items.clear();
        try
		{
        	// first is to check the local copy of cache is available or not
        	// if not the cache_miss
        	// else cache_hit (local copy from sqlite)
 			Gson  json = new Gson();
			Reader r = new InputStreamReader(DownloadJson.getJSONData(MakeWebServiceUrl(screentype)));
			CollectionEventTable obj = json.fromJson(r, CollectionEventTable.class);
		    for ( int i = 0; i <obj.GetEventResult.size();i++)
	        {
		    	Boolean check =  db.EventTableCheckIfExistEntry(obj.GetEventResult.get(i).getRSSItemID(),screentype);
	        	if ( check == false)
		    	db.EventTableInsert(obj.GetEventResult.get(i).getRSSItemID(),obj.GetEventResult.get(i).getEventIconUrl(),obj.GetEventResult.get(i).getLongitude().toString(),obj.GetEventResult.get(i).getLattitude().toString(),obj.GetEventResult.get(i).getLocationName(),obj.GetEventResult.get(i).getEventName(),obj.GetEventResult.get(i).getScreenType(),obj.GetEventResult.get(i).getDistance(),obj.GetEventResult.get(i).getReportedBy(),obj.GetEventResult.get(i).getReleaseDate(),obj.GetEventResult.get(i).getDescription(),obj.GetEventResult.get(i).getWebUrl(),obj.GetEventResult.get(i).getIsAlert().toString(),obj.GetEventResult.get(i).getAlertLevel().toString(),obj.GetEventResult.get(i).getHeldDate());
	        }
		}
		catch(Exception ex){
    		Log.e("Error in Inserting EventTable", ex.toString());
    		ex.printStackTrace();
    		return false;
		}
		return true;
	}
	public ArrayList<MenuItems> FetchEvent(String screentype)
	{
		ArrayList<MenuItems> items = new ArrayList<MenuItems>();
		//items.clear();
        try
		{
        	//db.EventTableTruncate();
        	ArrayList<ArrayList<Object>> obj = null;
        	obj = db.EventTableSelect(screentype, SettingUrls.Global_Lattitude.toString(), SettingUrls.Global_Longitude.toString());
        	for (int i =0; i< obj.size();i++)
        	{
        		items.add(new MenuItems(obj.get(i).get(0).toString(),obj.get(i).get(1).toString(),Double.valueOf(obj.get(i).get(2).toString()),Double.valueOf(obj.get(i).get(3).toString()),obj.get(i).get(4).toString(),obj.get(i).get(5).toString(),obj.get(i).get(6).toString(),obj.get(i).get(7).toString(),obj.get(i).get(8).toString(),obj.get(i).get(9).toString(),obj.get(i).get(10).toString(),obj.get(i).get(11).toString(),Boolean.valueOf(obj.get(i).get(12).toString()),Integer.valueOf(obj.get(i).get(13).toString()),obj.get(i).get(14).toString(),MenuItems.BombBlast));        		
        	}
        	count = obj.size();
        	//items.add(new MenuItems(obj.GetEventResult.get(i).getRSSItemID(),obj.GetEventResult.get(i).getEventIconUrl(),obj.GetEventResult.get(i).getLongitude(),obj.GetEventResult.get(i).getLattitude(),obj.GetEventResult.get(i).getLocationName(),obj.GetEventResult.get(i).getEventName(),obj.GetEventResult.get(i).getScreenType(),obj.GetEventResult.get(i).getDistance(),obj.GetEventResult.get(i).getReportedBy(),obj.GetEventResult.get(i).getReleaseDate(),obj.GetEventResult.get(i).getDescription(),obj.GetEventResult.get(i).getWebUrl(),obj.GetEventResult.get(i).getIsAlert(),obj.GetEventResult.get(i).getAlertLevel(),obj.GetEventResult.get(i).getHeldDate(),MenuItems.BombBlast));
		}
		catch(Exception ex){
			ex.printStackTrace();
		}
		return items;
	}


	public ArrayList<MenuItems> GetEvent(String screentype)
	{
		ArrayList<MenuItems> items = new ArrayList<MenuItems>();
	    try
		{
        	// first is to check the local copy of cache is available or not
        	// if not the cache_miss
        	// else cache_hit (local copy from sqlite)
        	items = FetchEvent(screentype);
        	/*if (count <= 0)
        	{
        	  //items.clear();
        	  DownloadEvent(screentype);
        	  FetchEvent(screentype);
        	  // update the ui with new data
        	}*/
		}
		catch(Exception ex){
			ex.printStackTrace();
		}
		return items;
	}
}
