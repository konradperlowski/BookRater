using System.Linq;

namespace BookRater.Models
{
    public class BookWithRates
    {
        public Book Book { get; init; }
        public int UserRate { get; }
        public int AverageRate { get; }

        public BookWithRates(Book book, string userId)
        {
            Book = book;
            var userRated = Book.Rates?.Any(r => r.UserId == userId);
            UserRate = userRated != null && userRated.Value ? Book.Rates.First(r => r.UserId == userId).Rate : 0;
            AverageRate = (int)(Book.Rates?.Average(r => r.Rate) ?? 0);
        }
    }
}