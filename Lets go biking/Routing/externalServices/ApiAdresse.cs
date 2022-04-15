using Newtonsoft.Json;
using Routing.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Routing.externalServices
{
    class ApiAdresse
    {
        static readonly HttpClient client = new HttpClient();
        public async Task<List<ConvertResult>> convertAddress(String address)
        {
            HttpResponseMessage response = await client.GetAsync("http://localhost:8735/Design_Time_Addresses/ProxyCache/Service1/rest/convert?address=" + address);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            return JsonConvert.DeserializeObject<Address>(responseBody, settings).convertResult;
        }
    }
}
