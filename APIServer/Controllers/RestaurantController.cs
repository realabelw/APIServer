using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServer.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly ILogger<RestaurantController> _logger;

        //this object is instantiated using dependency injecction which is registered
        //in services at the startup class [ConfigureServices()]
        private readonly IRestaurantBusinessLayer restaurantBusinessLayer;

        //using dependency injection, we get the instance of the businessLayer
        public RestaurantController(IRestaurantBusinessLayer _businessLayer, ILogger<RestaurantController> logger)
        {
            this.restaurantBusinessLayer = _businessLayer;
            _logger = logger;
        }

        /// <summary>
        /// this function returns the list of all restaurants that match the search.
        /// using and object instantiated via dependency injection
        /// </summary>
        /// <param name="location"></param>
        /// <param name="term"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/{action}")] //api/restaurants
        public List<Restaurant> Restaurants(string location, string term)
        {
            return restaurantBusinessLayer.GetRestaurants(location, term);
        }

        /// <summary>
        /// this function returns the restaurant that matches a specific id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/{action}/{id}")] //route with dynamic parameter value => api/restaurants
        public Restaurant Restaurants(string id)
        {
            return restaurantBusinessLayer.GetRestaurant(id);
        }
    }
}