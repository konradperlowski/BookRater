namespace BookRater.Models
{
    using Newtonsoft.Json;

    public class Book
    {
        [JsonProperty(PropertyName = "id")] public string Id { get; set; }

        [JsonProperty(PropertyName = "title")] public string Title { get; set; }

        [JsonProperty(PropertyName = "author")] public string Author { get; set; }
        
        [JsonProperty(PropertyName = "rates")] public RateBook[] Rates { get; set; }
    }

    public class RateBook
    {
        [JsonProperty(PropertyName = "rate")] public int Rate { get; set; }
        [JsonProperty(PropertyName = "userId")] public string UserId { get; set; }
    }
}