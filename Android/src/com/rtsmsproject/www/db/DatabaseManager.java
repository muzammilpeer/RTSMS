package com.rtsmsproject.www.db;
import java.util.ArrayList;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.SQLException;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;
import android.widget.Toast;

public class DatabaseManager {
	// the Activity or Application that is creating an object from this class.
	Context context;
 
	// a reference to the database used by this application/object
	private SQLiteDatabase db;
 
	// These constants are specific to the database.  They should be 
	// changed to suit your needs.
	private final String DB_NAME = "rtsmsdb";
	private final int DB_VERSION = 1;
 
	// These constants are specific to the database table.  They should be
	// changed to suit your needs.

/////////////////////////////////////////////////////////////////////////////////////////////////////////	
	// Database Table Names
	private final String Table_Event = "event";

    private final String Event_rssitemID = "rssitemid";
    private final String Event_EventIconUrl = "eventiconurl";
    private final String Event_Longitude = "longitude"; 
    private final String Event_Lattitude = "lattitude";
    private final String Event_LocationName = "locationname";
    private final String Event_EventName = "eventname";
    private final String Event_ScreenType = "screentype";
    private final String Event_Distance = "distance";
    private final String Event_ReportedBy  = "reportedby";
    private final String Event_ReleaseDate = "releasedate";
    private final String Event_Description = "description";
    private final String Event_WebUrl = "weburl";
	private final String Event_IsAlert = "isalert";
	private final String Event_AlertLevel = "alertlevel";
	private final String Event_HeldDate  = "helddate";

	
///////////////////////////////////////////////////////////////////////////////////////////////////////
	
/////////////////////////////////////////////////////////////////////////////////////////////////////// 
	public DatabaseManager(Context context)
	{
		this.context = context;
 
		// create or open the database
		CustomSQLiteOpenHelper helper = new CustomSQLiteOpenHelper(context);
	
		//SQLiteDatabase.openDatabase(DB_NAME, null,  SQLiteDatabase.NO_LOCALIZED_COLLATORS);
		//this.db = helper.onOpen(db.openDatabase(DB_NAME, null, db.NO_LOCALIZED_COLLATORS));
		
		this.db = helper.getWritableDatabase();
	}
 
 
 
/////////////////////////////////////Insert//////////////////////////////////////////////////////// 
	/**********************************************************************
	 * ADDING A ROW TO THE DATABASE TABLE
	 * 
	 * This is an example of how to add a row to a database table
	 * using this class.  You should edit this method to suit your
	 * needs.
	 * 
	 * the key is automatically assigned by the database
	 * @param rowStringOne the value for the row's first column
	 * @param rowStringTwo the value for the row's second column 
	 */
	
//	cursor = db.rawQuery("select rss.title,rss.details,rss.links,rss.postedby,rss.icon,rss.level,rss.pubdate,loc.name as locationname,loc.longitude,loc.lattitude,rss.distance,event.name as event from eventclass as event ,rssitem as rss,location as loc  where( rss.eventclassid=event.eventclassid AND rss.locationid=loc.locationid AND event.name= 'mynearby');", null);
	// Insert into EventTable

	public void EventTableInsert(String rssitemid,String iconurl,String longi,String latti, String locname,String evntname,String screentype,String distance,String reportedby,String releasedate,String description,String  weburl,String isalert,String alertlevel, String helddate)
	{
		// this is a key value pair holder used by android's SQLite functions
		ContentValues values = new ContentValues();
		values.put(Event_rssitemID, rssitemid);
		values.put(Event_EventIconUrl, iconurl);
		values.put(Event_Longitude, longi);
		values.put(Event_Lattitude, latti);
		values.put(Event_LocationName, locname);
		values.put(Event_EventName, evntname);
		values.put(Event_ScreenType, screentype);
		values.put(Event_Distance, distance);
		values.put(Event_ReportedBy, reportedby);
		values.put(Event_ReleaseDate, releasedate);
		values.put(Event_Description, description);
		values.put(Event_WebUrl, weburl);
		values.put(Event_IsAlert, isalert);
		values.put(Event_AlertLevel, alertlevel);
		values.put(Event_HeldDate, helddate);
 
		// ask the database object to insert the new data 
		try{db.insert(Table_Event, null, values);}
		catch(Exception e)
		{
			Log.e("DB ERROR", e.toString());
			e.printStackTrace();
		}
	}

	
	
/////////////////////////////////////////   [\]Insert    /////////////////////////////////////////////
 ///////////////////////////////////Delete/////////////////////////////////////////////////////////
	/**********************************************************************
	 * DELETING A ROW FROM THE DATABASE TABLE
	 * 
	 * This is an example of how to delete a row from a database table
	 * using this class. In most cases, this method probably does
	 * not need to be rewritten.
	 * 
	 * @param rowID the SQLite database identifier for the row to delete.
	 */

	public void EventTableDelete(String rssitemid)
	{
		// ask the database manager to delete the row of given id
		try {db.delete(Table_Event, Event_rssitemID + "=" + rssitemid, null);}
		catch (Exception e)
		{
			Log.e("DB ERROR", e.toString());
			e.printStackTrace();
		}
	}
	public void EventTableTruncate()
	{
		// ask the database manager to delete the row of given id
		try {db.delete(Table_Event, null, null);}
		catch (Exception e)
		{
			Log.e("DB ERROR", e.toString());
			e.printStackTrace();
		}
	}

	//////////////////////////////////////////  [\] Delete //////////////////////////////////////////
	
//////////////////////////////////////////Update///////////////////////////////////////////////////	
	/**********************************************************************
	 * UPDATING A ROW IN THE DATABASE TABLE
	 * 
	 * This is an example of how to update a row in the database table
	 * using this class.  You should edit this method to suit your needs.
	 * 
	 * @param rowID the SQLite database identifier for the row to update.
	 * @param rowStringOne the new value for the row's first column
	 * @param rowStringTwo the new value for the row's second column 
	 */ 
	
	public void EventTableUpdate(String rssitemid,String iconurl,String longi,String latti, String locname,String evntname,String screentype,String distance,String reportedby,String releasedate,String description,String  weburl,String isalert,String alertlevel, String helddate)
	{
		// this is a key value pair holder used by android's SQLite functions
		ContentValues values = new ContentValues();
		values.put(Event_rssitemID, rssitemid);
		values.put(Event_EventIconUrl, iconurl);
		values.put(Event_Longitude, longi);
		values.put(Event_Lattitude, latti);
		values.put(Event_LocationName, locname);
		values.put(Event_EventName, evntname);
		values.put(Event_ScreenType, screentype);
		values.put(Event_Distance, distance);
		values.put(Event_ReportedBy, reportedby);
		values.put(Event_ReleaseDate, releasedate);
		values.put(Event_Description, description);
		values.put(Event_WebUrl, weburl);
		values.put(Event_IsAlert, isalert);
		values.put(Event_AlertLevel, alertlevel);
		values.put(Event_HeldDate, helddate);
 
 
		try {db.update(Table_Event, values, Event_rssitemID + "=" + rssitemid, null);}
		catch (Exception e)
		{
			Log.e("DB Error", e.toString());
			e.printStackTrace();
		}
	}

////////////////////////////////////////   [\] Update /////////////////////////////////////////

	
	/**********************************************************************
	 * RETRIEVING ALL ROWS FROM THE DATABASE TABLE
	 * 
	 * This is an example of how to retrieve all data from a database
	 * table using this class.  You should edit this method to suit your
	 * needs.
	 * 
	 * the key is automatically assigned by the database
	 */
//	db.EventTableCheckIfExistEntry

	public Boolean EventTableCheckIfExistEntry(String rssitemid,String screentype)
	{
 
		// this is a database call that creates a "cursor" object.
		// the cursor object store the information collected from the
		// database and is used to iterate through the data.
		Cursor cursor;
		Integer count = 0;
		try
		{
			// ask the database object to create the cursor.
			cursor = db.rawQuery("select count(*) from "+Table_Event+" as ev where (ev."+Event_ScreenType+" = '"+screentype+"' and ev."+Event_rssitemID+" = '" +rssitemid + "');", null);
			// move the cursor's pointer to position zero.
			cursor.moveToFirst();
 
			// if there is data after the current cursor position, add it
			// to the ArrayList.
			if (!cursor.isAfterLast())
			{
			//	do
			//	{
					count = Integer.valueOf(cursor.getString(0));
					// Total 15 fields.
			//	}
				// move the cursor's pointer up one position.
			//	while (cursor.moveToNext());
			}
		}
		catch (SQLException e)
		{
			Log.e("DB Error", e.toString());
			e.printStackTrace();
		}
		if ( count > 0)
		{
			//Toast.makeText(context, "Get", 1).show();
			return true;
		}
		// return the ArrayList that holds the data collected from
		// the database.
		return false;
	}

	public ArrayList<ArrayList<Object>> EventTableSelect(String screentype,String Lattitude,String Longitude)
	{
		// create an ArrayList that will hold all of the data collected from
		// the database.
		ArrayList<ArrayList<Object>> dataArrays = new ArrayList<ArrayList<Object>>();
 
		// this is a database call that creates a "cursor" object.
		// the cursor object store the information collected from the
		// database and is used to iterate through the data.
		Cursor cursor;
 
		try
		{
			// ask the database object to create the cursor.
			cursor = db.rawQuery("select DISTINCT * from "+Table_Event+" as ev where (ev."+Event_ScreenType+" = '"+screentype+"' and ev."+Event_Lattitude+" = '" +Lattitude + "' and ev." + Event_Longitude+ " = '" +Longitude+ "') order by ev.distance;", null);
			// move the cursor's pointer to position zero.
			cursor.moveToFirst();
 
			// if there is data after the current cursor position, add it
			// to the ArrayList.
			if (!cursor.isAfterLast())
			{
				do
				{
					ArrayList<Object> dataList = new ArrayList<Object>();

					dataList.add(cursor.getString(0));
					dataList.add(cursor.getString(1));
					dataList.add(cursor.getString(2));
					dataList.add(cursor.getString(3));
					dataList.add(cursor.getString(4));
					dataList.add(cursor.getString(5));
					dataList.add(cursor.getString(6));
					dataList.add(cursor.getString(7));
					dataList.add(cursor.getString(8)); 
					dataList.add(cursor.getString(9)); 
					dataList.add(cursor.getString(10));
					dataList.add(cursor.getString(11));
					dataList.add(cursor.getString(12));
					dataList.add(cursor.getString(13));
					dataList.add(cursor.getString(13));
					// Total 15 fields.
					dataArrays.add(dataList);
				}
				// move the cursor's pointer up one position.
				while (cursor.moveToNext());
			}
		}
		catch (SQLException e)
		{
			Log.e("DB Error", e.toString());
			e.printStackTrace();
		}
 
		// return the ArrayList that holds the data collected from
		// the database.
		return dataArrays;
	}

 
 
	/**********************************************************************
	 * THIS IS THE BEGINNING OF THE INTERNAL SQLiteOpenHelper SUBCLASS.
	 * 
	 * I MADE THIS CLASS INTERNAL SO I CAN COPY A SINGLE FILE TO NEW APPS 
	 * AND MODIFYING IT - ACHIEVING DATABASE FUNCTIONALITY.  ALSO, THIS WAY 
	 * I DO NOT HAVE TO SHARE CONSTANTS BETWEEN TWO FILES AND CAN
	 * INSTEAD MAKE THEM PRIVATE AND/OR NON-STATIC.  HOWEVER, I THINK THE
	 * INDUSTRY STANDARD IS TO KEEP THIS CLASS IN A SEPARATE FILE.
	 *********************************************************************/
 
	/**
	 * This class is designed to check if there is a database that currently
	 * exists for the given program.  If the database does not exist, it creates
	 * one.  After the class ensures that the database exists, this class
	 * will open the database for use.  Most of this functionality will be
	 * handled by the SQLiteOpenHelper parent class.  The purpose of extending
	 * this class is to tell the class how to create (or update) the database.
	 * 
	 * @author Randall Mitchell
	 *
	 */
	private class CustomSQLiteOpenHelper extends SQLiteOpenHelper
	{
		public CustomSQLiteOpenHelper(Context context)
		{
			super(context, DB_NAME, null, DB_VERSION);
		}
 
		@Override
		public void onCreate(SQLiteDatabase db)
		{
			// This string is used to create the database.  It should
			// be changed to suit your needs.
			
			String EventTableQueryString = "create table " +
			Table_Event +
			" (" +
			Event_rssitemID + " text NOT NULL," +
			Event_EventIconUrl + " text," +
			Event_Longitude + " text," +
			Event_Lattitude + " text," +
			Event_LocationName + " text," +
			Event_EventName + " text NOT NULL," +
			Event_ScreenType + " text NOT NULL," +
			Event_Distance + " text," +
			Event_ReportedBy + " text," +
			Event_ReleaseDate + " text," +
			Event_Description + " text," +
			Event_WebUrl + " text," +
			Event_IsAlert + " text," +
			Event_AlertLevel + " text," +
			Event_HeldDate + " text" +
			");";

			System.out.println(EventTableQueryString);
			db.execSQL(EventTableQueryString);
		}
 
 
		@Override
		public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
		{
			// NOTHING TO DO HERE. THIS IS THE ORIGINAL DATABASE VERSION.
			// OTHERWISE, YOU WOULD SPECIFIY HOW TO UPGRADE THE DATABASE.
		}
	}
}
