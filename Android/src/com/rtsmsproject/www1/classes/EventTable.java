package com.rtsmsproject.www1.classes;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.text.DateFormat;

import org.json.JSONException;
import org.json.JSONObject;

public class EventTable {

    public String rssitemID;
    public String EventIconUrl;
	public Double Longitude; ;
	public Double Lattitude ;
	public String LocationName ;
	public String EventName ;
	public String ScreenType;
	public String Distance ;
	public String ReportedBy ;
	public String ReleaseDate ;
	public String Description ;
	public String WebUrl ;
	public Boolean IsAlert ;
	public Integer AlertLevel ;
	public String HeldDate ;

	public String getWebUrl()
    {
    	return WebUrl;
    }
    public void setWebUrl(String str) {
    	WebUrl = str;
    }

    public String getRSSItemID()
    {
    	return rssitemID;
    }
    public void setRSSItemID(String str) {
    	rssitemID = str;
    }

	public String getEventIconUrl()
    {
    	return EventIconUrl;
    }
    public void setEventIconUrl(String str) {
    	EventIconUrl = str;
    }

	public Double getLongitude()
    {
    	return Longitude;
    }
    public void setLongitude(Double str) {
    	Longitude = str;
    }

	public Double getLattitude()
    {
    	return Lattitude;
    }
    public void setLattitudeD(Double str) {
    	Lattitude = str;
    }

	public String getLocationName()
    {
    	return LocationName;
    }
    public void setLocationName(String str) {
    	LocationName = str;
    }

	public String getEventName()
    {
    	return EventName;
    }
    public void setEventName(String str) {
    	EventName = str;
    }

	public String getScreenType()
    {
    	return ScreenType;
    }
    public void setScreenType(String str) {
    	ScreenType = str;
    }

	public String getDistance()
    {
    	return Distance;
    }
    public void setDistance(String str) {
    	Distance = str;
    }

    public String getReportedBy()
    {
    	return ReportedBy;
    }
    public void setReportedBy(String str) {
    	ReportedBy = str;
    }

    public String getReleaseDate()
    {
       // DateFormat dateFormat = new SimpleDateFormat("yyyy/MM/dd HH:mm:ss");
    	//return dateFormat.format(ReleaseDate);
    	return ReleaseDate;
    }
    public void setReleaseDate(String str) {
    	ReleaseDate = str;
    }

    public String getDescription()
    {
    	return Description;
    }
    public void setDescription(String str) {
    	Description = str;
    }

    public Boolean getIsAlert()
    {
    	return IsAlert;
    }
    public void setIsAlert(Boolean str) {
    	IsAlert = str;
    }

    public Integer getAlertLevel()
    {
    	return AlertLevel;
    }
    public void setAlertLevel(Integer str) {
    	AlertLevel = str;
    }

    public String getHeldDate()
    {
       // DateFormat dateFormat = new SimpleDateFormat("yyyy/MM/dd HH:mm:ss");
    	//return dateFormat.format(HeldDate);
    	return HeldDate;
    }
    public void setHeldDate(String str) {
    	HeldDate = str;
    }
    
    
}
