using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace RTSMSWebService.Classes
{
    [DataContract]
    public class LoginUser
    {
        [DataMember]
        public string url;

        [DataMember]
        public string image;
    }
}