using Newtonsoft.Json;
using Routing.models;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Net.Http;
using System.Threading.Tasks;

namespace Routing.externalServices
{
    class JCDecaux
    {
        string apiKey = "079e5c452a49c4c91275b1be1e96300f1194190a";
        static readonly HttpClient client = new HttpClient();
        public async Task<List<Station>> GetStations()
        {
            HttpResponseMessage response = await client.GetAsync("http://localhost:8735/Design_Time_Addresses/ProxyCache/Service1/rest/stations");
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
}
