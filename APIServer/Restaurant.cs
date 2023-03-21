using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServer
{   
    /// <summary>
    /// Restaurant class to map to json result
    /// </summary>
    public class Restaurant
    {
        public string id { get; set; }
        //public string alias { get; set; } //include to map json object but ignored
        public string name { get; set; }
        public Location location { get; set; }
        public string image_url { get; set; }
        //public bool is_closed { get; set; } //include to map json object but ignored
        //public string url { get; set; } //include to map json object but ignored
        public int review_count { get; set; }
        public double rating { get; set; }
        public string phone { get; set; }
        public List<Category> categories { get; set; }
        //public Coordinates coordinates { get; set; } //include to map json object but ignored
        //public List<string> transactions { get; set; } //include to map json object but ignored
        //public string display_phone { get; set; } //include to map json object but ignored
        //public double distance { get; set; } //include to map json object but ignored
        //public string price { get; set; } //include to map json object but ignored
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
        //public string address3 { get; set; } //include to map json object but ignored
        public string city { get; set; }
        public string zip_code { get; set; }
        //public string country { get; set; } //include to map json object but ignored
        public string state { get; set; }
        //public List<string> display_address { get; set; } //include to map json object but ignored
    }
}
