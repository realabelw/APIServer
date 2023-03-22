using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APIServer
{
    public interface IRestaurant
    {
        string alias { get; set; }
        List<Category> categories { get; set; }
        Coordinates coordinates { get; set; }
        string display_phone { get; set; }
        double distance { get; set; }
        string id { get; set; }
        string image_url { get; set; }
        bool is_closed { get; set; }
        Location location { get; set; }
        string name { get; set; }
        string phone { get; set; }
        string price { get; set; }
        double rating { get; set; }
        int review_count { get; set; }
        List<string> transactions { get; set; }
        string url { get; set; }
    }

    /// <summary>
    /// Restaurant class to map to json result
    /// </summary>
    public class Restaurant : IRestaurant
    {
        public string id { get; set; }
        [JsonIgnore]
        public string alias { get; set; } //include to map json object but ignored
        public string name { get; set; }
        public Location location { get; set; }
        public string image_url { get; set; }
        [JsonIgnore]
        public bool is_closed { get; set; } //include to map json object but ignored
        [JsonIgnore]
        public string url { get; set; } //include to map json object but ignored
        public int review_count { get; set; }
        public double rating { get; set; }
        public string phone { get; set; }
        public List<Category> categories { get; set; }
        [JsonIgnore]
        public Coordinates coordinates { get; set; } //include to map json object but ignored
        [JsonIgnore]
        public List<string> transactions { get; set; } //include to map json object but ignored
        [JsonIgnore]
        public string display_phone { get; set; } //include to map json object but ignored
        [JsonIgnore]
        public double distance { get; set; } //include to map json object but ignored
        [JsonIgnore]
        public string price { get; set; } //include to map json object but ignored
    }

    public class Category
    {
        public string alias { get; set; }
        public string title { get; set; }
    }

    public class Location
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        [JsonIgnore]
        public string address3 { get; set; } //include to map json object but ignored
        public string city { get; set; }
        [JsonPropertyName("zip")] //dynamically name property
        public string zip_code { get; set; }
        [JsonIgnore]
        public string country { get; set; } //include to map json object but ignored
        public string state { get; set; }
        [JsonIgnore]
        public List<string> display_address { get; set; } //include to map json object but ignored
    }

    public class Center
    {
        public double longitude { get; set; }
        public double latitude { get; set; }
    }

    public class Coordinates
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public interface IRegion
    {
        Center center { get; set; }
    }

    public class Region : IRegion
    {
        public Center center { get; set; }
    }

    public interface IBusinessSearchResult
    {
        List<Restaurant> businesses { get; set; }
        Region region { get; set; }
        int total { get; set; }
    }

    public class RestaurantSearchList : IBusinessSearchResult
    {
        //enforcing interface properties for type safety
        public List<Restaurant> businesses { get; set; }
        public int total { get; set; }
        public Region region { get; set; }
    }
}
