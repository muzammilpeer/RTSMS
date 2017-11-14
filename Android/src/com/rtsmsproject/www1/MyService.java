package com.rtsmsproject.www1;

import com.rtsmsproject.www.webservice.EventAggregator;
import com.rtsmsproject.www1.R;
import com.rtsmsproject.www1.SettingUrls;

import android.app.Service;
import android.content.Intent;
import android.content.SharedPreferences;
import android.media.MediaPlayer;
import android.os.Handler;
import android.os.IBinder;
import android.util.Log;
import android.widget.Toast;

public class MyService extends Service {
	private static final String TAG = "RTSMS Service";
    public static final String PREFS_NAME = "MyPrefsFile";
	
	@Override
	public IBinder onBind(Intent intent) {
		return null;
	}
	
	@Override
	public void onCreate() {
		Log.d(TAG, "onCreate");
		WebServiceCall();
	}
	private void WebServiceCall()
	{
		 // Restore preferences
		SharedPreferences prefs = getSharedPreferences(PREFS_NAME, 0);
		Double latt = Double.valueOf(prefs.getString("Lattitude","0.0"));
		Double lngt = Double.valueOf(prefs.getString("Longitude","0.0"));
        final EventAggregator ea = new EventAggregator(this,latt,lngt);
        
        try
        {
            final Handler handler=new Handler();
            final Runnable r = new Runnable()
            {
                public void run() 
                {
                	ea.DownloadEvent(SettingUrls.ScreenName_DontGo);
   	                handler.postDelayed(this, 30*1000);
                	ea.DownloadEvent(SettingUrls.ScreenName_LastCates);
   	                handler.postDelayed(this, 30*1000);
                	ea.DownloadEvent(SettingUrls.ScreenName_MyNearBy);
   	                handler.postDelayed(this, 30*1000);
                	ea.DownloadEvent(SettingUrls.ScreenName_TodayEvent);
   	                handler.postDelayed(this, 30*1000);
                	ea.DownloadEvent(SettingUrls.ScreenName_HistoryEvent);
   	                handler.postDelayed(this, 30*1000);
                	ea.DownloadEvent(SettingUrls.ScreenName_FutureEvent);
   	                handler.postDelayed(this, 30*1000);
                }
            };

            handler.postDelayed(r, 30*1000);

		}
		catch(Exception ex){
			ex.printStackTrace();
		}
	}
	@Override
	public void onDestroy() {
		Log.d(TAG, "onDestroy");
	}
	
	@Override
	public void onStart(Intent intent, int startid) {
		WebServiceCall();
		Log.d(TAG, "onStart");
	}
}
