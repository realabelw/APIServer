using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIServer
{
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

    public class Region
    {
        public Center center { get; set; }
    }
}
