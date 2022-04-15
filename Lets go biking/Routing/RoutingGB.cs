using Routing.externalServices;
using Routing.models;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Globalization;
using System.ServiceModel.Web;

namespace Routing
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class RoutingGB : IRoutingGB
    {

        private static JCDecaux jcDecaux = new JCDecaux();
        private static Openrouteservice openrouteservice = new Openrouteservice();
        private static ApiAdresse apiAdresse = new ApiAdresse();
        private List<Station> stationsFresh = jcDecaux.GetStations().Result;

        public RoutingGB()
        {
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            WebOperationContext.Current.OutgoingRequest.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public Station GetClosestStation(Double longitude, Double latitude)
        {
            GeoCoordinate initialPosition = new GeoCoordinate(longitude, latitude);
            double distanceMin = Double.MaxValue;
            Station minRoot = stationsFresh[0];
            foreach (Station root in stationsFresh)
            {
                if(root.status == "OPEN" && root.totalStands.availabilities.bikes >= 1)
                {
                    GeoCoordinate geo = new GeoCoordinate(root.position.latitude, root.position.longitude);
                    double distance = geo.GetDistanceTo(initialPosition);
                    if (distance <= distanceMin)
                    {
                        distanceMin = distance;
                        minRoot = root;
                    }
                }
                
            }
            return minRoot;
        }

        public Travel travel(String startPoint, String endPoint)
        {
            string[] P1 = startPoint.Split(',');
            string[] P2 = endPoint.Split(',');
            Position position = new Position(Convert.ToDouble(P1[0], CultureInfo.InvariantCulture), Convert.ToDouble(P1[1], CultureInfo.InvariantCulture));
            Position position2 = new Position(Convert.ToDouble(P2[0], CultureInfo.InvariantCulture), Convert.ToDouble(P2[1], CultureInfo.InvariantCulture));
            Station startSation = GetClosestStation(position.longitude, position.latitude);
            Station endSation = GetClosestStation(position2.longitude, position2.latitude);

            GeoJson walkingTravel = getRoute(startPoint, endPoint, "foot-walking");
            GeoJson cyclingTravel = getRoute((startSation.position.longitude.ToString().Replace(",", ".") + "," + startSation.position.latitude.ToString().Replace(",", ".")), (endSation.position.longitude.ToString().Replace(",", ".") + "," + endSation.position.latitude.ToString().Replace(",", ".")), "cycling-regular");
            
            GeoJson toStation = getRoute(startPoint, (startSation.position.longitude.ToString().Replace(",", ".") + "," + startSation.position.latitude.ToString().Replace(",", ".")), "foot-walking");
            GeoJson toEnd = getRoute((endSation.position.longitude.ToString().Replace(",", ".") + "," + endSation.position.latitude.ToString().Replace(",", ".")), endPoint, "foot-walking");

            if (walkingTravel.features[0].properties.segments[0].duration < cyclingTravel.features[0].properties.segments[0].duration)
            {
                return new Travel(position, position2, null, null, walkingTravel, toStation, toEnd);
            }
            return new Travel(position, position2, startSation, endSation, cyclingTravel, toStation, toEnd);
        }




        public List<Station> getAllStation()
        {
            return stationsFresh;
        }

        public GeoJson getRoute(String llP1, String llP2, String profile)
        {
            string[] P1 = llP1.Split(',');
            string[] P2 = llP2.Split(',');
            Position position = new Position(Convert.ToDouble(P1[0], CultureInfo.InvariantCulture), Convert.ToDouble(P1[1], CultureInfo.InvariantCulture));
            Position position2 = new Position(Convert.ToDouble(P2[0], CultureInfo.InvariantCulture), Convert.ToDouble(P2[1], CultureInfo.InvariantCulture));
            return openrouteservice.GetRoute(position, position2, profile).Result;
        }



        public String test()
        {
            return "test";
        }

        public List<ConvertResult> convert(string address)
        {
            return apiAdresse.convertAddress(address).Result;
        }
    }
}
