using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace RTSMSWebService.Classes
{
    [DataContract]
    public class Status
    {
        // 1 == Safe Zone, 0 == Danger Zone
        [DataMember]
        public Boolean AlertType { get; set; }
        [DataMember]
        public Double Distance { get; set; }
        [DataMember]
        // 1 == Miles, 0 == KM
        public Boolean UnitTypeDistance { get; set; }
    }
}