using APIServer.Models;
using System.Collections.Generic;

namespace APIServer.Abstractions
{
    public interface IYelpAPISearchResult
    {
        List<Restaurant> businesses { get; set; }
        Region region { get; set; }
        int total { get; set; }
    }
}
