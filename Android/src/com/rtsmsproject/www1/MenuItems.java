package com.rtsmsproject.www1;

public class MenuItems {


	private int type = -1;
	// ---------------------------------
	// -- All the possible item types --
	// ---------------------------------
	public static final int Killing 			= 	0;
	public static final int BombBlast 			= 	1;
	public static final int Trafffic 			= 	2;
	public static final int Others 				= 	3;
	public static final int BusinessFinance 	= 	4;
	public static final int LawCrime 			= 	5;
	public static final int DisasterAccident 	=	6;
	public static final int WarConflict 		= 	7;
	public static final int TechnologyInternet	=	8;
	public static final int SocialIssues		= 	9;
	public static final int Politics 			= 	10;
	public static final int Sports 				= 	11;
	public static final int ConferenceCall 		= 	12;
	public static final int ManMadeDisaster 	= 	13;
	public static final int VotingResult 		= 	14;

    private String rssitemID;
    private String EventIconUrl;
    private Double Longitude; ;
    private Double Lattitude ;
    private String LocationName ;
    private String EventName ;
    private String ScreenType;
    private String Distance ;
    private String ReportedBy ;
    private String ReleaseDate ;
    private String Description ;
    private String WebUrl ;
	private Boolean IsAlert ;
	private Integer AlertLevel ;
	private String HeldDate ;

	// public static final int ...
	/* --------------------------------- */

	public MenuItems(String rssitemid,String iconurl,Double longi,Double latti, String locname,String evntname,String screentype,String distance,String reportedby,String releasedate,String description,String  weburl,Boolean isalert,Integer alertlevel, String helddate,int type)
	{
        this.AlertLevel = alertlevel;
        this.Description = description;
        this.Distance = distance;
        this.EventIconUrl = iconurl;
        this.EventName = evntname;
        this.HeldDate = helddate;
        this.IsAlert = isalert;
        this.Longitude = longi;
        this.Lattitude = latti;
        this.ScreenType = screentype;
        this.LocationName = locname;
        this.ReleaseDate = releasedate;
        this.ReportedBy = reportedby;
        this.rssitemID = rssitemid;
        this.WebUrl = weburl;
		this.type = type;
	}

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
    
	public int getType() {
		return type;
	}

	public void setType(int type) {
		this.type = type;
	}


}