package com.rtsmsproject.www1.classes;

public class UserProfile {
//{"FirstName":"ali","HomeLocation":"1","LastName":"aziz","OfficeLocation":"1","Password":"abc","UserName":"ali"}
	private String FirstName;
	private String LastName;
	private String UserName;
	private String Password;
	private String Email;
	private String HomeLocation;
	private String OfficeLocation;
	public String getFirstName()
	{
		return FirstName;
	}
	public void setFirstName(String str)
	{
		FirstName = str;
	}
	public String getLastName()
	{
		return LastName;
	}
	public void setLastName(String str)
	{
		LastName = str;
	}
	public String getEmail()
	{
		return Email;
	}
	public void setEmail(String str)
	{
		Email = str;
	}
	public String getUserName()
	{
		return UserName;
	}
	public void setUserName(String str)
	{
		UserName = str;
	}
	public String getPassword()
	{
		return Password;
	}
	public void setPassword(String str)
	{
		Password = str;
	}
	public String getHomeLocation()
	{
		return HomeLocation;
	}
	public void setHomeLocation(String str)
	{
		HomeLocation = str;
	}
	public String getOfficeLocation()
	{
		return OfficeLocation;
	}
	public void setOfficeLocation(String str)
	{
		OfficeLocation = str;
	}
}
