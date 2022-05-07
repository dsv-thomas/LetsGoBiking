using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProxyCache.Items
{
    internal class ApiAdresseItem
    {
        static readonly HttpClient client = new HttpClient();

        [DataMember]
        public List<FeatureAddr> addressList { get; set; }

        public ApiAdresseItem()
        {
        }

        public ApiAdresseItem(Dictionary<String, String> dict)
        {
            addressList = ConvertAddress(dict["address"]).Result;
        }


        public async Task<List<FeatureAddr>> ConvertAddress(String address)
        {
            Console.WriteLine("Call external API convert Address : " + address);
            HttpResponseMessage response = await client.GetAsync("https://api-adresse.data.gouv.fr/search/?q=" + address);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            return JsonConvert.DeserializeObject<Address>(responseBody, settings).features;
        }
    }

    public class GeometryAddr
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class PropertiesAddr
    {
        public string label { get; set; }
        public double score { get; set; }
        public string housenumber { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string postcode { get; set; }
        public string citycode { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public string city { get; set; }
        public string context { get; set; }
        public string type { get; set; }
        public double importance { get; set; }
        public string street { get; set; }
    }

    public class FeatureAddr
    {
        public string type { get; set; }
        public GeometryAddr geometry { get; set; }
        public PropertiesAddr properties { get; set; }
    }

    public class Address
    {
        public string type { get; set; }
        public string version { get; set; }
        public List<FeatureAddr> features { get; set; }
        public string attribution { get; set; }
        public string licence { get; set; }
        public string query { get; set; }
        public int limit { get; set; }
    }
}
