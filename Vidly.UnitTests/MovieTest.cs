using NUnit.Framework;
using vidly.Models;

namespace Vidly.UnitTests
{
    [TestFixture]
    class MovieTest
    {
        [Test]
        public void Disponibility_LessThan50CustomersRented_ReturnsTrue()
        {
            Movie movie = new Movie();

            //for (var i = 0; i < 5; i++)
            //{
            movie.Customers.Add(new Customer());
            //}

            var result = movie.Disponibility();

            Assert.That(result, Is.True);
        }

        [Test]
        public void Disponibility_EqualTo50CustomersRented_ReturnsFalse()
        {
            Movie movie = new Movie();

            for (var i = 0; i < 50; i++)
            {
                movie.Customers.Add(new Customer());
            }

            Assert.That(movie.Disponibility(), Is.False);
        }

        [Test]
        public void Disponibility_MoreThan50CustomersRented_ReturnsFalse()
        {
            Movie movie = new Movie();

            for (var i = 0; i < 60; i++)
            {
                movie.Customers.Add(new Customer());
            }

            Assert.That(movie.Disponibility(), Is.False);
        }
    }
}