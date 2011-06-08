using System;
using System.Collections.Generic;
using NUnit.Framework;
using VideoWorld.Models;
using VideoWorld.Utils;
using System.Linq;

namespace UnitTests.Models
{
    [TestFixture]
    public class TransactionTests
    {
        private static Movie FINDING_NEMO = new Movie("Finding Nemo", Movie.CHILDRENS);
        private static Movie SHAWSHANK_REDEMPTION = new Movie("Shawshank Redemption", Movie.REGULAR);

        private static Customer CUSTOMER_ONE = new Customer("James Cameron", "jcameron", "password1");
        private static Customer CUSTOMER_TWO = new Customer("Quentin Tarantino", "qtarantino", "password2");

        private static Rental RENTAL_ONE = new Rental(FINDING_NEMO, Period.Of(DateTime.Now,
                                                                              Duration.OfDays(1)), CUSTOMER_ONE);

        private static Rental RENTAL_TWO = new Rental(SHAWSHANK_REDEMPTION, Period.Of(
            DateTime.Now, Duration.OfDays(3)), CUSTOMER_TWO);

        [Test]
        public void ShouldReturnDifferentRentalTransactionFromConstruction()
        {
            var rentals = new List<Rental>();
            rentals.Add(RENTAL_ONE);
            var transaction = new Transaction(CUSTOMER_ONE, DateTime.Now, rentals);
            rentals.Add(RENTAL_TWO);
            Assert.IsFalse(rentals.Equals(transaction.Rentals));
            Assert.That(transaction.Rentals.Count, Is.EqualTo(1));
            Assert.That(transaction.Rentals.First(), Is.EqualTo(RENTAL_ONE));
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowExceptionIfRentalForDifferentCustomer()
        {
            var rentals = new List<Rental>();
            rentals.Add(RENTAL_ONE);
            var transaction = new Transaction(CUSTOMER_TWO, DateTime.Now, rentals);
            Assert.Fail();
        }
    }
}