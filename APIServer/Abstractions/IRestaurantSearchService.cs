using APIServer.Models;
using System.Threading.Tasks;

namespace APIServer.Abstractions
{
    public interface IRestaurantSearchService
    {
        Task<Restaurant> GetRestaurant(string id);
        Task<IYelpAPISearchResult> GetRestaurants(string location, string term);
    }
}