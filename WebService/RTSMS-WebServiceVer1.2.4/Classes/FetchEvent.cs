using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace RTSMSWebService.Classes
{
    public class FetchEvent
    {
        List<String> query = new List<String>();
        public FetchEvent()
        {
            query.Add("SELECT [demoEventID] ,[RSSItemID] ,[CategoryID] ,[Title] ,[demoEvent].[Description],[Link],[RiskLevel],[Status],[Alert],[Message],[pubDate],[heldDate],[Location].LocationName  FROM [demoEvent] inner join [Location] on ([demoEvent].LocationID = [Location] .LocationID  and [demoEvent].[isdontgo] = 1 );");
            query.Add("SELECT [demoEventID] ,[RSSItemID] ,[CategoryID] ,[Title] ,[demoEvent].[Description],[Link],[RiskLevel],[Status],[Alert],[Message],[pubDate],[heldDate],[Location].LocationName  FROM [demoEvent] inner join [Location] on ([demoEvent].LocationID = [Location] .LocationID  and [demoEvent].[iscatastrophe] = 1 );");
            query.Add("SELECT [demoEventID] ,[RSSItemID] ,[CategoryID] ,[Title] ,[demoEvent].[Description],[Link],[RiskLevel],[Status],[Alert],[Message],[pubDate],[heldDate],[Location].LocationName  FROM [demoEvent] inner join [Location] on ([demoEvent].LocationID = [Location] .LocationID  and [demoEvent].[isnearby] = 1 );");
            query.Add("SELECT [demoEventID] ,[RSSItemID] ,[CategoryID] ,[Title] ,[demoEvent].[Description],[Link],[RiskLevel],[Status],[Alert],[Message],[pubDate],[heldDate],[Location].LocationName  FROM [demoEvent] inner join [Location] on ([demoEvent].LocationID = [Location] .LocationID  and [demoEvent].[ispresent] = 1 );");
            query.Add("SELECT [demoEventID] ,[RSSItemID] ,[CategoryID] ,[Title] ,[demoEvent].[Description],[Link],[RiskLevel],[Status],[Alert],[Message],[pubDate],[heldDate],[Location].LocationName  FROM [demoEvent] inner join [Location] on ([demoEvent].LocationID = [Location] .LocationID  and [demoEvent].[ishistory] = 1 );");
            query.Add("SELECT [demoEventID] ,[RSSItemID] ,[CategoryID] ,[Title] ,[demoEvent].[Description],[Link],[RiskLevel],[Status],[Alert],[Message],[pubDate],[heldDate],[Location].LocationName  FROM [demoEvent] inner join [Location] on ([demoEvent].LocationID = [Location] .LocationID  and [demoEvent].[isfuture] = 1 );");
        }

        public List<EventTable> GetEventFromDatabase(String screentype)
        {
            List<EventTable> results = new List<EventTable>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = Properties.Settings.Default.RTSMSdbConnectionString;
                // 1.  create a command object identifying
                //     the stored procedure
                SqlDataReader reader = null;
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                }
                try
                {
                    String querystring = null;

                    if (screentype == "lastcates")
                    {
                        querystring = query[1];
                    }
                    else if (screentype == "mynearby")
                    {
                        querystring = query[2];
                    }
                    else if (screentype == "todayevent")
                    {
                        querystring = query[3];
                    }
                    else if (screentype == "historyevent")
                    {
                        querystring = query[4];
                    }
                    else if (screentype == "futureevent")
                    {
                        querystring = query[5];
                    }
                    else if (screentype == "dontgo")
                    {
                        querystring = query[0];
                    }
                    else {
                        querystring = query[0];
                    }
                    


                    SqlCommand cmd = new SqlCommand(querystring, conn);

                    // execute the command
                    reader = cmd.ExecuteReader();

                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                        { 
                          /*
                          * results.Add(new EventTable(rssitemid, "/rtsms_pic/maps_icons/strike.png", 0.0, 0.0, locationname, eventtype, screentype, "2.1", "RTSMS News", pubdate, Description, "http://www.geo.tv/", isalertType, risklevel, helddate));
                            "SELECT [demoEventID] ,[RSSItemID] ,[CategoryID] ,[Title] ,[demoEvent].[Description],[Link],[RiskLevel],[Status],[Alert],[Message],[pubDate],[heldDate],[Location].LocationName  FROM [demoEvent] inner join [Location] on ([demoEvent].LocationID = [Location] .LocationID  and [demoEvent].[isdontgo] = 1 );");
                          */
                            String pubdate = reader.GetDateTime(11).ToString(); // pub
                            String Description = reader.GetString(4);// descr
                            String Link = reader.GetString(5); // link
                            String locationname = reader.GetString(12); //locaname
                            String eventtype = reader.GetString(3); // eventtype
                            String rssitemid = reader.GetString(1); // rssitemid
                            Int32 risklevel = reader.GetInt32(6); // riskleve
                            //bool isalertType = Boolean.Parse(reader.GetInt32(8).ToString());// alert
                            String helddate = reader.GetDateTime(11).ToString(); // held
                            results.Add(new EventTable(rssitemid, "/rtsms_pic/maps_icons/strike.png", 0.0, 0.0, locationname, eventtype, screentype, "2.1", "RTSMS News", pubdate, Description, Link, false, risklevel, helddate));

                        }
                    }
                    else
                    {
                        results.Add(new EventTable("123131", "/rtsms_pic/maps_icons/strike.png", 0.0, 0.0, "no location", "no event", screentype, "0.0", "RTSMS News", "", "No Data", "http://www.geo.tv/", false, 0, ""));
                    }
                }
                catch (Exception ex)
                {
                    results.Add(new EventTable("123131", "/rtsms_pic/maps_icons/strike.png", 0.0, 0.0, "error", "no event", screentype, "0.0", "RTSMS News", "", "No Data", "http://www.geo.tv/", false, 0, ""));
                }
            }
            return results;
        }

    }
}