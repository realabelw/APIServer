using APIServer.Abstractions;
using System.Collections.Generic;

namespace APIServer.Models
{
    public class BusinessSearchResult : IYelpAPISearchResult
    {
        //enforcing interface properties for type safety
        public List<Restaurant> businesses { get; set; }
        public int total { get; set; }
        public Region region { get; set; }
        public string Error { get; set; }
    }
}
