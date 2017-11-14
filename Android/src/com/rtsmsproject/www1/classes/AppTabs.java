package com.rtsmsproject.www1.classes;

import com.rtsmsproject.www1.FutureEvent;
import com.rtsmsproject.www1.HistoryEvent;
import com.rtsmsproject.www1.ImageActivity;
import com.rtsmsproject.www1.R;
import com.rtsmsproject.www1.TodayEvent;

import android.content.Context;
import android.content.Intent;
import android.widget.TabHost;
import android.widget.TabHost.OnTabChangeListener;
import android.widget.TabHost.TabSpec;

public class AppTabs {

	public static void setMyTabs(TabHost tabHost, Context context){
        TabSpec firstTabSpec = tabHost.newTabSpec("tid1");
        TabSpec secondTabSpec = tabHost.newTabSpec("tid1");
        TabSpec thirdTabSpec = tabHost.newTabSpec("tid1");

        firstTabSpec.setIndicator("Today Event", context.getResources().getDrawable(R.anim.btnmaptag));
        secondTabSpec.setIndicator("History Event", context.getResources().getDrawable(R.anim.btnmaptag));
        thirdTabSpec.setIndicator("Future Event", context.getResources().getDrawable(R.anim.btnmaptag));
        
        firstTabSpec.setContent(new Intent(context, TodayEvent.class));
        secondTabSpec.setContent(new Intent(context, HistoryEvent.class));
        thirdTabSpec.setContent(new Intent(context, FutureEvent.class));
        
        tabHost.addTab(firstTabSpec);
        tabHost.addTab(secondTabSpec);
        tabHost.addTab(thirdTabSpec);
     
        tabHost.getTabWidget().setCurrentTab(0);
        tabHost.setOnTabChangedListener(MyOnTabChangeListener);
        

        // Setting BackGround
//      for(int i=0; i<tabHost.getTabWidget().getChildCount(); i++)
//      {
//      	tabHost.getTabWidget().getChildAt(i).setBackgroundColor(Color.WHITE);
//      }
//      
//      tabHost.getTabWidget().setCurrentTab(1);
//      tabHost.getTabWidget().getChildAt(1).setBackgroundColor(Color.GRAY);
      
	}

	private static OnTabChangeListener MyOnTabChangeListener = new OnTabChangeListener(){
		public void onTabChanged(String tabId) {
//			for(int i=0;i<tabHost.getTabWidget().getChildCount();i++)
//	        {
//	        	tabHost.getTabWidget().getChildAt(i).setBackgroundColor(Color.WHITE);
//	        } 
//					
//			tabHost.getTabWidget().getChildAt(tabHost.getCurrentTab()).setBackgroundColor(Color.GRAY);
		}		
	};

}
