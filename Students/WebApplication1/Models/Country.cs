using Newtonsoft.Json;

namespace WebApplication1.Models
{
    public class Country
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
