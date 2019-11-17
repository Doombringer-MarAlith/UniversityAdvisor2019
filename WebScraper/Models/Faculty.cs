﻿using Newtonsoft.Json;

namespace WebScraper.Models
{
    [JsonObject]
    class Faculty
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        /* [JsonProperty("Location")]
         public string Location { get; set; }

         [JsonProperty("StudentCount")]
         public string StudentCount { get; set; }*/

        [JsonProperty("FacultyGuid")]
        public string FacultyGuid { get; set; }

        [JsonProperty("UniGuid")]
        public string UniGuid { get; set; }
    }
}
