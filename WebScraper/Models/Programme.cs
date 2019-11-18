using Newtonsoft.Json;

namespace WebScraper.Models
{
    [JsonObject]
    class Programme
    {    
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Guid")]
        public string Guid { get; set; }

        [JsonProperty("FacultyGuid")]
        public string FacultyGuid { get; set; }
    }
}
