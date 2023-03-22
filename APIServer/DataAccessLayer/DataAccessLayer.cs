using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace APIServer.DataAccessLayer
{
    public class DataAccessLayer
    {
        public interface IRestaurantDataAccessLayer
        {
            Task<IBusinessSearchResult> GetRestaurants(string location, string term);
            Task<Restaurant> GetRestaurant(string id);
        }

        public class RestaurantDataAccessLayer : IRestaurantDataAccessLayer
        {
            private string API_KEY;
            private string API_Endpoint;
            //private IBusinessSearchResult businessSearchInstance;

            public RestaurantDataAccessLayer(IConfiguration configuration)
            {
                API_KEY = configuration.GetValue<string>("YelpFusionAPIKey");
                API_Endpoint = configuration.GetValue<string>("YelpFusionAPIEndpoint");
                //businessSearchInstance = _businessSearchInstance;
            }

            public async Task<IBusinessSearchResult> GetRestaurants(string location, string term)
            {
                RestaurantSearchList result;

                string baseurl = $"{API_Endpoint}/search?term={term}&location={location}";

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", API_KEY);
                string jsonStr = await client.GetStringAsync(baseurl);

                //result = JsonConvert.DeserializeObject<List<Restaurant>>(jsonStr).ToList();
                result = JsonConvert.DeserializeObject<RestaurantSearchList>(jsonStr);

                return result ?? new RestaurantSearchList();
            }

            public async Task<Restaurant> GetRestaurant(string id)
            {
                Restaurant result;

                string baseurl = $"{API_Endpoint}/{id}";

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", API_KEY);
                string jsonStr = await client.GetStringAsync(baseurl);

                result = JsonConvert.DeserializeObject<Restaurant>(jsonStr);

                return result ?? new Restaurant();
            }
        }
    }
}
