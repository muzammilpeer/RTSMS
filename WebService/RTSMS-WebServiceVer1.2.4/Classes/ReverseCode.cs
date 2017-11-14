using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace RTSMSWebService.Classes
{
    public class ReverseCode
    {

        private XmlDocument XmlDoc = null;

        public ReverseCode(string url)
        {
            XmlDoc = XMLDownloader(url);
        }

        public string URL { get; set; }

        public ReverseCodeCoordinate GetGeoCode()
        {
            ReverseCodeCoordinate tmp = new ReverseCodeCoordinate();
            tmp.locationname = String.Empty;

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

        private ReverseCodeCoordinate GetLatLng(XmlDocument xmlDoc)
        {
            XmlNode nodeGeocodeResponse = null;
            XmlNode nodeResult = null;
            XmlNode nodeGeometry = null;
            XmlNode nodeLocationItem = null;
            ReverseCodeCoordinate res = new ReverseCodeCoordinate();

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

                // If it is the channel tag

                if (nodeGeocodeResponse.ChildNodes[2].Name == "result")
                {
                    // <channel> tag found
                    nodeResult = nodeGeocodeResponse.ChildNodes[2];
                }


            // Loop for the <geometry> tag
            for (int i = 0; i < nodeResult.ChildNodes.Count; i++)
            {
                // If it is the channel tag
                if (nodeResult.ChildNodes[i].Name == "formatted_address")
                {
                    // <channel> tag found
                    res.locationname = nodeResult.ChildNodes[i].InnerText;
                }
            }


            /////<location>finding
            //for (int i = 0; i < nodeGeometry.ChildNodes.Count; i++)
            //{
            //    // If it is the item tag, then it has children tags which we will add as items to the ListView
            //    if (nodeGeometry.ChildNodes[i].Name == "location")
            //    {
            //        nodeLocationItem = nodeGeometry.ChildNodes[i];
            //        res.Latitude = nodeLocationItem["lat"].InnerText;
            //        res.Longitude = nodeLocationItem["lng"].InnerText;
            //    }
            //}

            //res.IsValue = true;
            //return res;
            return res;
        }
    }
}