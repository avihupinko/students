using Newtonsoft.Json;

namespace WebApplication1.Models
{
    public class StudentPageModel
    {
        [JsonProperty("data")]
        public List<StudentViewModel> Data { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
