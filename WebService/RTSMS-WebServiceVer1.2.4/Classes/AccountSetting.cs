using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace RTSMSWebService.Classes
{
    // Use a data contract as illustrated in the sample below to add composite types to service operations
    [DataContract]
    public class AccountSetting
    {
        [DataMember]
        public bool Status { get; set; }
    }
}