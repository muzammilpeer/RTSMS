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
namespace RTSMSWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    [ServiceBehavior(TransactionIsolationLevel = System.Transactions.IsolationLevel.Serializable, TransactionTimeout = "00:00:30")]
    public class WebService : IWebService
    {
        public List<User> listuser = new List<User>();
        public string RTSMSdbConnectionString = Properties.Settings.Default.RTSMSdbConnectionString;

        public Status GetHomePageStatus(string location)
        {
            Status tmp = new Status();
            tmp.HomeStatus = location;
            return tmp;
        }

        //[OperationBehavior(TransactionAutoComplete = true, TransactionScopeRequired = true)]
        public bool Login(string username, string hashpassword)
        {
            User usr = new User();
            usr.UserName = username;
            usr.Password = hashpassword;
            //return ValidateUser(usr);
            return true;
        }

        public bool Signup(string firstname, string lastname, string username, string email, string pass, string homelattitude, string homelongitude, string officelattitude, string officelongitude)
        {
            return CreateAccount(firstname, lastname, username, email, pass, homelattitude, homelongitude, officelattitude, officelongitude);
        }
        public EventTableDetail GetEventTableDetail(string EventTableid)
        {
            Int32 eventid = Int32.Parse(EventTableid);
            DateTime dt = DateTime.Now;
            return new EventTableDetail(eventid, "sdfsdf", true, false, dt, dt, "HelloWorld", "New Statement", "http://localhost", 1, "Alert Message");

        }

        public List<EventTable> GetTodayUpdates(string currentlocation)
        {
            List<EventTable> results = new List<EventTable>();
            String query = "select * from [Event],[Location] where (Event.ispresent=1 and Event.LocationID = (select Location.LocationID from [Location] where (Location.LocationName='" + currentlocation + "')))and Location.LocationID=Event.LocationID; ";
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
                    if (reader.HasRows == true)
                    { 
  
                    DateTime tm = DateTime.Now;

                    while (reader.Read())
                        {
                          // bool status = reader.GetBoolean(8);
                           //string alert = reader.GetString(8);
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
            }

            return results;
        }


        public List<EventTable> GetHistory(string currentlocation)
        {
            //currentlocation = "karachi";
            List<EventTable> results = new List<EventTable>();
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
                    if (reader.HasRows == true)
                    {

                        DateTime tm = DateTime.Now;

                        while (reader.Read())
                        {
                            // bool status = reader.GetBoolean(8);
                            //string alert = reader.GetString(8);
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
            }

            return results;
        }

        public List<EventTable> GetFutureEventTable(string currentlocation)
        {
//            currentlocation = "Karachi";
            List<EventTable> results = new List<EventTable>();
            String query = "select * from [Event],[Location] where (Event.isfuture=1 and Event.LocationID = (select Location.LocationID from [Location] where (Location.LocationName='" + currentlocation + "')))and Location.LocationID=Event.LocationID;";
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
                    if (reader.HasRows == true)
                    {

                        DateTime tm = DateTime.Now;

                        while (reader.Read())
                        {
                            // bool status = reader.GetBoolean(8);
                            //string alert = reader.GetString(8);
                            results.Add(new EventTable(reader.GetInt32(0), reader.GetString(1), true, false, tm, tm, reader.GetString(4), reader.GetString(5), reader.GetDouble(18), reader.GetDouble(19)));
                        }
                    }
                    else
                    {
                        DateTime tm = DateTime.Now;
                        results.Add(new EventTable(1, " ", true, false, tm, tm, "Tmp", "No Result", 0.0, 0.0));
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
            }

            return results;
        }
        public List<EventTable> GetCatastrophe(string currentlocation)
        {
            List<EventTable> results = new List<EventTable>();
            String query = @"select * from [Event],[Location] where ((Event.ispresent=1) or (Event.ishistory=1) and (Event.pubDate  <= GETDATE() and Event.pubDate >= '" + DateTime.Now.AddDays(-1).ToString() + "') and Event.LocationID = (select Location.LocationID from [Location] where (Location.LocationName='" + currentlocation + "')))and Location.LocationID=Event.LocationID;";
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
                    if (reader.HasRows == true)
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
            }

            return results;
        }

        public List<EventTable> GetDontGo(string currentlocation)
        {
            DateTime tm = DateTime.Now;
            List<EventTable> results = new List<EventTable>();
            results.Add(new EventTable(1, "123", true, false, tm, tm, "Sadar,Karachi", "10 people killed in Sadar.", 67.0242519, 24.8517936));
            results.Add(new EventTable(2, "456", true, false, tm, tm, "Accident !.KalaBoard,Karachi", "Bus Accident at KalaBoard.", 67.1823501, 24.8822664));
            results.Add(new EventTable(3, "4567", true, false, tm, tm, "Malir 15", "Traffic jam from last two hours.", 67.0280610, 24.8933790));
            return results;
        }

        public List<EventTable> GetMyNearBy(string currentlocation)
        {
            DateTime tm = DateTime.Now;
            List<EventTable> results = new List<EventTable>();
            results.Add(new EventTable(1, "123", true, false, tm, tm, "Quaidabad", "3 people killed in Quaidabad.", 67.2162619, 24.8601083));
            results.Add(new EventTable(2, "456", true, false, tm, tm, "Malir 15", "1.1 RactorScale Reading at Malir and Landhi", 67.0280610, 24.8933790));
            results.Add(new EventTable(3, "4567", true, false, tm, tm, "Landhi", "Dawood Chorangi road is blocked from last two hours.", 67.2093479, 24.8524531));
            return results;
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

        [OperationBehavior(TransactionAutoComplete = true, TransactionScopeRequired = true)]
        public string AccountSettingsSave(string username, string firstname, string lastname, string password, string homelocation, string officelocation)
        {
            User usr = new User();
            usr.UserName = username;
            usr.FirstName = firstname;
            usr.LastName = lastname;
            usr.Password = password;
            usr.HomeLocation = homelocation;
            usr.OfficeLocation = officelocation;
            //listuser.Add(usr);
        //    string query = "hello world";
            return UpdateAccount(usr);
         }
        public string UpdateAccount(User usr)
        {
            string query;
            query = @"update [User] set [HomeLocationID]=" + usr.HomeLocation + ", [OfficeLocationID]=" + usr.OfficeLocation + ", [FirstName]='" + usr.FirstName + "', [LastName]='" + usr.LastName + "' where [UserName]='" + usr.UserName + "';";
            string connectionString = RTSMSdbConnectionString;

            using (SqlConnection cnn = new SqlConnection())
            {
                cnn.ConnectionString = connectionString;
                SqlCommand cmd = new SqlCommand(query, cnn);
                try
                {
                    cnn.Open();
                }
                catch (Exception ex)
                {
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
                   cmd.ExecuteNonQuery();

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
            return query;
        }



        public List<User> GetUserList()
        {
            return listuser;
        }

        public bool ValidateUser(User usr)
        {
            string query;
            bool val = false;
            query = @"SELECT COUNT(*) FROM [User] AS u where u.UserName = '"+usr.UserName+"' AND u.hashPassword = '"+usr.Password.ToString()+"';";
            string connectionString = RTSMSdbConnectionString;

            using (SqlConnection cnn = new SqlConnection())
            {
                cnn.ConnectionString = connectionString;
                SqlCommand cmd = new SqlCommand(query, cnn);
                try
                {
                    cnn.Open();
                }
                catch (Exception ex)
                {
                    var connectionFault = new ConnectionFault();
                    connectionFault.Operation = "ListCustomers";
                    connectionFault.Reason =
                    "Can't connect to the database";
                    connectionFault.Details = ex.Message;
                    throw new FaultException<ConnectionFault>(
                    connectionFault);
                }
                try
                {
                   
                    SqlDataReader readerS = cmd.ExecuteReader();
                    int check=5;
                    
                    while (readerS.Read())
                    {
                       check = Int32.Parse(readerS[0].ToString());
                    }
                    if (check == 0)
                        val = false; 
                    else 
                    val = true;
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
            return true;
//            return val;
//            return connectionString;
        }



        public bool CreateAccount(string firstname,string lastname,string username,string email,string password,string homelattitude,string homelongitude,string officelattitude,string officelongitude)
        {
            bool check;
            string query;
            query = @"insert into [User] values (" + 1 + "," + 1 + "," + 1 + ",'" + firstname+ "','" + lastname + "','" + username + "','" + password + "','" + DateTime.Now.ToString() + "')";
            string connectionString = RTSMSdbConnectionString;

            using (SqlConnection cnn = new SqlConnection())
            {
                cnn.ConnectionString = connectionString;
                SqlCommand cmd = new SqlCommand(query, cnn);
                try
                {
                    cnn.Open();
                }
                catch (Exception ex)
                {
                    var connectionFault = new ConnectionFault();
                    connectionFault.Operation = "ListCustomers";
                    connectionFault.Reason =
                    "Can't connect to the database";
                    connectionFault.Details = ex.Message;
                    throw new FaultException<ConnectionFault>(
                    connectionFault);
                }
                try
                {
                  int i = cmd.ExecuteNonQuery();
                  if (i < 0)
                  {
                      check = false;
                      return false;
                  }
                  else
                      check = true;
                    
                    
                }
                catch (Exception ex)
                {
                    check = false;
                    var dataFault = new DataFault();
                    dataFault.Operation = "AddOrder";
                    dataFault.Reason = "ErrError writing to the database";
                    dataFault.Details = ex.Message;
                    return false;
                    throw new FaultException<DataFault>(dataFault,
                      new FaultReason("Error writing to the database"));

                }

            }
            return check;
        }


        public User GetUserProfile(string username)
        {
            string query;
            User usr = new User();
            query = @"select * from [User] AS u where u.UserName='"+username+"'";
            string connectionString = RTSMSdbConnectionString;

            using (SqlConnection cnn = new SqlConnection())
            {
                cnn.ConnectionString = connectionString;
                SqlCommand cmd = new SqlCommand(query, cnn);
                try
                {
                    cnn.Open();
                }
                catch (Exception ex)
                {
                    var connectionFault = new ConnectionFault();
                    connectionFault.Operation = "ListCustomers";
                    connectionFault.Reason =
                    "Can't connect to the database";
                    connectionFault.Details = ex.Message;
                    throw new FaultException<ConnectionFault>(
                    connectionFault);
                }
                try
                {

                    SqlDataReader readerS = cmd.ExecuteReader();
                    int check = 5;
                    while (readerS.Read())
                    {
                        usr.HomeLocation = readerS[1].ToString();
                        usr.OfficeLocation = readerS[2].ToString();
                        usr.FirstName = readerS[4].ToString();
                        usr.LastName = readerS[5].ToString();
                        usr.UserName = readerS[6].ToString();
                        usr.Password = readerS[7].ToString();
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
            return usr;
        }
    }
}

