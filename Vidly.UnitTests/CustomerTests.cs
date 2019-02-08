using NUnit.Framework;
using vidly.Models;

namespace Vidly.UnitTests
{
    [TestFixture]
    class CustomerTests
    {
        [Test]
        public void CanReserveMore_LessThan5MoviesRented_ReturnsTrue()
        {
            Customer customer = new Customer();

            for(var i = 0; i < 3; i++)
            {
                customer.Movies.Add(new Movie());
            }

            var result = customer.CanReserveMore();

            Assert.That(result , Is.True);
        }

        [Test]
        public void CanReserveMore_EqualTo5MoviesRented_ReturnsFalse()
        {
            Customer customer = new Customer();

            for (var i = 0; i < 5; i++)
            {
                customer.Movies.Add(new Movie());
            }

            Assert.That(customer.CanReserveMore(), Is.False);
        }

        [Test]
        public void CanReserveMore_MoreThan5MoviesRented_ReturnsFalse()
        {
            Customer customer = new Customer();

            for (var i = 0; i < 10; i++)
            {
                customer.Movies.Add(new Movie());
            }

            Assert.That(customer.CanReserveMore(), Is.False);
        }
    }
}
