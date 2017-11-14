using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using RTSMSWebService.Classes;

namespace RTSMSWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.

    [ServiceContract(Namespace = "RTSMS", Name = "RTSMSWebService")]
    public interface IWebService
    {
        // every client must inform the web service that it comes from which type(iphone,android,bb,blackberry)

        [OperationContract]
        [WebGet(UriTemplate = "GetHomeStatus/?latt={latt}&lngt={lngt}", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        Status GetHomePageStatus(Double latt, Double lngt);

        [OperationContract()]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        [FaultContract(typeof(ConnectionFault))]
        [FaultContract(typeof(DataFault))]
        [WebGet(UriTemplate = "GetNearByLocal/?lat={lat}&lng={lng}&radius={radius}", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        List<GeoName> GetNearByLocal(Double lat, Double lng,Double radius);


        [OperationContract()]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        [FaultContract(typeof(ConnectionFault))]
        [FaultContract(typeof(DataFault))]
        [WebGet(UriTemplate = "GetEvent/?lat={latt}&lng={lngt}&radius={radius}&screentype={screentype}&clienttype={clienttype}", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        List<EventTable> GetEvent(Double latt, Double lngt, Decimal radius, string screentype, string clienttype);


        [OperationContract]
        [WebGet(UriTemplate = "GetLocationName/?longitude={longitude}&lattitude={lattitude}", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string GetLocationName(Double longitude, Double lattitude);

        [OperationContract]
        [WebGet(UriTemplate = "GetCoordinate/{name}", ResponseFormat = WebMessageFormat.Json)]
        ICoordinate GetCoordinate(string name);

    }
}
