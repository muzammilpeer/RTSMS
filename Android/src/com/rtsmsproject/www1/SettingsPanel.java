package com.rtsmsproject.www1;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.net.Uri;
import android.os.Bundle;
import android.preference.Preference;
import android.preference.Preference.OnPreferenceClickListener;
import android.preference.Preference.OnPreferenceChangeListener;
import android.preference.PreferenceActivity;
import android.preference.PreferenceManager;
import android.widget.Toast;

public class SettingsPanel extends PreferenceActivity {

	boolean CheckboxPreference;
    String ListPreference;
    String editTextPreference;
    String ringtonePreference;
    String secondEditTextPreference;
    String customPref;

    static public boolean CheckboxShowDistance; 
    static public String ListDistance;
    static public String ListDate;
    static public String ListRisk;
    static public String CustomSetup; 
    static public String ListUnit;
    static public String CustomFeedback;
    static public String CustomLogs;
    static public String CustomRate;
    static public String ListTrack;

    /*boolean CheckboxShowDistance; 
    String ListDistance;
    String ListDate;
    String ListRisk;
    String CustomSetup; 
    String ListUnit;
    String CustomFeedback;
    String CustomLogs;
    String CustomRate;
    String ListTrack;
*/
	@Override
    protected void onStart()
    {
		super.onStart();
    	getPrefs();
    }
   
/*    protected void onRestart()
    {}

    protected void onResume()
    {}
    protected void onPause()
    {}

    protected void onStop()
    {}

    protected void onDestroy()
    {}
  */  
    private void getPrefs() {
            // Get the xml/settingspanel.xml preferences
            SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(getBaseContext());
            CheckboxShowDistance = prefs.getBoolean("checkboxShowDistance", true);
            ListDistance = prefs.getString("listDistance","10");
            ListDate = prefs.getString("listDate","1");
            ListRisk = prefs.getString("listRisk","1");
            ListUnit = prefs.getString("listUnit","1");
            ListTrack = prefs.getString("listTrack","1");
            CustomSetup  = prefs.getString("customSetup", "");
            CustomFeedback  = prefs.getString("customFeedback", "");
            CustomLogs  = prefs.getString("customLogs", "");
            CustomRate  = prefs.getString("customRate", "");

/*            CheckboxPreference = prefs.getBoolean("checkboxPref", true);
            ListPreference = prefs.getString("listPref", "nr1");
            editTextPreference = prefs.getString("editTextPref",
                            "Nothing has been entered");
            ringtonePreference = prefs.getString("ringtonePref",
                            "DEFAULT_RINGTONE_URI");
            secondEditTextPreference = prefs.getString("SecondEditTextPref",
                            "Nothing has been entered");
            // Get the custom preference
            SharedPreferences mySharedPreferences = getSharedPreferences(
                            "myCustomSharedPrefs", Activity.MODE_PRIVATE);
            customPref = mySharedPreferences.getString("myCusomPref", "");*/
    }
    private void LoadListner()
    {
    	Preference customPref = (Preference) findPreference("customFeedback");
        customPref.setOnPreferenceClickListener(new OnPreferenceClickListener() {
            public boolean onPreferenceClick(Preference preference) {
	    		Intent act = new Intent(Intent.ACTION_VIEW, Uri.parse("http://rtsms.dyndns.info"));
		        startActivity(act);    	
                return true;
            }
        });
    	Preference customPref1 = (Preference) findPreference("customLogs");
        customPref1.setOnPreferenceClickListener(new OnPreferenceClickListener() {
            public boolean onPreferenceClick(Preference preference) {
	    		Intent act = new Intent(Intent.ACTION_VIEW, Uri.parse("http://rtsms.dyndns.info"));
		        startActivity(act);    	
                return true;
            }
        });
        Preference customPref2 = (Preference) findPreference("customRate");
        customPref2.setOnPreferenceClickListener(new OnPreferenceClickListener() {
            public boolean onPreferenceClick(Preference preference) {
	    		Intent act = new Intent(Intent.ACTION_VIEW, Uri.parse("http://rtsms.dyndns.info"));
		        startActivity(act);    	
                return true;
            }
        });
    }
    
	@Override
    protected void onCreate(Bundle savedInstanceState) {
            super.onCreate(savedInstanceState);
            
            
            getWindow().setBackgroundDrawableResource(R.drawable.rtsmswallpaper_gradient); 
            getListView().setBackgroundColor(Color.TRANSPARENT); 
            getListView().setCacheColorHint(Color.TRANSPARENT);
            addPreferencesFromResource(R.xml.settingspanel);
            LoadListner();
           // SharedPreferences customPref = PreferenceManager
           // .getDefaultSharedPreferences(getBaseContext());
           // CheckboxPreference = customPref.getBoolean("checkboxPref", true);

            
            // Get the custom preference  
/*          Preference customPref = (Preference) findPreference("customPref");
          
            customPref.setOnPreferenceClickListener(new OnPreferenceClickListener() {
	            public boolean onPreferenceClick(Preference preference) {
	                SharedPreferences prefs = PreferenceManager
	                .getDefaultSharedPreferences(getBaseContext());
	                //CheckboxPreference = prefs.getBoolean("checkboxShowDistance", false);
	                Toast.makeText(getApplicationContext(), ListDistance, 1).show();
	                if (CheckboxPreference == true )
	                {
	                    Toast.makeText(getBaseContext(),
	                                    "The custom preference has been clicked",
	                                    Toast.LENGTH_LONG).show();
	                }  
	                  /* SharedPreferences customSharedPreference = getSharedPreferences(
	                                    "myCustomSharedPrefs", Activity.MODE_PRIVATE);
	                    SharedPreferences.Editor editor = customSharedPreference
	                                    .edit();
	                    editor.putString("myCustomPref",
	                                    "The preference has been clicked");
	                    editor.commit();
	                    
	                return true;
	            }


            });*/
    }
}
