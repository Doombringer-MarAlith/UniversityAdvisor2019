namespace Models
{
    public class Faculty
    {
        public string Name { get; set; }

        /* [JsonProperty("Location")]
         public string Location { get; set; }

         [JsonProperty("StudentCount")]
         public string StudentCount { get; set; }*/

        public int UniversityId { get; set; }

        public int Id { get; set; }
    }
}