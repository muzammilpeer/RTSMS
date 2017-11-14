using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace RTSMSWebService.Classes
{
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
}