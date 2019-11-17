using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Webserver.Models
{
    [JsonObject]
    public class Faculty
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

       /* [JsonProperty("Location")]
        public string Location { get; set; }

        [JsonProperty("StudentCount")]
        public string StudentCount { get; set; }*/

        [JsonProperty("UniGuid")]
        public string UniGuid { get; set; }

        [Key]
        [JsonProperty("Id")]
        public string Id { get; set; }
    }
}
