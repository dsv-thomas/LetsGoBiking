using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Routing.models
{
    public class GeometryAddr
    {
        public List<double> coordinates { get; set; }
        public string type { get; set; }
    }

    public class PropertiesAddr
    {
        public string city { get; set; }
        public string citycode { get; set; }
        public string context { get; set; }
        public object housenumber { get; set; }
        public string id { get; set; }
        public double importance { get; set; }
        public string label { get; set; }
        public string name { get; set; }
        public string postcode { get; set; }
        public double score { get; set; }
        public object street { get; set; }
        public string type { get; set; }
        public double x { get; set; }
        public double y { get; set; }
    }

    public class ConvertResult
    {
        public GeometryAddr geometry { get; set; }
        public PropertiesAddr properties { get; set; }
        public string type { get; set; }
    }

    public class Address
    {
        public List<ConvertResult> convertResult { get; set; }
    }
}
