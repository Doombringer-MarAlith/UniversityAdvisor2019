﻿using Newtonsoft.Json;
using System;

namespace WebScraper.Models
{
    [JsonObject]
    class University
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Guid")]
        public string Guid { get; set; }

        [JsonProperty("Location")]
        public string Location { get; set; }

        [JsonProperty("FoundingDate")]
        public DateTime FoundingDate { get; set; }
    }
}