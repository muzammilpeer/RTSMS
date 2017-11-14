using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace RTSMSWebService.Classes
{
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

}