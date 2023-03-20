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

        public RestaurantController(ILogger<RestaurantController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("api/{action}")] //api/restaurants
        public List<Restaurant> Restaurants(string location, string term)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Restaurant
            {

            })
            .ToList();
        }

        [HttpGet]
        [Route("api/{action}/{id}")] //route with dynamic parameter value => api/restaurants
        public Restaurant Restaurants(string id)
        {
            return new Restaurant();
        }
    }
}
