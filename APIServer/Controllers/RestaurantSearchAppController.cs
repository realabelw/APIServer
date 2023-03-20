using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantSearchAppController : ControllerBase
    {
        private readonly ILogger<RestaurantSearchAppController> _logger;

        public RestaurantSearchAppController(ILogger<RestaurantSearchAppController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<RestaurantSearchApp> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new RestaurantSearchApp
            {

            })
            .ToArray();
        }
    }
}
