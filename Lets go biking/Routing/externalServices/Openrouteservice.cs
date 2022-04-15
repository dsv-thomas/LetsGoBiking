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
    internal class Openrouteservice
    {
        string apiKey = "5b3ce3597851110001cf6248e4dd7eb3344b478fa618aecc746388d3";
        static readonly HttpClient client = new HttpClient();
        public async Task<GeoJson> GetRoute(Position pStart, Position pEnd, String profile)
        {
            String url = "https://api.openrouteservice.org/v2/directions/"+profile+"?api_key=" + apiKey + "&start=" + pStart.latitude.ToString().Replace(",", ".") + "," + pStart.longitude.ToString().Replace(",", ".") + "&end="+pEnd.latitude.ToString().Replace(",", ".") + ","+pEnd.longitude.ToString().Replace(",", ".");
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            return JsonConvert.DeserializeObject<GeoJson>(responseBody, settings);
        }
    }
}
