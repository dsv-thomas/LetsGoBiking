using Routing.models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Routing
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IRoutingGB
    {


        [OperationContract]
        [WebInvoke
            (
                Method = "GET",
                UriTemplate = "stations",
                RequestFormat = WebMessageFormat.Json,
                BodyStyle = WebMessageBodyStyle.Wrapped,
                ResponseFormat = WebMessageFormat.Json
            )
        ]
        List<Station> getAllStation();

        [OperationContract]
        [WebInvoke(
        Method = "GET",
        UriTemplate = "route?llP1={llP1}&llP2={llP2}&profile={profile}",
        RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json
    )]
        GeoJson getRoute(String llP1, String llP2, String profile);

        [OperationContract]
        [WebInvoke(
        Method = "GET",
        UriTemplate = "convert?address={address}",
        RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json
        )]
        List<ConvertResult> convert(String address);

        [OperationContract]
        [WebInvoke(
        Method = "GET",
        UriTemplate = "travel?startpoint={startpoint}&endpoint={endpoint}",
        RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json
        )]
        Travel travel(String startpoint, String endpoint);


        [OperationContract]
        [WebInvoke(
        Method = "GET",
        UriTemplate = "test",
        RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json
        )]
        String test();



    }


    [DataContract]
    public class Station
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string address { get; set; }

        [DataMember]
        public int number { get; set; }

        [DataMember]
        public string contract_name { get; set; }

        [DataMember]
        public string status { get; set; }

        [DataMember]
        public Position position { get; set; }

        [DataMember]
        public TotalStands totalStands { get; set; }
    }
    [DataContract]
    public class Availabilities
    {
        [DataMember]
        public int bikes { get; set; }
        [DataMember]
        public int stands { get; set; }
        [DataMember]
        public int mechanicalBikes { get; set; }
        [DataMember]
        public int electricalBikes { get; set; }
        [DataMember]
        public int electricalInternalBatteryBikes { get; set; }
        [DataMember]
        public int electricalRemovableBatteryBikes { get; set; }
    }
    [DataContract]
    public class TotalStands
    {
        [DataMember]
        public Availabilities availabilities { get; set; }
        [DataMember]
        public int capacity { get; set; }
    }



    [DataContract]
    public class Position
    {
        [DataMember]
        public double latitude { get; set; }

        [DataMember]
        public double longitude { get; set; }

        public Position(Double p1, Double p2)
        {
            latitude = p1;
            longitude = p2;
        }
    }


}
