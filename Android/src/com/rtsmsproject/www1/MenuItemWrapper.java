package com.rtsmsproject.www1;


import android.view.MenuItem;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

public class MenuItemWrapper {
	private TextView locationtext=null;
	private TextView title=null;
	private TextView postedbytext=null;
	private TextView timeagotext=null;
	private TextView description=null;
	private TextView distance=null;
	private ImageView icon=null;
	private View row=null;

	public MenuItemWrapper(View row) {
		this.row=row; //The View object that represents a single row
	}

	public void populateFrom(MenuItems r) { //Associate to the item components the value from the menu bean
		getLocation().setText(r.getLocationName());
		getTitle().setText(r.getEventName());
		getPostedBy().setText(r.getReportedBy());
		getTimeAgo().setText(r.getReleaseDate());
		getDescription().setText(r.getDescription());
		getDistance().setText(r.getDistance());
		if (r.getType() == MenuItems.Killing) { //Here we use the item type to associate the right icon
			getIcon().setImageResource(R.drawable.gun);
		}
		else if (r.getType() == MenuItems.BombBlast) {
			getIcon().setImageResource(R.drawable.blast);
		}
		else if (r.getType() == MenuItems.Trafffic ) {
			getIcon().setImageResource(R.drawable.traffic);
		}
		else if (r.getType() == MenuItems.Others ) {
			getIcon().setImageResource(R.drawable.maps_menu);
		}
	}

	TextView getLocation() {
		if (locationtext==null) {
			locationtext=(TextView)row.findViewById(R.id.locationtext);
		}
		return(locationtext);
	}
	TextView getDistance() {
		if (distance==null) {
			distance=(TextView)row.findViewById(R.id.distancetext);
		}
		return(distance);
	}

	TextView getTitle() {
		if (title==null) {
			title=(TextView)row.findViewById(R.id.title);
		}
		return(title);
	}
	TextView getPostedBy() {
		if (postedbytext==null) {
			postedbytext=(TextView)row.findViewById(R.id.postedbytext);
		}
		return(postedbytext);
	}
	TextView getTimeAgo() {
		if (timeagotext==null) {
			timeagotext=(TextView)row.findViewById(R.id.timeagotext);
		}
		return(timeagotext);
	}

	TextView getDescription() {
		if (description == null) {
			description=(TextView)row.findViewById(R.id.description);
		}
		return(description);
	}

	ImageView getIcon() {
		if (icon==null) {
			icon=(ImageView)row.findViewById(R.id.icon);
		}
		return(icon);
	}
}