using APIServer.Models;
using System.Threading.Tasks;

namespace APIServer.Abstractions
{
    public interface IRestaurantDataAccessLayer
    {
        Task<IYelpAPISearchResult> GetRestaurants(string location, string term);
        Task<Restaurant> GetRestaurant(string id);
    }
}