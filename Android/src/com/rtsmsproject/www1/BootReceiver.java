package com.rtsmsproject.www1;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;

public class BootReceiver extends BroadcastReceiver {
	@Override
	public void onReceive(Context context, Intent intent) {
        Intent serviceIntent = new Intent(com.rtsmsproject.www1.MyService.class.getName());
        context.startService(serviceIntent); 
	}
}
