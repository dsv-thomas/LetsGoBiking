using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Routing.models
{
    [DataContract]
    public class GeometryAddr
    {
        [DataMember]
        public List<double> coordinates { get; set; }
        [DataMember]
        public string type { get; set; }
    }
    [DataContract]
    public class PropertiesAddr
    {
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string citycode { get; set; }
        [DataMember]
        public string context { get; set; }
        [DataMember]
        public object housenumber { get; set; }
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public double importance { get; set; }
        [DataMember]
        public string label { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string postcode { get; set; }
        [DataMember]
        public double score { get; set; }
        [DataMember]
        public object street { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public double x { get; set; }
        [DataMember]
        public double y { get; set; }
    }


    [DataContract]
    public class Address
    {
        [DataMember]
        public List<ConvertResult> convertResult { get; set; }
    }
}
