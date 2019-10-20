using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Models.Models
{
    [JsonObject]
    public class Review
    {

        [JsonProperty("UniGuid")]
        public string UniGuid { get; set; }

        [JsonProperty("FacultyGuid")]
        public string FacultyGuid { get; set; }

        [JsonProperty("LecturerGuid")]
        public string LecturerGuid { get; set; }

        [JsonProperty("CourseGuid")]
        public string CourseGuid { get; set; }
        
        [JsonProperty("Text")]
         public string Text { get; set; }

        [JsonProperty("Value")]
         public string Value { get; set; }

        [JsonProperty("UserId")]
        public string UserId { get; set; }

        [JsonProperty("ReviewGuid")]
        public string ReviewGuid { get; set; }

    }
}
