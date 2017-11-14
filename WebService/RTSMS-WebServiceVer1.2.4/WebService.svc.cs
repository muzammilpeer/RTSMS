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
using System.Data;
namespace RTSMSWebService
{
    // (TransactionIsolationLevel = System.Transactions.IsolationLevel.Serializable, TransactionTimeout = "00:00:30", TransactionScopeOption.Required)
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    [ServiceBehavior(
        TransactionIsolationLevel = System.Transactions.IsolationLevel.Serializable,
        TransactionTimeout = "00:02:45")]
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

        [OperationBehavior(TransactionScopeRequired = true)]
        public List<GeoName> GetNearByLocal(Double lat, Double lng,Double radius)
        {
            List<GeoName> results = new List<GeoName>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = Properties.Settings.Default.GeoNameConnectionString2;
                // 1.  create a command object identifying
                //     the stored procedure
                SqlDataReader rdr = null;
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                }
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "GetNearByCall", conn);

                    // 2. set the command object so it knows
                    //    to execute a stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // 3. add parameter to command, which
                    //    will be passed to the stored procedure
                    cmd.Parameters.Add(
                        new SqlParameter("@latt", lat));

                    cmd.Parameters.Add(
                        new SqlParameter("@lngt", lng));

                    cmd.Parameters.Add(
                        new SqlParameter("@radius", radius));

                    // execute the command
                    rdr = cmd.ExecuteReader();

                    // iterate through results, printing each to console
                    while (rdr.Read())
                    {
                        results.Add(new GeoName(rdr["geonameid"].ToString(),
                                                rdr["name"].ToString(),
                                                rdr["asciiname"].ToString(),
                                                rdr["alternatenames"].ToString(),
                                                rdr["latitude"].ToString(),
                                                rdr["longitude"].ToString(),
                                                rdr["feature_class"].ToString(),
                                                rdr["feature_code"].ToString(),
                                                rdr["country_code"].ToString(),
                                                rdr["cc2"].ToString(),
                                                rdr["admin1_code"].ToString(),
                                                rdr["admin2_code"].ToString(),
                                                rdr["admin3_code"].ToString(),
                                                rdr["admin4_code"].ToString(),
                                                rdr["population"].ToString(),
                                                rdr["elevation"].ToString(),
                                                rdr["gtopo30"].ToString(),
                                                rdr["timezone"].ToString(),
                                                rdr["modification_date"].ToString(),
                                                rdr["distance"].ToString()));
                    }
                }
                catch (Exception ex)
                {
                }
                return results;
            }
        }


        // Main Page Status Endpoint function 
        public Status GetHomePageStatus(Double latt, Double lngt)
        {
            Status tmp = new Status();
            tmp.AlertType = true;  // 1 == Safe Zone, 0 == Danger Zone
            tmp.Distance = 45.12;
            tmp.UnitTypeDistance = true;  // 1 == Miles, 0 == KM
            return tmp;
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public List<EventTable> GetEvent(Double latt, Double lngt, Decimal radius, string screentype, string clienttype)
        {
            FetchEvent fe = new FetchEvent();
            return fe.GetEventFromDatabase(screentype);

                int i = 0;

                Double latt1 = 24.8601083;
                Double lngi1 = 67.2162619;
                Double latt2 = 24.8933790;
                Double lngi2 = 67.0280610;

                double e = (3.1415926538 * latt1 / 180);
                double f = (3.1415926538 * lngi1 / 180);
                double g = (3.1415926538 * latt2 / 180);
                double h = (3.1415926538 * lngi2 / 180);
                double P = (Math.Cos(e) * Math.Cos(g) * Math.Cos(f) * Math.Cos(h) + Math.Cos(e) * Math.Sin(f) * Math.Cos(g) * Math.Sin(h) + Math.Sin(e) * Math.Sin(g));
                double j = (Math.Acos(P));
                double k = (6371 * j);

                /*foreach (WikipediaArticle article in wiki)
                {
                    //                    DateTime tm;  
                  //  long dat = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000);
                    String dat = DateTime.Now.AddSeconds(i++).ToString();
                  */

        }

/*        SELECT [demoEventID] ,[RSSItemID] ,[CategoryID] ,[Title] ,[demoEvent].[Description],[Link],[RiskLevel],[Status],[Alert],[Message],[pubDate],[heldDate],[Location].LocationName  FROM [demoEvent] inner join [Location] on ([demoEvent].LocationID = [Location] .LocationID  and [demoEvent].[iscatastrophe] = 1 )    
GO*/
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

