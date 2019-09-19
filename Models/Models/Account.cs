using System;
using Newtonsoft.Json;

namespace Models.Models
{
    [JsonObject]
    public class Account
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Password")]
        public string Password { get; set; }
        [JsonProperty("Age")]
        public DateTime Age { get; set; }
        [JsonProperty("Email")]
        public string Email { get; set; }
        [JsonProperty("Id")]
        public string Id { get; set; }
        [JsonProperty("Guid")]
        public string Guid { get; set; }
    }
}