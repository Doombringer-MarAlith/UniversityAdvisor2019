using System;
using Newtonsoft.Json;

namespace Models.Models
{
    [JsonObject]
    public class Faculty
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Location")]
        public string Location { get; set; }

        [JsonProperty("StudentCount")]
        public string StudentCount { get; set; }

        [JsonProperty("Guid")]
        public string Guid { get; set; }

        [JsonProperty("UniGuid")]
        public string UniGuid { get; set; }


    }
}
