using ProxyCache.Cache;
using ProxyCache.Items;
using System;
using System.Collections.Generic;
using System.ServiceModel.Web;

namespace ProxyCache
{

    public class ProxyGB : IProxyGB
    {

        private readonly static Cache<JCDecauxItem> cacheJCDecaux = new Cache<JCDecauxItem>();
        private readonly static Cache<ApiAdresseItem> cacheApiAdresse = new Cache<ApiAdresseItem>();
        private readonly double EXPIRATION_TIME = 60;
        private readonly double EXPIRATION_TIME_LONG = 3600;

        public ProxyGB()
        {
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            WebOperationContext.Current.OutgoingRequest.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public List<Station> GetStations()
        {
            return cacheJCDecaux.Get("/stations", EXPIRATION_TIME_LONG).stations;
        }

        public Station GetStationId(int StationId)
        {
            Console.WriteLine("Proxy get station id : " + StationId);
            Dictionary<String, String> map = new Dictionary<String, String>();
            map.Add("stationId", StationId.ToString());
            return cacheJCDecaux.Get("/stations/" + StationId.ToString() + "?contract=lyon", EXPIRATION_TIME, map).station;
        }

        public List<FeatureAddr> convert(String address)
        {
            Console.WriteLine("Proxy convert : " + address);
            Dictionary<String, String> map = new Dictionary<String, String>();
            map.Add("address", address);
            return cacheApiAdresse.Get("/convert?address=" + address, EXPIRATION_TIME_LONG, map).addressList;
        }
    }
}
