using Newtonsoft.Json;

namespace RedisWorkshop
{
    public class ListingsInfo
    {
        public ListingsInfo(int id, string listingUrl, string name, double? latitude, double? longitude)
        {
            Id = id;
            ListingUrl = listingUrl;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }

        [JsonProperty(PropertyName = "Id")]
        public int Id { get; set; }
        
        [JsonProperty(PropertyName = "ListingUrl")]
        public string ListingUrl { get; set; }
        
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        
        [JsonProperty(PropertyName = "Latitude")]
        public double? Latitude { get; set; }
        
        [JsonProperty(PropertyName = "Longitude")]
        public double? Longitude { get; set; }
    }
}