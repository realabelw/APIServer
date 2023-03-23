using APIServer.Abstractions;
using APIServer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace APIServer.DataAccessLayer
{
    public class RestaurantDataAccessLayer : IRestaurantDataAccessLayer
    {
        //This has moved to starup
        //private string API_KEY;
        //private string API_Endpoint;

        //private IBusinessSearchResult businessSearchInstance;
        private IHttpClientFactory httpClientFactory;
        private string errorString;
        private IYelpAPISearchResult searchResult;
        private Restaurant restaurant;
        private readonly ILogger<RestaurantDataAccessLayer> _logger;

        public RestaurantDataAccessLayer(IConfiguration configuration, IHttpClientFactory _clientFactory, ILogger<RestaurantDataAccessLayer> logger)
        {
            httpClientFactory = _clientFactory; //@inject httpClientFactory here
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            //This has moved to starup
            //API_KEY = configuration.GetValue<string>("YelpFusionAPIKey");
            //API_Endpoint = configuration.GetValue<string>("YelpFusionAPIEndpoint");
        }

        public async Task<IYelpAPISearchResult> GetRestaurants(string location, string term)
        {
            try
            {
                //HttpClientFactory creates the client from the pool, this solves socket exhaustion causing overheads
                //Client settings are defined in startup configureservices method for BaseAddress, DefaultRequestHeaders
                var client = httpClientFactory.CreateClient("meta"); //named client instance: settings defined in startup class

                HttpResponseMessage response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"search?term={term}&location={location}"));

                if (response.IsSuccessStatusCode)
                {
                    _logger.Log(LogLevel.Information, "Trying to fetch the search result from Yelp Fusion API.");
                    searchResult = await response.Content.ReadFromJsonAsync<BusinessSearchResult>();
                    errorString = string.Empty;
                    _logger.Log(LogLevel.Information, "Search result completed successfully.");
                }
                else
                {
                    errorString = $"There was an error getting the result: {response.ReasonPhrase}";
                    _logger.Log(LogLevel.Error, $"Error: {errorString}");

                }

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
            }

            return searchResult ?? null; //return null for unit test case
        }

        public async Task<Restaurant> GetRestaurant(string id)
        {
            try
            {
                var client = httpClientFactory.CreateClient("meta"); //named client instance: settings defined in startup class

                HttpResponseMessage response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, id));

                if (response.IsSuccessStatusCode)
                {
                    _logger.Log(LogLevel.Information, "Trying to fetch the search result from Yelp Fusion API.");
                    restaurant = await response.Content.ReadFromJsonAsync<Restaurant>();
                    errorString = string.Empty;
                    _logger.Log(LogLevel.Information, "Search result completed successfully.");
                }
                else
                {
                    errorString = $"There was an error getting the result: {response.ReasonPhrase}";
                    _logger.Log(LogLevel.Error, $"Error: {errorString}");

                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
            }

            return restaurant ?? new Restaurant();
        }
    }
}