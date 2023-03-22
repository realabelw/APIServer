using System.Collections.Generic;

namespace APIServer
{
    public interface IYelpAPISearchResult
    {
        List<Restaurant> businesses { get; set; }
        Region region { get; set; }
        int total { get; set; }
    }

    public class BusinessSearchResult : IYelpAPISearchResult
    {
        //enforcing interface properties for type safety
        public List<Restaurant> businesses { get; set; }
        public int total { get; set; }
        public Region region { get; set; }
    }
}
