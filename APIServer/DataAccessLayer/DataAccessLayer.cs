using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServer.DataAccessLayer
{
    public class DataAccessLayer
    {
        public interface IRestaurantDataAccessLayer
        {
            List<Restaurant> GetRestaurants(string location, string term);
            Restaurant GetRestaurant(string id);
        }

        public class RestaurantDataAccessLayer : IRestaurantDataAccessLayer
        {
            public List<Restaurant> GetRestaurants(string location, string term)
            {
                var rng = new Random();
                return Enumerable.Range(1, 5).Select(index => new Restaurant
                {

                })
                .ToList();
            }

            public Restaurant GetRestaurant(string id)
            {
                return new Restaurant();
            }
        }

    }
}
