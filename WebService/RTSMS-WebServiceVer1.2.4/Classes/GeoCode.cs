using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Net;

namespace RTSMSWebService.Classes
{
    public class GeoCode
    {
        private XmlDocument XmlDoc = null;

        public GeoCode(string url)
        {
            XmlDoc = XMLDownloader(url);
        }
        public string URL { get; set; }

        public ICoordinate GetGeoCode()
        {
            ICoordinate tmp = new ICoordinate();
            tmp.IsValue = false;
            tmp.Latitude = null;
            tmp.Longitude = null;

            if (XmlDoc != null)
            {
                if (XMLStatusCheck(XmlDoc) == true)
                {
                    return GetLatLng(XmlDoc);
                }
                else
                {
                    return tmp;
                }
            }
            else
            {
                
                XmlDoc = XMLDownloader(URL);
                if (XMLStatusCheck(XmlDoc) == true)
                {
                    return GetLatLng(XmlDoc);
                }
                else
                {
                    return tmp;
                }
            }

        }
        private XmlDocument XMLDownloader(string url)
        {
/*            WebRequest web = HttpWebRequest.Create(url);
            WebResponse response = web.GetResponse();
            WebProxy myProxy = new WebProxy("10.11.0.23:8080",true);
            IWebProxy proxy = web.Proxy;
            web.Proxy = myProxy;
            reader = new XmlTextReader(response.GetResponseStream ());
          */
            XmlTextReader reader;
            XmlDocument xmlDoc;
            reader = new XmlTextReader(@url);
            xmlDoc = new XmlDocument();
            // Load the XML content into a XmlDocument
            xmlDoc.Load(reader);
            return xmlDoc;
        }
        private bool XMLStatusCheck(XmlDocument xmlDocument)
        {
            XmlNode header = null;
            // Loop for the <GeocodeResponse> tag
            for (int i = 0; i < xmlDocument.ChildNodes.Count; i++)
            {
                // If it is the rss tag
                if (xmlDocument.ChildNodes[i].Name == "GeocodeResponse")
                {
                    // <rss> tag found
                    header = xmlDocument.ChildNodes[i];
                }
            }
            if (header["status"].InnerText.ToString() == "OK" || header["status"].InnerText.ToString() == "ok")
                return true;
            else return false;
        }
        private ICoordinate GetLatLng(XmlDocument xmlDoc)
        {
            XmlNode nodeGeocodeResponse = null;
            XmlNode nodeResult = null;
            XmlNode nodeGeometry = null;
            XmlNode nodeLocationItem = null;
            ICoordinate res = new ICoordinate();

            // Loop for the <GeocodeResponse> tag
            for (int i = 0; i < xmlDoc.ChildNodes.Count; i++)
            {
                // If it is the rss tag
                if (xmlDoc.ChildNodes[i].Name == "GeocodeResponse")
                {
                    // <rss> tag found
                    nodeGeocodeResponse = xmlDoc.ChildNodes[i];
                }
            }

            // Loop for the <result> tag
            for (int i = 0; i < nodeGeocodeResponse.ChildNodes.Count; i++)
            {
                // If it is the channel tag
                if (nodeGeocodeResponse.ChildNodes[i].Name == "result")
                {
                    // <channel> tag found
                    nodeResult = nodeGeocodeResponse.ChildNodes[i];
                }
            }


            // Loop for the <geometry> tag
            for (int i = 0; i < nodeResult.ChildNodes.Count; i++)
            {
                // If it is the channel tag
                if (nodeResult.ChildNodes[i].Name == "geometry")
                {
                    // <channel> tag found
                    nodeGeometry = nodeResult.ChildNodes[i];
                }
            }


            ///<location>finding
            for (int i = 0; i < nodeGeometry.ChildNodes.Count; i++)
            {
                // If it is the item tag, then it has children tags which we will add as items to the ListView
                if (nodeGeometry.ChildNodes[i].Name == "location")
                {
                    nodeLocationItem = nodeGeometry.ChildNodes[i];
                    res.Latitude = nodeLocationItem["lat"].InnerText;
                    res.Longitude = nodeLocationItem["lng"].InnerText;
                }
            }

            res.IsValue = true;
            return res;
        }

    }
}