using ProxyCache.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace ProxyCache
{
    [ServiceContract]
    public interface IProxyGB
    {

        [OperationContract]
        [WebInvoke
            (
                Method = "GET",
                UriTemplate = "stations",
                RequestFormat = WebMessageFormat.Json,
                BodyStyle = WebMessageBodyStyle.Bare,
                ResponseFormat = WebMessageFormat.Json
            )
        ]
        List<Station> GetStations();

        [OperationContract]
        [WebInvoke
            (
                Method = "GET",
                UriTemplate = "station?StationId={StationId}",
                RequestFormat = WebMessageFormat.Json,
                BodyStyle = WebMessageBodyStyle.Bare,
                ResponseFormat = WebMessageFormat.Json
            )
        ]
        Station GetStationId(int StationId);

        [OperationContract]
        [WebInvoke(
        Method = "GET",
        UriTemplate = "convert?address={address}",
        RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json
        )]
        List<FeatureAddr> convert(String address);

    }
}
