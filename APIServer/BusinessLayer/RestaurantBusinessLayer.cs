using System.Collections.Generic;
using System.Threading.Tasks;
using static APIServer.DataAccessLayer.DataAccessLayer;

namespace APIServer
{
    public interface IRestaurantBusinessLayer
    {
        Task<Restaurant> GetRestaurant(string id);
        Task<IYelpAPISearchResult> GetRestaurants(string location, string term);
    }

    public class RestaurantBusinessLayer : IRestaurantBusinessLayer
    {
        //Implement dependency Injection here to avoid tight coupling of the restaurant dataaccesslayer 
        //The business layer can take up any object of type IRestaurantDataAccessLayer for future proofing changes in the source 
        IRestaurantDataAccessLayer restaurantDataAccess;
        public RestaurantBusinessLayer(IRestaurantDataAccessLayer _restaurantDataAccess)
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