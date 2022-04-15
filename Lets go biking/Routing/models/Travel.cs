using System.Runtime.Serialization;

namespace Routing.models
{
    [DataContract]
    public class Travel
    {
        public Travel(Position positionStart, Position positionEnd, Station stationStart, Station stationEnd, GeoJson travelChoice, GeoJson toStation, GeoJson toEndPoint)
        {
            PositionStart = positionStart;
            PositionEnd = positionEnd;
            this.stationStart = stationStart;
            this.stationEnd = stationEnd;
            this.travelChoice = travelChoice;
            this.toStation = toStation;
            this.toEndPoint = toEndPoint;
        }

        [DataMember]
        public Position PositionStart { get; set; }
        [DataMember]
        public Position PositionEnd { get; set; }
        [DataMember]
        public Station stationStart { get; set; }
        [DataMember]
        public Station stationEnd { get; set; }
        [DataMember]
        public GeoJson travelChoice { get; set; }

        [DataMember]
        public GeoJson toStation { get; set; }

        [DataMember]
        public GeoJson toEndPoint { get; set; }
    }
    [DataContract]
    public class GeoPosition
    {
        [DataMember]
        public double longitude { get; set; }
        [DataMember]
        public double latitude { get; set; }
    }
}
