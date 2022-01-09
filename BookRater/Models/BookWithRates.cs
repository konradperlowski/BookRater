namespace BookRater.Models
{
    public class BookWithRates
    {
        public Book Book { get; init; }
        public int UserRate { get; init; }
        public double AverageRate { get; set; }
    }
}