using BookRater.Models;
using NUnit.Framework;

namespace Tests
{
    public class BookWithRatesTest
    {
        private Book book;
        private string userId;

        [SetUp]
        public void Setup()
        {
            book = new Book()
            {
                Title = "Test title",
                Author = "Test author"
            };
            userId = "Test";
        }

        [Test]
        public void ShouldCreateWhenNoRates()
        {
            var bookWithRates = new BookWithRates(book, userId);

            Assert.AreEqual(0, bookWithRates.AverageRate);
            Assert.AreEqual(0, bookWithRates.UserRate);
        }

        [Test]
        public void ShouldCreateWhenUserDidntRate()
        {
            book.Rates = new[]
            {
                new RateBook
                {
                    Rate = 5,
                    UserId = "TestId"
                },
                new RateBook
                {
                    Rate = 10,
                    UserId = "TestId2"
                }
            };
            var bookWithRates = new BookWithRates(book, userId);

            Assert.AreEqual(7, bookWithRates.AverageRate);
            Assert.AreEqual(0, bookWithRates.UserRate);
        }

        [Test]
        public void ShouldCreateWhenUserRate()
        {
            book.Rates = new[]
            {
                new RateBook
                {
                    Rate = 3,
                    UserId = userId
                },
                new RateBook
                {
                    Rate = 5,
                    UserId = "TestId"
                },
                new RateBook
                {
                    Rate = 10,
                    UserId = "TestId2"
                }
            };
            var bookWithRates = new BookWithRates(book, userId);

            Assert.AreEqual(6, bookWithRates.AverageRate);
            Assert.AreEqual(3, bookWithRates.UserRate);
        }
    }
}