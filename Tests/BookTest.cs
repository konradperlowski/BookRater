using System.Linq;
using NUnit.Framework;
using BookRater.Models;

namespace Tests
{
    public class BookTest
    {
        private string userId;
        private Book book;

        [SetUp]
        public void Setup()
        {
            book = new Book
            {
                Author = "Test author",
                Title = "Test title"
            };
            userId = "Test";
        }

        [Test]
        public void ShouldGenerateIdAfterInit()
        {
            Assert.NotNull(book.Id);
        }
        
        [Test]
        public void ShouldCreateArrayWhenNoRates()
        {
            var rates = book.SaveOrUpdateRate(userId, 5);

            Assert.AreEqual(1, rates.Length);
            Assert.True(rates.Any(r => r.UserId == userId));
        }

        [Test]
        public void ShouldAddUserRate()
        {
            book.Rates = new[]
            {
                new RateBook
                {
                    Rate = 5,
                    UserId = "TestId"
                }
            };
            var rates = book.SaveOrUpdateRate(userId, 5);

            Assert.AreEqual(2, rates.Length);
            Assert.True(rates.Any(r => r.UserId == userId));
        }

        [Test]
        public void ShouldUpdateUserRate()
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
                    Rate = 6,
                    UserId = userId
                }
            };
            var rates = book.SaveOrUpdateRate(userId, 3);

            Assert.AreEqual(2, rates.Length);
            Assert.True(rates.Any(r => r.UserId == userId && r.Rate == 3));
        }
    }
}