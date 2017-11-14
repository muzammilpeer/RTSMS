using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Data.SqlClient;
using System.Transactions;
using RTSMSWebService.Classes;
using BenjaminSchroeter.GeoNames;
namespace RTSMSWebService
{
    // (TransactionIsolationLevel = System.Transactions.IsolationLevel.Serializable, TransactionTimeout = "00:00:30", TransactionScopeOption.Required)
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    [ServiceBehavior]
    public class WebService : IWebService
    {
        /*  Track User ipaddress and port no
         * OperationContext context = OperationContext.Current;
            MessageProperties messageProperties = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpointProperty =
                messageProperties[RemoteEndpointMessageProperty.Name]
                as RemoteEndpointMessageProperty;
 
            return string.Format("Hello {0}! Your IP address is {1} and your port is {2}",
                value, endpointProperty.Address, endpointProperty.Port);
        
        */
        /* Double latt1 = 24.8601083
         * Double lngi1 = 67.2162619
         * Double latt2 = 24.8933790
         * Double lngi2 = 67.0280610
         *
         *  double e=(3.1415926538*latt1/180);
            double f=(3.1415926538*lngi1/180);
            double g=(3.1415926538*latt2/180);
            double h=(3.1415926538*lngi2/180);
            double i=(Math.cos(e)*Math.cos(g)*Math.cos(f)*Math.cos(h)+Math.cos(e)*Math.sin(f)*Math.cos(g)*Math.sin(h)+Math.sin(e)*Math.sin(g));
            double j=(Math.acos(i));
            double k=(6371*j);

            return k; 
         */

        // Database Connection String in Resources.Settings 
        public string RTSMSdbConnectionString = Properties.Settings.Default.RTSMSdbConnectionString;

        // Main Page Status Endpoint function 
        public Status GetHomePageStatus(Double latt, Double lngt)
        {
            Status tmp = new Status();
            tmp.AlertType = true;  // 1 == Safe Zone, 0 == Danger Zone
            tmp.Distance = 45.12;
            tmp.UnitTypeDistance = true;  // 1 == Miles, 0 == KM
            return tmp;
        }

        public List<EventTable> GetTodayUpdates(Double latt, Double lngt)
        {
            //String currentlocation = GetLocationName(latt, lngt);
            List<EventTable> results = new List<EventTable>();
          /*  String query = "select * from [Event],[Location] where (Event.ispresent=1 and Event.LocationID = (select Location.LocationID from [Location] where (Location.LocationName='" + currentlocation + "')))and Location.LocationID=Event.LocationID; ";
            using (SqlConnection sql = new SqlConnection())
            {
                sql.ConnectionString = RTSMSdbConnectionString;
                SqlCommand cmd = new SqlCommand(query, sql);
                try
                {
                    sql.Open();
                }
                catch (Exception ex)
                {
                    DateTime tm = DateTime.Now;
                    results.Add(new EventTable(1, "123", true, false, tm.ToString(), tm.ToString(), "Killed !.Quadidabd,Karachi", "3 people killed in Quaidabad.", 67.2162619, 24.8601083, "12"));
                    var connectionFault = new ConnectionFault();
                    connectionFault.Operation = "UpdateAccount";
                    connectionFault.Reason =
                    "Can't connect to the database";
                    connectionFault.Details = ex.Message;
                    throw new FaultException<ConnectionFault>(
                    connectionFault);
                }
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows == true)
                    { 
  
                    DateTime tm = DateTime.Now;

                    while (reader.Read())
                        {
                          // bool status = reader.GetBoolean(8);
                           //string alert = reader.GetString(8);
                            results.Add(new EventTable(reader.GetInt32(0), reader.GetString(1), true, false, tm.ToString(), tm.ToString(), reader.GetString(4), reader.GetString(5), reader.GetDouble(18), reader.GetDouble(19), "45"));
                        }
                    }
                    else 
                    {
                        DateTime tm = DateTime.Now;
                        results.Add(new EventTable(1, " ", true, false, tm.ToString(), tm.ToString(), "No Result", "No Result", 0.0, 0.0, "45"));
                    }
                    

                }
                catch (Exception ex)
                {
                    var dataFault = new DataFault();
                    dataFault.Operation = "AddOrder";
                    dataFault.Reason = "ErrError writing to the database";
                    dataFault.Details = ex.Message;
                    throw new FaultException<DataFault>(dataFault,
                      new FaultReason("Error writing to the database"));
                }
            }
            */
            return results;
        }


        public List<EventTable> GetHistory(Double latt, Double lngt)
        {
         //   String currentlocation = GetLocationName(latt, lngt);
            List<EventTable> results = new List<EventTable>();
          /*
            String query = "select * from [Event],[Location] where (Event.ishistory=1 and Event.LocationID = (select Location.LocationID from [Location] where (Location.LocationName='" + currentlocation + "')))and Location.LocationID=Event.LocationID;";
            using (SqlConnection sql = new SqlConnection())
            {
                sql.ConnectionString = RTSMSdbConnectionString;
                SqlCommand cmd = new SqlCommand(query, sql);
                try
                {
                    sql.Open();
                }
                catch (Exception ex)
                {
                    DateTime tm = DateTime.Now;
                    results.Add(new EventTable(1, "123", true, false, tm.ToString(), tm.ToString(), "Killed !.Quadidabd,Karachi", "3 people killed in Quaidabad.", 67.2162619, 24.8601083, "45"));
                    var connectionFault = new ConnectionFault();
                    connectionFault.Operation = "UpdateAccount";
                    connectionFault.Reason =
                    "Can't connect to the database";
                    connectionFault.Details = ex.Message;
                    throw new FaultException<ConnectionFault>(
                    connectionFault);
                }
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows == true)
                    {

                        DateTime tm = DateTime.Now;

                        while (reader.Read())
                        {
                            // bool status = reader.GetBoolean(8);
                            //string alert = reader.GetString(8);
                            results.Add(new EventTable(reader.GetInt32(0), reader.GetString(1), true, false, tm.ToString(), tm.ToString(), reader.GetString(4), reader.GetString(5), reader.GetDouble(18), reader.GetDouble(19), "45"));
                        }
                    }
                    else
                    {
                        DateTime tm = DateTime.Now;
                        results.Add(new EventTable(1, " ", true, false, tm.ToString(), tm.ToString(), "No Result", "No Result", 0.0, 0.0, "45"));
                    }


                }
                catch (Exception ex)
                {
                    DateTime tm = DateTime.Now;
                    results.Add(new EventTable(1, "123", true, false, tm.ToString(), tm.ToString(), "Killed !.Quadidabd,Karachi", "3 people killed in Quaidabad.", 67.2162619, 24.8601083, "45"));
                    var dataFault = new DataFault();
                    dataFault.Operation = "AddOrder";
                    dataFault.Reason = "ErrError writing to the database";
                    dataFault.Details = ex.Message;
                    throw new FaultException<DataFault>(dataFault,
                      new FaultReason("Error writing to the database"));
                }
            }
            */
            return results;
        }

        public List<EventTable> GetFutureEventTable(Double latt,Double lngt)
        {
          //  String currentlocation = GetLocationName(latt, lngt);
            List<EventTable> results = new List<EventTable>();
           /* String query = "select * from [Event],[Location] where (Event.isfuture=1 and Event.LocationID = (select Location.LocationID from [Location] where (Location.LocationName='" + currentlocation + "')))and Location.LocationID=Event.LocationID;";
            using (SqlConnection sql = new SqlConnection())
            {
                sql.ConnectionString = RTSMSdbConnectionString;
                SqlCommand cmd = new SqlCommand(query, sql);
                try
                {
                    sql.Open();
                }
                catch (Exception ex)
                {
                    DateTime tm = DateTime.Now;
                    results.Add(new EventTable(1, "123", true, false, tm.ToString(), tm.ToString(), "Killed !.Quadidabd,Karachi", "3 people killed in Quaidabad.", 67.2162619, 24.8601083, "45"));
                    var connectionFault = new ConnectionFault();
                    connectionFault.Operation = "UpdateAccount";
                    connectionFault.Reason =
                    "Can't connect to the database";
                    connectionFault.Details = ex.Message;
                    throw new FaultException<ConnectionFault>(
                    connectionFault);
                }
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows == true)
                    {

                        DateTime tm = DateTime.Now;

                        while (reader.Read())
                        {
                            // bool status = reader.GetBoolean(8);
                            //string alert = reader.GetString(8);
                            results.Add(new EventTable(reader.GetInt32(0), reader.GetString(1), true, false, tm.ToString(), tm.ToString(), reader.GetString(4), reader.GetString(5), reader.GetDouble(18), reader.GetDouble(19), "45"));
                        }
                    }
                    else
                    {
                        DateTime tm = DateTime.Now;
                        results.Add(new EventTable(1, " ", true, false, tm.ToString(), tm.ToString(), "Tmp", "No Result", 0.0, 0.0, "45"));
                    }


                }
                catch (Exception ex)
                {
                    DateTime tm = DateTime.Now;
                    results.Add(new EventTable(1, "123", true, false, tm.ToString(), tm.ToString(), "Killed !.Quadidabd,Karachi", "3 people killed in Quaidabad.", 67.2162619, 24.8601083, "45"));
                    var dataFault = new DataFault();
                    dataFault.Operation = "AddOrder";
                    dataFault.Reason = "ErrError writing to the database";
                    dataFault.Details = ex.Message;
                    throw new FaultException<DataFault>(dataFault,
                      new FaultReason("Error writing to the database"));
                }
            }
            */
            return results;
        }
        public List<EventTable> GetCatastrophe(Double latt, Double lngt)
        {
           // String currentlocation = GetLocationName(latt, lngt);
            List<EventTable> results = new List<EventTable>();
           /* String query = @"select * from [Event],[Location] where ((Event.ispresent=1) or (Event.ishistory=1) and (Event.pubDate  <= GETDATE() and Event.pubDate >= '" + DateTime.Now.AddDays(-1).ToString() + "') and Event.LocationID = (select Location.LocationID from [Location] where (Location.LocationName='" + currentlocation + "')))and Location.LocationID=Event.LocationID;";
            using (SqlConnection sql = new SqlConnection())
            {
                sql.ConnectionString = RTSMSdbConnectionString;
                SqlCommand cmd = new SqlCommand(query, sql);
                try
                {
                    sql.Open();
                }
                catch (Exception ex)
                {
                    DateTime tm = DateTime.Now;
                    results.Add(new EventTable(1, "123", true, false, tm.ToString(), tm.ToString(), "Killed !.Quadidabd,Karachi", "3 people killed in Quaidabad.", 67.2162619, 24.8601083, "45"));
                    var connectionFault = new ConnectionFault();
                    connectionFault.Operation = "UpdateAccount";
                    connectionFault.Reason =
                    "Can't connect to the database";
                    connectionFault.Details = ex.Message;
                    throw new FaultException<ConnectionFault>(
                    connectionFault);
                }
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows == true)
                    {

                        DateTime tm = DateTime.Now;

                        while (reader.Read())
                        {
                            // bool status = reader.GetBoolean(8);
                            //string alert = reader.GetString(8);
                            //lng 19 lat20
                            results.Add(new EventTable(reader.GetInt32(0), reader.GetString(1), true, false, tm.ToString(), tm.ToString(), reader.GetString(4), reader.GetString(5), reader.GetDouble(18), reader.GetDouble(19), "45"));
                        }
                    }
                    else
                    {
                        DateTime tm = DateTime.Now;
                        results.Add(new EventTable(1, " ", true, false, tm.ToString(), tm.ToString(), "No Result", "No Result", 0.0, 0.0, "45"));
                    }


                }
                catch (Exception ex)
                {
                    DateTime tm = DateTime.Now;
                    results.Add(new EventTable(1, "123", true, false, tm.ToString(), tm.ToString(), "Killed !.Quadidabd,Karachi", "3 people killed in Quaidabad.", 67.2162619, 24.8601083, "45"));
                    var dataFault = new DataFault();
                    dataFault.Operation = "AddOrder";
                    dataFault.Reason = "ErrError writing to the database";
                    dataFault.Details = ex.Message;
                    throw new FaultException<DataFault>(dataFault,
                      new FaultReason("Error writing to the database"));
                }
            }
            */
            return results;
        }

        public List<EventTable> GetDontGo(Double latt, Double lngt)
        {
            DateTime tm = DateTime.Now;
            List<EventTable> results = new List<EventTable>();
          /*  results.Add(new EventTable(1, "123", true, false, tm.ToString(), tm.ToString(), "Sadar,Karachi", "10 people killed in Sadar.", 67.0242519, 24.8517936, "45"));
            results.Add(new EventTable(2, "456", true, false, tm.ToString(), tm.ToString(), "Accident !.KalaBoard,Karachi", "Bus Accident at KalaBoard.", 67.1823501, 24.8822664, "45"));
            results.Add(new EventTable(3, "4567", true, false, tm.ToString(), tm.ToString(), "Malir 15", "Traffic jam from last two hours.", 67.0280610, 24.8933790, "45"));
            */
            return results;
        }

        public List<EventTable> GetMyNearBy(Double latt, Double lngt,Decimal radius)
        {
            //EventTable(string rssitemid,string iconurl,Double longi,Double latti, string locname,string evntname,string distance,string reportedby,string releasedate,string description,string  weburl,Boolean isalert,Int32 alertlevel, string helddate)
            List<EventTable> results = new List<EventTable>();
            String query;
            try
            {
                FindNearBy fn = new FindNearBy(latt, lngt);
                List<WikipediaArticle> wiki;
                if (radius > 0)
                {
                    wiki = fn.GetNearByLocations(radius);
                }
                else
                {
                    wiki = fn.GetNearByLocations(10);
                }
                foreach (WikipediaArticle article in wiki)
                {
//                    DateTime tm;  
                  long dat =  ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000);
                  results.Add(new EventTable("rssitemid","http://192.168.1.1014/logo1.jpg",Double.Parse(article.Longitude.ToString()), Double.Parse(article.Latitude.ToString()),article.Title,"Kill-Firing","NearBy",article.Distance.ToString(), "Geo News",dat.ToString(),article.Summary,"http://www.geo.tv/archive/12334.htm",false,1,dat.ToString()));
                            
                    /*
                    query = @"select * from [Event],[Location] where (Event.pubDate  <= GETDATE() and Event.LocationID = (select Location.LocationID from [Location] where (Location.LocationName='" + article.Title + "'))) and Location.LocationID=Event.LocationID;";
                    using (SqlConnection sql = new SqlConnection())
                    {
                        sql.ConnectionString = RTSMSdbConnectionString;
                        SqlCommand cmd = new SqlCommand(query, sql);
                        try
                        {
                            sql.Open();
                        }
                        catch (Exception ex)
                        {
                            DateTime tm = DateTime.Now;
                            results.Add(new EventTable(1, "123", true, false, tm, tm, "Killed !.Quadidabd,Karachi", "3 people killed in Quaidabad.", 67.2162619, 24.8601083));
                            var connectionFault = new ConnectionFault();
                            connectionFault.Operation = "UpdateAccount";
                            connectionFault.Reason =
                            "Can't connect to the database";
                            connectionFault.Details = ex.Message;
                            throw new FaultException<ConnectionFault>(
                            connectionFault);
                        }
                        try
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            DateTime tm = DateTime.Now;

                            results.Add(new EventTable(0, article.Title, true, false, tm, tm, "", "", 4.1, 2.2));

                          //   if (reader.HasRows == true)
                             {

                                 DateTime tm = DateTime.Now;

                                 while (reader.Read())
                                 {
                                     // bool status = reader.GetBoolean(8);
                                     //string alert = reader.GetString(8);
                                     //lng 19 lat20
                                     results.Add(new EventTable(reader.GetInt32(0), reader.GetString(1), true, false, tm, tm, reader.GetString(4), reader.GetString(5), reader.GetDouble(18), reader.GetDouble(19)));
                                 }
                             }
                             else
                             {
                                 DateTime tm = DateTime.Now;
                                 results.Add(new EventTable(1, " ", true, false, tm, tm, "No Result", "No Result", 0.0, 0.0));
                             }

                             
                            
                        }
                        catch (Exception ex)
                        {
                            DateTime tm = DateTime.Now;
                            results.Add(new EventTable(1, "123", true, false, tm, tm, "Killed !.Quadidabd,Karachi", "3 people killed in Quaidabad.", 67.2162619, 24.8601083));
                            var dataFault = new DataFault();
                            dataFault.Operation = "AddOrder";
                            dataFault.Reason = "ErrError writing to the database";
                            dataFault.Details = ex.Message;
                            throw new FaultException<DataFault>(dataFault,
                              new FaultReason("Error writing to the database"));
                        }
                    } */
                } 
            }
            catch (Exception ex)
            {
                DateTime tm = DateTime.Now;
                //results.Add(new EventTable(1, "123", true, false, tm.ToString(), tm.ToString(), "Killed !.Quadidabd,Karachi", "3 people killed in Quaidabad.", 67.2162619, 24.8601083, "45"));
                var dataFault = new DataFault();
                dataFault.Operation = "AddOrder";
                dataFault.Reason = "ErrError writing to the database";
                dataFault.Details = ex.Message;
                throw new FaultException<DataFault>(dataFault,
                  new FaultReason("Error writing to the database"));
                
            }
                return results;
 
        }

/*        SELECT [demoEventID] ,[RSSItemID] ,[CategoryID] ,[Title] ,[demoEvent].[Description],[Link],[RiskLevel],[Status],[Alert],[Message],[pubDate],[heldDate],[Location].LocationName  FROM [demoEvent] inner join [Location] on ([demoEvent].LocationID = [Location] .LocationID  and [demoEvent].[iscatastrophe] = 1 )    
GO*/




        public List<EventTable> GetEvent(Double latt, Double lngt, Decimal radius, string screentype, string clienttype)
        {

            String query = " SELECT [demoEventID] ,[RSSItemID] ,[CategoryID] ,[Title] ,[demoEvent].[Description],[Link],[RiskLevel],[Status],[Alert],[Message],[pubDate],[heldDate],[Location].LocationName  FROM [demoEvent] inner join [Location] on ([demoEvent].LocationID = [Location] .LocationID  and [demoEvent].[iscatastrophe] = 1 ) ";
            using (SqlConnection sql = new SqlConnection())
            {
                sql.ConnectionString = RTSMSdbConnectionString;
                SqlCommand cmd = new SqlCommand(query, sql);
                try
                {
                    sql.Open();
                }
                catch (Exception ex)
                {
                    DateTime tm = DateTime.Now;
                    var connectionFault = new ConnectionFault();
                    connectionFault.Operation = "UpdateAccount";
                    connectionFault.Reason =
                    "Can't connect to the database";
                    connectionFault.Details = ex.Message;
                    throw new FaultException<ConnectionFault>(
                    connectionFault);
                }
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows == true)
                    {

                        DateTime tm = DateTime.Now;

                        while (reader.Read())
                        {
                            // bool status = reader.GetBoolean(8);
                            //string alert = reader.GetString(8);
                            results.Add(new EventTable(reader.GetInt32(0), reader.GetString(1), true, false, tm.ToString(), tm.ToString(), reader.GetString(4), reader.GetString(5), reader.GetDouble(18), reader.GetDouble(19), "45"));
                            results.Add(new EventTable("1adcdsf", "http://192.168.1.101/rtsms_pic/logo3.jpg", Double.Parse(article.Longitude.ToString()), Double.Parse(article.Latitude.ToString()), article.Title, "Kill-Firing", screentype, article.Distance.ToString(), "AryNews", dat.ToString(), article.Summary + tmp, "http://www.geo.tv/archive/12334.htm", false, i, dat.ToString()));

                        }
                    }
                    else
                    {
//                        DateTime tm = DateTime.Now;
 //                       results.Add(new EventTable("1adcdsf", "http://192.168.1.101/rtsms_pic/logo3.jpg", Double.Parse(article.Longitude.ToString()), Double.Parse(article.Latitude.ToString()), article.Title, "Kill-Firing", screentype, article.Distance.ToString(), "AryNews", dat.ToString(), article.Summary + tmp, "http://www.geo.tv/archive/12334.htm", false, i, dat.ToString()));
                    }


                }
                catch (Exception ex)
                {
                    DateTime tm = DateTime.Now;
                   // results.Add(new EventTable("1adcdsf", "http://192.168.1.101/rtsms_pic/logo3.jpg", Double.Parse(article.Longitude.ToString()), Double.Parse(article.Latitude.ToString()), article.Title, "Kill-Firing", screentype, article.Distance.ToString(), "AryNews", dat.ToString(), article.Summary + tmp, "http://www.geo.tv/archive/12334.htm", false, i, dat.ToString()));
                    var dataFault = new DataFault();
                    dataFault.Operation = "AddOrder";
                    dataFault.Reason = "ErrError writing to the database";
                    dataFault.Details = ex.Message;
                    throw new FaultException<DataFault>(dataFault,
                        new FaultReason("Error writing to the database"));
                    }
                }
   
                    return results;
 
            /*
            //EventTable(string rssitemid,string iconurl,Double longi,Double latti, string locname,string evntname,string distance,string reportedby,string releasedate,string description,string  weburl,Boolean isalert,Int32 alertlevel, string helddate)
            List<EventTable> results = new List<EventTable>();
            String query;
            try
            {
                FindNearBy fn = new FindNearBy(latt, lngt);
                List<WikipediaArticle> wiki;
                if (radius > 0)
                {
                    wiki = fn.GetNearByLocations(radius);
                }
                else
                {
                    wiki = fn.GetNearByLocations(10);
                }
                int i = 0;

                Double latt1 = 24.8601083;
                Double lngi1 = 67.2162619;
                Double latt2 = 24.8933790;
                Double lngi2 = 67.0280610;
         
                double e=(3.1415926538*latt1/180);
                double f=(3.1415926538*lngi1/180);
                double g=(3.1415926538*latt2/180);
                double h=(3.1415926538*lngi2/180);
                double P=(Math.Cos(e)*Math.Cos(g)*Math.Cos(f)*Math.Cos(h)+Math.Cos(e)*Math.Sin(f)*Math.Cos(g)*Math.Sin(h)+Math.Sin(e)*Math.Sin(g));
                double j=(Math.Acos(P));
                double k=(6371*j);

                String tmp = "sdfjlsjflsjfl sjfl sjf lsjf lsjf lsjflsjf klsjf lsjf lsjf lsjf dsl fjsl fjdsl fjsld fjiwrjovcxlvlvjsl fjls djfsld fjs fljdsfl sj flsj<b>testsfsdf sfjsldf</b> cvlxvljflsjdoie rfdljf sdlf";

                foreach (WikipediaArticle article in wiki)
                {
                    //                    DateTime tm;  
                  //  long dat = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000);
                    String dat = DateTime.Now.AddSeconds(i++).ToString();
                    results.Add(new EventTable("1adcdsf", "http://192.168.1.101/rtsms_pic/logo3.jpg", Double.Parse(article.Longitude.ToString()), Double.Parse(article.Latitude.ToString()), article.Title, "Kill-Firing", screentype, article.Distance.ToString(), "AryNews", dat.ToString(), article.Summary+tmp, "http://www.geo.tv/archive/12334.htm", false, i, dat.ToString()));

                }
            }
            catch (Exception ex)
            {
                var dataFault = new DataFault();
                dataFault.Operation = "AddOrder";
                dataFault.Reason = "ErrError writing to the database";
                dataFault.Details = ex.Message;
                throw new FaultException<DataFault>(dataFault,
                  new FaultReason("Error writing to the database"));
            }
            return results;
            */
        }



        public string GetLocationName(double longitude, double lattitude)
        {
            string url = String.Format(@"http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=true", lattitude, longitude);
            ReverseCode rv = new ReverseCode(url);
            return rv.GetGeoCode().locationname;
            /*
            if (longitude == 67.1823502 && lattitude == 24.8822664)
                return "Quaidabad,Karachi,Pakistan";
            else return "Quaidabad,Karachi,Pakistan";
       */ }

        public ICoordinate GetCoordinate(string name)
        {
            string url = String.Format(@"http://maps.googleapis.com/maps/api/geocode/xml?address={0},+&sensor=true",name);
            GeoCode g = new GeoCode(url);
            return g.GetGeoCode();
        }

     
     }
}

