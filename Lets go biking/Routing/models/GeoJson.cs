using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Routing.models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    [DataContract]
    public class Step
    {
        [DataMember(Order = 0)]
        public double distance { get; set; }
        [DataMember(Order = 1)]
        public double duration { get; set; }
        [DataMember(Order = 2)]
        public int type { get; set; }
        [DataMember(Order = 3)]
        public string instruction { get; set; }
        [DataMember(Order = 4)]
        public string name { get; set; }
        [DataMember(Order = 5)]
        public List<int> way_points { get; set; }
        [DataMember(Order = 6)]
        public int? exit_number { get; set; }
    }
    [DataContract]
    public class Segment
    {
        [DataMember(Order = 0)]
        public double distance { get; set; }
        [DataMember(Order = 1)]
        public double duration { get; set; }
        [DataMember(Order = 2)]
        public List<Step> steps { get; set; }
    }
    [DataContract]
    public class Summary
    {
        [DataMember(Order = 0)]
        public double value { get; set; }
        [DataMember(Order = 1)]
        public double distance { get; set; }
        [DataMember(Order = 2)]
        public double amount { get; set; }
        [DataMember(Order = 3)]
        public double duration { get; set; }
    }
    [DataContract]
    public class Roadaccessrestrictions
    {
        [DataMember(Order = 0)]
        public List<List<int>> values { get; set; }
        [DataMember(Order = 1)]
        public List<Summary> summary { get; set; }
    }
    [DataContract]
    public class Extras
    {
        [DataMember(Order = 0)]
        public Roadaccessrestrictions roadaccessrestrictions { get; set; }
    }
    [DataContract]
    public class Warning
    {
        [DataMember(Order = 0)]
        public int code { get; set; }
        [DataMember(Order = 1)]
        public string message { get; set; }
    }
    [DataContract]
    public class Properties
    {
        [DataMember(Order = 0)]
        public List<Segment> segments { get; set; }
        [DataMember(Order = 1)]
        public Extras extras { get; set; }
        [DataMember(Order = 2)]
        public List<Warning> warnings { get; set; }
        [DataMember(Order = 3)]
        public Summary summary { get; set; }
        [DataMember(Order = 4)]
        public List<int> way_points { get; set; }
    }
    [DataContract]
    public class Geometry
    {
        [DataMember(Order = 0)]
        public List<List<double>> coordinates { get; set; }
        [DataMember(Order = 1)]
        public string type { get; set; }
    }
    [DataContract]
    public class Feature
    {
        [DataMember(Order = 0)]
        public List<double> bbox { get; set; }
        [DataMember(Order = 1)]
        public string type { get; set; }
        [DataMember(Order = 2)]
        public Properties properties { get; set; }
        [DataMember(Order = 3)]
        public Geometry geometry { get; set; }
    }
    [DataContract]
    public class Query
    {
        [DataMember(Order = 0)]
        public List<List<double>> coordinates { get; set; }
        [DataMember(Order = 1)]
        public string profile { get; set; }
        [DataMember(Order = 2)]
        public string format { get; set; }
    }
    [DataContract]
    public class Engine
    {
        [DataMember(Order = 0)]
        public string version { get; set; }
        [DataMember(Order = 1)]
        public DateTime build_date { get; set; }
        [DataMember(Order = 2)]
        public DateTime graph_date { get; set; }
    }
    [DataContract]
    public class Metadata
    {
        [DataMember(Order = 0)]
        public string attribution { get; set; }
        [DataMember(Order = 1)]
        public string service { get; set; }
        [DataMember(Order = 2)]
        public long timestamp { get; set; }
        [DataMember(Order = 3)]
        public Query query { get; set; }
        [DataMember(Order = 4)]
        public Engine engine { get; set; }
    }

    [DataContract]
    public class GeoJson
    {
        [DataMember(Order = 0)]
        public string type { get; set; }
        [DataMember(Order = 1)]
        public List<Feature> features { get; set; }
        [DataMember(Order = 2)]
        public List<double> bbox { get; set; }
        [DataMember(Order = 3)]
        public Metadata metadata { get; set; }
    }

}
