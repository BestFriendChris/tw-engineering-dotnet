using System.Collections.Generic;
using MvcContrib.TestHelper;
using NUnit.Framework;
using VideoWorld.Controllers;
using VideoWorld.Models;
using VideoWorld.Repositories;
using VideoWorld.ViewModels;

namespace UnitTests.Controllers
{
    [TestFixture]
    public class CurrentRentalsControllerTests
    {
        private Customer customer;
        private ICustomerRepository customerRepository;
        private CurrentRentalsController controller;
        private RentalRepository rentalRepository;
        private List<Rental> rentals;

        [SetUp]
        public void SetUp()
        {
            customer = new Customer("John Smith", "jsmith", "password");
            customerRepository = new CustomerRepository();
            customerRepository.Add(customer);
            rentalRepository = new RentalRepository();
            rentals = new List<Rental>
                              {
                                  GetRental("Avatar", 2), 
                                  GetRental("Up in the Air", 1)
                              };
            rentalRepository.Add(rentals);
            var builder = new TestControllerBuilder();
            controller = builder.CreateController<CurrentRentalsController>(customerRepository, rentalRepository);
            controller.Session["CurrentUser"] = customer.Username;
        }

        private Rental GetRental(string movieTitle, int duration)
        {
            return new Rental(new Movie(movieTitle, new NewReleasePrice()), new Period(duration), this.customer);
        }

        [Test]
        public void ShouldDisplayCurrentRentalsForACustomer()
        {
            var viewResult = controller.Index();
            var currentRentalsModel = (CurrentRentalsModel) viewResult.Model;
            Assert.That(currentRentalsModel.CurrentCustomer, Is.EqualTo(customer));
            Assert.That(currentRentalsModel.CurrentRentals, Is.EqualTo(rentals));
        }
    }
}