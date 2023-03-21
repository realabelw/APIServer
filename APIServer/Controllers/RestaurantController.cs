using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace APIServer.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly ILogger<RestaurantController> _logger;
        private const string restaurantsListCacheKey = "restaurantsList";
        private const string restaurantIDCacheKey = "restaurantID";
        //this object is instantiated using dependency injecction which is registered
        //in services at the startup class [ConfigureServices()]
        private readonly IRestaurantBusinessLayer restaurantBusinessLayer;

        //manage concurrent cache access
        //using the slim because the wait times are expected to be very short.
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        //memory cache service, using dependency injection
        private readonly IMemoryCache _cache;

        //using dependency injection, we get the instance of the businessLayer
        public RestaurantController(IRestaurantBusinessLayer _businessLayer, ILogger<RestaurantController> logger, IMemoryCache cache)
        {
            //safety checks, we need to have a valid instance
            this.restaurantBusinessLayer = _businessLayer ?? throw new ArgumentNullException(nameof(_businessLayer));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// this function returns the list of all restaurants that match the search.
        /// using and object instantiated via dependency injection
        /// the method is async to implement asynchronous calls
        /// </summary>
        /// <param name="location"></param>
        /// <param name="term"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/{action}")] //api/restaurants
        public async Task<List<Restaurant>> Restaurants(string location, string term)
        {
            //return restaurantBusinessLayer.GetRestaurants(location, term);

            _logger.Log(LogLevel.Information, "Trying to fetch the list of restaurants from cache.");
            if (_cache.TryGetValue(restaurantsListCacheKey, out List<Restaurant> restaurants))
            {
                _logger.Log(LogLevel.Information, "Restaurants list found in cache.");
            }
            else
            {
                try
                {
                    //wait until the lock is released
                    await semaphore.WaitAsync();

                    _logger.Log(LogLevel.Information, "Restaurants list not found in cache. Fetching from Yelp Fusion API.");

                    //lets have this on a seperate thread
                    restaurants = await Task.Run(() => restaurantBusinessLayer.GetRestaurants(location, term));

                    //expire set cache item after 5 mins => 300s
                    //AbsoluteExpiration will expire the entry after a set amount of time.
                    //SlidingExpiration will expire the entry if it hasn't been accessed in a set
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(300))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(restaurantsListCacheKey, restaurants, cacheEntryOptions);
                }
                finally
                {
                    semaphore.Release();
                }
            }

            return restaurants;
        }

        /// <summary>
        /// this function returns the restaurant that matches a specific id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/{action}/{id}")] //route with dynamic parameter value => api/restaurants
        public async Task<Restaurant> Restaurants(string id)
        {
            //return restaurantBusinessLayer.GetRestaurant(id);

            _logger.Log(LogLevel.Information, "Trying to fetch the restaurant id from cache.");
            if (_cache.TryGetValue(restaurantIDCacheKey, out Restaurant restaurant))
            {
                _logger.Log(LogLevel.Information, "Restaurant id found in cache.");
            }
            else
            {
                try
                {
                    //wait until the lock is released
                    await semaphore.WaitAsync();

                    _logger.Log(LogLevel.Information, "Restaurant id not found in cache. Fetching from Yelp Fusion API.");

                    //lets have this on a seperate thread
                    restaurant = await Task.Run(() => restaurantBusinessLayer.GetRestaurant(id));

                    //expire set cache item after 5 mins => 300s
                    //AbsoluteExpiration will expire the entry after a set amount of time.
                    //SlidingExpiration will expire the entry if it hasn't been accessed in a set
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(300))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(restaurantIDCacheKey, restaurant, cacheEntryOptions);
                }
                finally
                {
                    semaphore.Release();
                }
            }

            return restaurant;
        }
    }
}