package com.rtsmsproject.www1;

import java.util.ArrayList;

import com.rtsmsproject.www.webservice.EventAggregator;
import com.rtsmsproject.www1.DetailView;
import com.rtsmsproject.www1.MenuItems;
import com.rtsmsproject.www1.R;
import com.rtsmsproject.www1.DontGo.MenuItemAdapter;
import com.rtsmsproject.www1.ListViewWrapper.Adapter;
import com.rtsmsproject.www1.ListViewWrapper.Dataset;
import com.rtsmsproject.www1.ListViewWrapper.ListAdapterScrollListener;

import android.app.ListActivity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.AdapterView.OnItemClickListener;

/*
 * @author Binil Thomas
 * Date:20/may/2010
 */
public class ImageActivity extends ListActivity {
	public ArrayList<MenuItems> items = new ArrayList<MenuItems>();
    public static final String PREFS_NAME = "MyPrefsFile";

    /** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
	        // Restore preferences
		SharedPreferences prefs = getSharedPreferences(PREFS_NAME, 0);
		Double latt = Double.valueOf(prefs.getString("Lattitude","0.0"));
		Double lngt = Double.valueOf(prefs.getString("Longitude","0.0"));
/*        final EventAggregator ea = new EventAggregator(this,latt,lngt);
        
        try{
             items = ea.GetEvent(SettingUrls.ScreenName_DontGo);
		}
		catch(Exception ex){
			ex.printStackTrace();
		}
*/

	   	 Adapter mAdapter=new Adapter(this,R.layout.listview);
         setListAdapter(mAdapter);
         ListView MenuBar1 = (ListView) findViewById(android.R.id.list);
 		 MenuBar1.setAdapter(mAdapter);
 		 MenuBar1.setTextFilterEnabled(true);
 		 MenuBar1.setOnItemClickListener(new OnItemClickListener() {

 		public void onItemClick(AdapterView<?>parent, View view, int position, long arg3) {

 			}
 		});
         getListView().setOnScrollListener(new ListAdapterScrollListener(mAdapter));
        
      }
    public void onLoad()
    {
    }
}