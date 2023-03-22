using System.Collections.Generic;
using System.Threading.Tasks;
using APIServer.Abstractions;
using APIServer.Models;

namespace APIServer.Services
{
    public class RestaurantSearchService : IRestaurantSearchService
    {
        //Implement dependency Injection here to avoid tight coupling to the data access layer 
        //The business layer can take up any object of type IRestaurantDataAccessLayer for future proofing changes in the source 
        IRestaurantDataAccessLayer restaurantDataAccess;
        public RestaurantSearchService(IRestaurantDataAccessLayer _restaurantDataAccess)
        {
            this.restaurantDataAccess = _restaurantDataAccess;
        }

        public async Task<IYelpAPISearchResult> GetRestaurants(string location, string term)
        {
            //get list of restaurants through dependency injection
            return await restaurantDataAccess.GetRestaurants(location, term);
        }

        public Task<Restaurant> GetRestaurant(string id)
        {
            //get list of restaurants through dependency injection
            return restaurantDataAccess.GetRestaurant(id);
        }
    }
}