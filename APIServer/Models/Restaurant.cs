using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIServer
{
    /// <summary>
    /// Restaurant class to map to json result
    /// </summary>
    public class Restaurant
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

}
