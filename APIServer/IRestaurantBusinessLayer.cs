using System.Collections.Generic;
using static APIServer.DataAccessLayer.DataAccessLayer;

namespace APIServer
{
    public interface IRestaurantBusinessLayer
    {
        Restaurant GetRestaurant(string id);
        List<Restaurant> GetRestaurants(string location, string term);
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

        public List<Restaurant> GetRestaurants(string location, string term)
        {
            //get list of restaurants through dependency injection
            return restaurantDataAccess.GetRestaurants(location, term);
        }

        public Restaurant GetRestaurant(string id)
        {
            //get list of restaurants through dependency injection
            return restaurantDataAccess.GetRestaurant(id);
        }
    }
}