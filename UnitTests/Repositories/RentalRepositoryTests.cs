using System;
using System.Collections.Generic;
using NUnit.Framework;
using VideoWorld.Models;
using VideoWorld.Repositories;
using VideoWorld.Utils;

namespace UnitTests.Repositories
{
    class RentalRepositoryTests
    {
        private RentalRepository rentalRepository;
        private readonly Customer customer = new Customer("M F", "mf", "mf-pw");

        [SetUp]
        public void SetUp()
        {
            rentalRepository = new RentalRepository();
            var rentals = new List<Rental>
                              {
                                  new Rental(
                                      new Movie("Top Gun", Movie.REGULAR),
                                      Period.Of(DateTime.Now.AddDays(-2), Duration.OfDays(3)),
                                      customer
                                      ),
                                  new Rental(
                                      new Movie("Star Wars", Movie.NEW_RELEASE),
                                      Period.Of(DateTime.Now.AddDays(-3), Duration.OfDays(2)),
                                      customer)
                              };
            rentalRepository.Add(rentals);
        }

        [Test]
        public void ShouldGetAllRentalsForCustomer()
        {
            var rentals = rentalRepository.AllRentalsFor(customer);
            Assert.That(rentals.Count, Is.EqualTo(2));
        }

        [Test]
        public void ShouldGetCurrentRentalsForCustomer()
        {
            var currentRentals = rentalRepository.CurrentRentalsFor(customer);
            Assert.That(currentRentals.Count, Is.EqualTo(1));
            Assert.That(currentRentals.Exists(rental => rental.Movie.Title == "Top Gun"));
        }
    }
}