using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace RTSMSWebService.Classes
{
    [DataContract]
    public class EventTable
    {
        [DataMember]
        public string rssitemID { get; set; }

        [DataMember]
        public String EventIconUrl { get; set; }

        [DataMember]
        public Double Longitude { get; set; }

        [DataMember]
        public Double Lattitude { get; set; }

        [DataMember]
        public String LocationName { get; set; }

        [DataMember]
        public String EventName { get; set; }
 
        [DataMember]
        public String ScreenType { get; set; }

        [DataMember]
        public string Distance { get; set; }

        [DataMember]
        public string ReportedBy { get; set; }

        [DataMember]
        public String ReleaseDate { get; set; }

        [DataMember]
        public String Description { get; set; }

        // if the RSSfeeditem has the weburl else if tweet has the weburl for more detail
        [DataMember]
        public String WebUrl { get; set; }

        [DataMember]
        public bool IsAlert { get; set; }
       
        [DataMember]
        public Int32 AlertLevel { get; set; }
       
        // if future event then the held date
        [DataMember]
        public String HeldDate { get; set; }
        
        // Constructor with Parameters
        public EventTable(string rssitemid,string iconurl,Double longi,Double latti, string locname,string evntname,string screentype,string distance,string reportedby,string releasedate,string description,string  weburl,Boolean isalert,Int32 alertlevel, string helddate)
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
        }
    }
}