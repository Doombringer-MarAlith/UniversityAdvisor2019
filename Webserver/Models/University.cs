using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Webserver.Models
{
    [JsonObject]
    public class University
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }
        
        [JsonProperty("Location")]
        public string Location { get; set; }

        [JsonProperty("FoundingDate")]
        public DateTime FoundingDate { get; set; }

        [Key]
        [JsonProperty("Id")]
        public string Id { get; set; }
    }
}
