using APIServer.Abstractions;
using APIServer.Models;
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
    //[Route("api/[action]")]
    [Route("api/restaurants")]
    public class RestaurantSearchAPIController : ControllerBase
    {
        private readonly ILogger<RestaurantSearchAPIController> _logger;
        private const string restaurantsListCacheKey = "restaurantsList"; //to track the cache item for /api/restaurants
        private const string restaurantIDCacheKey = "restaurantID"; //to track the cache item for /api/restaurants/{id}

        //this object is injected using dependency injecction in the startup class [ConfigureServices()]
        private readonly IRestaurantSearchService restaurantBusinessLayer;

        //manage concurrent cache access
        //using the slim because the wait times are expected to be very short.
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        //memory cache service, using dependency injection
        private readonly IMemoryCache _cache;

        //using dependency injection, we get the instance of the businessLayer
        public RestaurantSearchAPIController(IRestaurantSearchService _businessLayer, ILogger<RestaurantSearchAPIController> logger, IMemoryCache cache)
        {
            //safety checks, we need to have a valid instance
            this.restaurantBusinessLayer = _businessLayer ?? throw new ArgumentNullException(nameof(_businessLayer));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// URL: api/restaurants
        /// Required fields are location and term
        /// This function returns the list of all restaurants that match the search.
        /// Using and object instantiated via dependency injection
        /// The method is async to implement asynchronous calls
        /// </summary>
        /// <param name="location"></param>
        /// <param name="term"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{location}/{term}")]
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
                    //restaurants = await Task.Run(() => restaurantBusinessLayer.GetRestaurants(location, term));
                    var searchResult = await restaurantBusinessLayer.GetRestaurants(location, term);
                    restaurants = searchResult.businesses;

                    //expire set cache item after 5 mins => 300s
                    //SlidingExpiration will expire the entry if it hasn't been accessed in 1 minute
                    //AbsoluteExpiration will expire the entry after 5 minutes
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(300)) //expire after 5 mins
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
        /// URL: api/restaurants/{id}
        /// Required field is {id}
        /// This function returns the restaurant that matches a specific id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")] //route with dynamic parameter value => id
        public async Task<Restaurant> Restaurants(string id)
        {
            //return await restaurantBusinessLayer.GetRestaurant(id);

            _logger.Log(LogLevel.Information, "Trying to fetch the restaurant from cache.");
            if (_cache.TryGetValue(restaurantIDCacheKey, out Restaurant restaurant))
            {
                _logger.Log(LogLevel.Information, "Restaurant found in cache.");
            }
            else
            {
                try
                {
                    //wait until the lock is released
                    await semaphore.WaitAsync();

                    _logger.Log(LogLevel.Information, "Restaurant not found in cache. Fetching from Yelp Fusion API.");

                    //lets have this on a seperate thread
                    //restaurants = await Task.Run(() => restaurantBusinessLayer.GetRestaurants(location, term));
                    restaurant = await restaurantBusinessLayer.GetRestaurant(id);

                    //expire set cache item after 5 mins => 300s
                    //SlidingExpiration will expire the entry if it hasn't been accessed in 1 minute
                    //AbsoluteExpiration will expire the entry after 5 minutes
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(300)) //expire after 5 mins
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