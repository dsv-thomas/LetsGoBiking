using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProxyCache
{
    internal class JCDecauxItem
    {
        string apiKey = "079e5c452a49c4c91275b1be1e96300f1194190a";
        static readonly HttpClient client = new HttpClient();


        [DataMember]
        public List<Station> stations { get; set; }

        public JCDecauxItem()
        {
            stations = GetStations().Result;
        }

        public async Task<List<Station>> GetStations()
        {
            HttpResponseMessage response = await client.GetAsync("https://api.jcdecaux.com/vls/v3/stations" + "?apiKey=" + apiKey);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            return JsonConvert.DeserializeObject<List<Station>>(responseBody, settings);
        }
    }

    [DataContract]
    public class Station
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string address { get; set; }

        [DataMember]
        public int number { get; set; }

        [DataMember]
        public string contract_name { get; set; }

        [DataMember]
        public string status { get; set; }

        [DataMember]
        public Position position { get; set; }

        [DataMember]
        public TotalStands totalStands { get; set; }
    }
    [DataContract]
    public class Availabilities
    {
        [DataMember]
        public int bikes { get; set; }
        [DataMember]
        public int stands { get; set; }
        [DataMember]
        public int mechanicalBikes { get; set; }
        [DataMember]
        public int electricalBikes { get; set; }
        [DataMember]
        public int electricalInternalBatteryBikes { get; set; }
        [DataMember]
        public int electricalRemovableBatteryBikes { get; set; }
    }
    [DataContract]
    public class TotalStands
    {
        [DataMember]
        public Availabilities availabilities { get; set; }
        [DataMember]
        public int capacity { get; set; }
    }



    [DataContract]
    public class Position
    {
        [DataMember]
        public double latitude { get; set; }

        [DataMember]
        public double longitude { get; set; }

        public Position(Double p1, Double p2)
        {
            latitude = p1;
            longitude = p2;
        }
    }
}
