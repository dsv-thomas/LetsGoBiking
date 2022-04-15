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
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
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
