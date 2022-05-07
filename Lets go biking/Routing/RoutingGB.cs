using Routing.externalServices;
using Routing.models;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Globalization;
using System.Linq;
using System.ServiceModel.Web;

namespace Routing
{
    public class RoutingGB : IRoutingGB
    {
        private static JCDecaux jcDecaux = new JCDecaux();
        private static Openrouteservice openrouteservice = new Openrouteservice();
        private static ApiAdresse apiAdresse = new ApiAdresse();
        private List<Station> stationsLongCache = jcDecaux.GetStations().Result;
        public static int CountTotalRoute = 0;
        public static Dictionary<string, long> StationsTopList = new Dictionary<string, long>();
        public static double MoyResponseTime = 0;

        public RoutingGB()
        {
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            WebOperationContext.Current.OutgoingRequest.Headers.Add("Access-Control-Allow-Origin", "*");
        }



        public Station GetClosestStation(Double longitude, Double latitude, bool isArrival)
        {
            GeoCoordinate initialPosition = new GeoCoordinate(longitude, latitude);
            Station minRoot = stationsLongCache[0];
            foreach (Station root in stationsLongCache.OrderBy(test => new GeoCoordinate(test.position.latitude, test.position.longitude).GetDistanceTo(initialPosition)))
            {
                if (isValidStation(root.number, isArrival))
                {
                    StationsTopList.TryGetValue(root.name, out var currentCount); ;
                    StationsTopList[root.name] = currentCount + 1;
                    return root;
                }

            }
            StationsTopList.TryGetValue(minRoot.name, out var currentCount2); ;
            StationsTopList[minRoot.name] = currentCount2 + 1;
            return minRoot;
        }

        public Boolean isValidStation(int stationId, bool isArrival)
        {
            Station station = jcDecaux.GetStation(stationId).Result;
            return station.status == "OPEN" && (!isArrival && station.totalStands.availabilities.bikes >= 1) || (isArrival && station.totalStands.availabilities.stands >= 1);
        }

        public Travel travel(String startPoint, String endPoint)
        {
            Console.WriteLine("Search for a travel with start position : " + startPoint + " end position : " + endPoint);
            string[] P1 = startPoint.Split(',');
            string[] P2 = endPoint.Split(',');
            Position position = new Position(Convert.ToDouble(P1[0], CultureInfo.InvariantCulture), Convert.ToDouble(P1[1], CultureInfo.InvariantCulture));
            Position position2 = new Position(Convert.ToDouble(P2[0], CultureInfo.InvariantCulture), Convert.ToDouble(P2[1], CultureInfo.InvariantCulture));
            Station startSation = GetClosestStation(position.longitude, position.latitude, false);
            Station endSation = GetClosestStation(position2.longitude, position2.latitude, true);

            GeoJson walkingTravel = getRoute(startPoint, endPoint, "foot-walking");
            GeoJson cyclingTravel = getRoute((startSation.position.longitude.ToString().Replace(",", ".") + "," + startSation.position.latitude.ToString().Replace(",", ".")), (endSation.position.longitude.ToString().Replace(",", ".") + "," + endSation.position.latitude.ToString().Replace(",", ".")), "cycling-regular");

            GeoJson toStation = getRoute(startPoint, (startSation.position.longitude.ToString().Replace(",", ".") + "," + startSation.position.latitude.ToString().Replace(",", ".")), "foot-walking");
            GeoJson toEnd = getRoute((endSation.position.longitude.ToString().Replace(",", ".") + "," + endSation.position.latitude.ToString().Replace(",", ".")), endPoint, "foot-walking");

            if (walkingTravel.features[0].properties.segments[0].duration < cyclingTravel.features[0].properties.segments[0].duration + toStation.features[0].properties.segments[0].duration + toEnd.features[0].properties.segments[0].duration)
            {
                return new Travel(position, position2, null, null, walkingTravel, toStation, toEnd);
            }
            return new Travel(position, position2, startSation, endSation, cyclingTravel, toStation, toEnd);
        }




        public List<Station> getAllStation()
        {
            return stationsLongCache;
        }

        public GeoJson getRoute(String llP1, String llP2, String profile)
        {
            Console.WriteLine("Search for a route with start position : "+llP1 + " end position : " + llP2);
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            CountTotalRoute++;
            string[] P1 = llP1.Split(',');
            string[] P2 = llP2.Split(',');
            Position position = new Position(Convert.ToDouble(P1[0], CultureInfo.InvariantCulture), Convert.ToDouble(P1[1], CultureInfo.InvariantCulture));
            Position position2 = new Position(Convert.ToDouble(P2[0], CultureInfo.InvariantCulture), Convert.ToDouble(P2[1], CultureInfo.InvariantCulture));
            GeoJson geoJson = openrouteservice.GetRoute(position, position2, profile).Result;
            watch.Stop();
            MoyResponseTime = (MoyResponseTime + watch.ElapsedMilliseconds) / 2;
            return geoJson;
        }


        public Dictionary<String, String> getStatistics()
        {
            Console.WriteLine("Obtaining statistics");
            Dictionary<String, String> dict = new Dictionary<String, String>();
            dict.Add("TotalRoute", CountTotalRoute.ToString());
            dict.Add("TopStation", StationsTopList.FirstOrDefault(x => x.Value == StationsTopList.Values.Max()).Key);
            dict.Add("MoyResponseTime", MoyResponseTime.ToString());
            return dict;
        }

        public List<ConvertResult> convert(string address)
        {
            Console.WriteLine("Convert address : " + address);
            return apiAdresse.convertAddress(address).Result;
        }

    }
}
