using System;
using System.Linq;

namespace BookRater.Models
{
    using Newtonsoft.Json;

    public class Book
    {
        [JsonProperty(PropertyName = "id")] public string Id { get; set; }

        [JsonProperty(PropertyName = "title")] public string Title { get; set; }

        [JsonProperty(PropertyName = "author")] public string Author { get; set; }
        
        [JsonProperty(PropertyName = "rates")] public RateBook[] Rates { get; set; }

        public Book()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Book(BookWithCover bookWithCover)
        {
            Id = Guid.NewGuid().ToString();
            Title = bookWithCover.Title;
            Author = bookWithCover.Author;
        }
        
        public RateBook[] SaveOrUpdateRate(string userId, int rate)
        {
            var rateBook = new RateBook
            {
                UserId = userId,
                Rate = rate
            };
            
            if (Rates == null)
                Rates = new[]
                {
                    rateBook
                };
            else
            {
                var userRated = Rates.Any(r => r.UserId == userId);
                if (userRated)
                {
                    Rates.First(r => r.UserId == userId).Rate = rate;
                }
                else
                {
                    Rates = Rates.Append(rateBook).ToArray();
                }
            }

            return Rates;
        }
    }

    public class RateBook
    {
        [JsonProperty(PropertyName = "rate")] public int Rate { get; set; }
        [JsonProperty(PropertyName = "userId")] public string UserId { get; set; }
    }
}