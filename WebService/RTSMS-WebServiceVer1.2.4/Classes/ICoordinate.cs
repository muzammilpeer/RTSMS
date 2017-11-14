using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTSMSWebService.Classes
{
    public class ICoordinate
    {
      public string Latitude { get; set; }
      public string Longitude { get; set; }
      public bool IsValue { get; set; }
    }
}