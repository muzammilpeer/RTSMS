package com.rtsmsproject.www1;

import android.app.TabActivity;
import android.content.Intent;
import android.os.Bundle;
import android.widget.TabHost;
import android.widget.TextView;
import android.widget.TabHost.TabSpec;

public class FirstActivity extends TabActivity {
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
		/* Second Tab Content */
		TextView textView = new TextView(this);
		textView.setText("Second Tab");
		setContentView(textView);

    }
}
