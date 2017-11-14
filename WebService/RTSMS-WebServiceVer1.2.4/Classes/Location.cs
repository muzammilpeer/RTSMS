using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTSMSWebService.Classes
{
    public class Location
    {
        public String LocationName = String.Empty;
        public Double Lattitude = 0.0;
        public Double Longitude = 0.0;
        public Double Height = 0.0;
        public String Description = String.Empty;
        public DateTime DateAdded = DateTime.Now;
    }
}