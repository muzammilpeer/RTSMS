using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace RTSMSWebService.Classes
{

    [DataContract]
    public class GeoName
    {
            [DataMember]
            public String geonameid;
            [DataMember]
            public String name;
            [DataMember]
            public String asciiname;
            [DataMember]
            public String alternatenames;
            [DataMember]
            public String latitude;
            [DataMember]
            public String longitude;
            [DataMember]
            public String feature_class;
            [DataMember]
            public String feature_code;
            [DataMember]
            public String country_code;
            [DataMember]
            public String cc2;
            [DataMember]
            public String admin1_code;
            [DataMember]
            public String admin2_code;
            [DataMember]
            public String admin3_code;
            [DataMember]
            public String admin4_code;
            [DataMember]
            public String population;
            [DataMember]
            public String elevation;
            [DataMember]
            public String gtopo30;
            [DataMember]
            public String timezone;
            [DataMember]
            public String modification_date;
            [DataMember]
            public String distance;

            public GeoName(String GeoNameID, String Name, String Asciiname, String Alternatename, String Lattitude, String Longitude, String Feature_class,
                   String Feature_code, String Country_code, String Cc2, String Admin1_code, String Admin2_code, String Admin3_code, String Admin4_code,
                   String Population, String Elevation, String Gtopo30, String Timezone, String Modification_date, String Distance)
            {
                this.admin1_code = Admin1_code;
                this.admin2_code = Admin2_code;
                this.admin3_code = Admin3_code;
                this.admin4_code = Admin4_code;
                this.alternatenames = Alternatename;
                this.asciiname = Asciiname;
                this.cc2 = Cc2;
                this.country_code = Country_code;
                this.distance = Distance;
                this.elevation = Elevation;
                this.feature_class = Feature_class;
                this.feature_code = Feature_code;
                this.geonameid = GeoNameID;
                this.gtopo30 = Gtopo30;
                this.latitude = Lattitude;
                this.longitude = Longitude;
                this.modification_date = Modification_date;
                this.name = Name;
                this.population = Population;
                this.timezone = Timezone;
                

            }
    }
}