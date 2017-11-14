using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTSMSWebService.Classes
{
    public class FindNearBy
    {
            // Free Service 
            // Radius 20 at Max
            // 100 Max rows
            // Premium Service
            // Radius 150 at Max
            // Unlimited rows.
            
        private Double lat = 0.0;
        private Double lng = 0.0;
        public FindNearBy(Double latt,Double lngt)
        {
            lat = latt;
            lng = lngt;
        }
        public void SetLatLng(Double latt,Double lngt)
        {
            lat = latt;
            lng = lngt;
        }
/*        public List<WikipediaArticle> GetNearByLocations(Double latt,Double lngt,Decimal radius)
        {
            //            foreach (WikipediaArticle article in a)
            //          List<WikipediaArticle> a = GeoNamesOrgWebservice.FindNearbyWikipedia(lat, lng, this.txtWikipediaLang.Text, this.numWikipediaRadius.Value, (int)this.numWikipediaRows.Value);
            SetLatLng(latt, lngt);
            List<WikipediaArticle> a = GeoNamesOrgWebservice.FindNearbyWikipedia(Decimal.Parse(lat.ToString()), Decimal.Parse(lng.ToString()), "", radius, 100);
            return a;
        }
        public List<WikipediaArticle> GetNearByLocations( Decimal radius)
        {
            List<WikipediaArticle> a = GeoNamesOrgWebservice.FindNearbyWikipedia(Decimal.Parse(lat.ToString()), Decimal.Parse(lng.ToString()), "", radius, 100);
            return a;
        }

*/
    }
}