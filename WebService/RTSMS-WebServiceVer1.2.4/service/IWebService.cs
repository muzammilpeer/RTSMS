using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using RTSMSWebService.Classes;

namespace RTSMSWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(Namespace = "TestCode", Name = "TimeTrakkerService")]
    public interface IWebService
    {
        [OperationContract]
        [WebGet(UriTemplate = "Home/{location}", ResponseFormat = WebMessageFormat.Json)]
        Status GetHomePageStatus(string location);
        
        [OperationContract]
        [FaultContract(typeof(ConnectionFault))]
        [FaultContract(typeof(DataFault))]
        [WebGet(UriTemplate = "Login/?usr={username}&pass={hashpassword}",BodyStyle=WebMessageBodyStyle.Wrapped,ResponseFormat = WebMessageFormat.Json)]
        bool Login(String username, String hashpassword);


        [OperationContract]
        [FaultContract(typeof(ConnectionFault))]
        [FaultContract(typeof(DataFault))]
        [WebGet(UriTemplate = "GetUserProfile/{username}", ResponseFormat = WebMessageFormat.Json)]
        User GetUserProfile(String username);

        [OperationContract]
        [FaultContract(typeof(ConnectionFault))]
        [FaultContract(typeof(DataFault))]
        
        [WebGet(UriTemplate = "Signup/?fname={firstname}&lname={lastname}&usr={username}&email={email}&pass={pass}&homelattitude={homelattitude}&homelongitude={homelongitude}&officelattitude={officelattitude}&officelongitude={officelongitude}",  ResponseFormat = WebMessageFormat.Json)]
        bool Signup(string firstname, string lastname, string username, string email, string pass, string homelattitude, string homelongitude, string officelattitude, string officelongitude);

        [OperationContract]
        [FaultContract(typeof(ConnectionFault))]
        [FaultContract(typeof(DataFault))]
        [WebGet(UriTemplate = "GetEventTableDetail/{EventTableid}", ResponseFormat = WebMessageFormat.Json,BodyStyle = WebMessageBodyStyle.Wrapped)]
        EventTableDetail GetEventTableDetail(string EventTableid);

        [OperationContract]
        [FaultContract(typeof(ConnectionFault))]
        [FaultContract(typeof(DataFault))]
        [WebGet(UriTemplate = "GetTodayEventTable/{currentlocation}", ResponseFormat = WebMessageFormat.Json,BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<EventTable> GetTodayUpdates(string currentlocation);

        
        [OperationContract]
        [FaultContract(typeof(ConnectionFault))]
        [FaultContract(typeof(DataFault))]
        [WebGet(UriTemplate = "GetHistory/{currentlocation}", ResponseFormat = WebMessageFormat.Json,BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<EventTable> GetHistory(string currentlocation);

        [OperationContract]
        [FaultContract(typeof(ConnectionFault))]
        [FaultContract(typeof(DataFault))]
        [WebGet(UriTemplate = "GetFutureEventTable/{currentlocation}", ResponseFormat = WebMessageFormat.Json,BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<EventTable> GetFutureEventTable(string currentlocation);

        [OperationContract]
        [FaultContract(typeof(ConnectionFault))]
        [FaultContract(typeof(DataFault))]
        [WebGet(UriTemplate = "GetCatastrophe/{currentlocation}", ResponseFormat = WebMessageFormat.Json ,BodyStyle=WebMessageBodyStyle.Wrapped)]
        List<EventTable> GetCatastrophe(string currentlocation);

        [OperationContract]
        [FaultContract(typeof(ConnectionFault))]
        [FaultContract(typeof(DataFault))]
        [WebGet(UriTemplate = "GetDontGo/{currentlocation}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<EventTable> GetDontGo(string currentlocation);

        [OperationContract]
        [FaultContract(typeof(ConnectionFault))]
        [FaultContract(typeof(DataFault))]
        [WebGet(UriTemplate = "GetMyNearBy/{currentlocation}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<EventTable> GetMyNearBy(string currentlocation);

        [OperationContract]
        [WebGet(UriTemplate = "GetLocationName/?longitude={longitude}&lattitude={lattitude}", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string GetLocationName(Double longitude, Double lattitude);

        [OperationContract]
        [WebGet(UriTemplate = "GetCoordinate/{name}", ResponseFormat = WebMessageFormat.Json)]
        ICoordinate GetCoordinate(string name);

        [OperationContract]
        [WebGet(UriTemplate = "GetUserList", ResponseFormat = WebMessageFormat.Json)]
        List<User> GetUserList();

        [OperationContract]
        [FaultContract(typeof(ConnectionFault))]
        [FaultContract(typeof(DataFault))]
        [WebGet(UriTemplate = "AccountSettingsSave/?username={username}&firstname={firstname}&lastname={lastname}&password={password}&homelocation={homelocation}&officelocation={officelocation}", ResponseFormat = WebMessageFormat.Json)]
        string AccountSettingsSave(string username,string firstname,string lastname,string password,string homelocation,string officelocation);

    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations
    [DataContract]
    public class User
    {
        public User()
        { }
        public User(string user, string pass)
        {
            UserName = user;
            Password = pass;
        }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string HomeLocation { get; set; }
        [DataMember]
        public string OfficeLocation { get; set; }
    }

    [DataContract]
    public class Status
    {
        [DataMember]
        public string HomeStatus { get; set; }
    }
    [DataContract]
    public class AccountSetting
    {
        [DataMember]
        public bool Status { get; set; }
    }

    [DataContract]
    public class EventTable
    {

        [DataMember]
        public Double Longitude { get; set; }

        [DataMember]
        public Double Lattitude { get; set; }

        [DataMember]
        public int EventID { get; set; }

        [DataMember]
        public string rssitemID { get; set; }
        
        [DataMember]
        public bool status { get; set; }
        
        [DataMember]
        public bool alert { get; set; }
        
        [DataMember]
        public DateTime publishdate { get; set; }
        
        [DataMember]
        public DateTime helddate { get; set; }
        
        [DataMember]
        public String t_title { get; set; }
      
        [DataMember]
        public String short_message { get; set; }

        public EventTable()
        { }
        public EventTable(int EventTableid, string rssitemid, bool Status,bool Alert,DateTime Pubdate,DateTime HeldDate,String Title,Double longi,Double latti)
        {
            this.EventID = EventTableid;
            this.rssitemID = rssitemid;
            this.status = Status;
            this.alert = Alert;
            this.Longitude = longi;
            this.Lattitude = latti;
            publishdate = Pubdate;
            helddate = HeldDate;
            t_title = Title;
        }
        public EventTable(int EventTableid, string rssitemid, bool Status, bool Alert, DateTime Pubdate, DateTime HeldDate, String Title, string shortmsg, Double longi, Double latti)
        {
            this.EventID = EventTableid;
            this.rssitemID = rssitemid;
            this.status = Status;
            this.alert = Alert;
            this.Longitude = longi;
            this.Lattitude = latti;
            publishdate = Pubdate;
            helddate = HeldDate;
            t_title = Title;
            short_message = shortmsg;
        }
        public void SetTable(int EventTableid, string rssitemid, bool Status, bool Alert, DateTime Pubdate, DateTime HeldDate, String Title)
        {
            this.EventID = EventTableid;
            this.rssitemID = rssitemid;
            this.status = Status;
            this.alert = Alert;
            publishdate = Pubdate;
            helddate = HeldDate;
            t_title = Title;
        }
    }

   
    [DataContract]
    public class EventTableDetail :EventTable
    {

        [DataMember]
        public String description { get; set; }
        
        [DataMember]
        public String link { get; set; }
        
        [DataMember]
        public int risklevel { get; set; }
        
        [DataMember]
        public String message { get; set; }

        public EventTableDetail(int EventTableid, String rssitemid, bool Status, bool Alert, DateTime Pubdate, DateTime HeldDate, String Title, String Desc, String Link, int RiskLevel, String Message)
        {
            SetTable(EventTableid, rssitemid, Status, Alert, Pubdate, HeldDate, Title);
            this.description = Desc;
            this.link = Link;
            this.risklevel = risklevel;
            this.message = Message;
        }
    }
    [DataContract]
    public class ConnectionFault
    {
        [DataMember]
        public string Operation;

        [DataMember]
        public string Reason;

        [DataMember]
        public string Details;
    }

    [DataContract]
    public class DataFault
    {
        [DataMember]
        public string Operation;

        [DataMember]
        public string Reason;

        [DataMember]
        public string Details;
    }
    [DataContract]
    public class LoginUser
    {
        [DataMember]
        public string url;

        [DataMember]
        public string image;
    }

}
