using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using MvcContrib.TestHelper;
using NUnit.Framework;
using VideoWorld.Controllers;
using VideoWorld.Models;
using VideoWorld.Repositories;
using VideoWorld.Utils;
using VideoWorld.ViewModels;

namespace UnitTests.Controllers
{
    [TestFixture]
    class CheckoutControllerTests
    {
        private Customer customer;
        private ICustomerRepository customerRepository;
        private CheckoutController controller;
        private TransactionRepository transactionRepository;
        private RentalRepository rentalRepository;

        [SetUp]
        public void Setup()
        {
            customer = new Customer("John Smith", "jsmith", "password");
            customerRepository = new CustomerRepository();
            customerRepository.Add(customer);
            
            transactionRepository = new TransactionRepository();
            rentalRepository = new RentalRepository();
            var builder = new TestControllerBuilder();
            controller = builder.CreateController<CheckoutController>(customerRepository, rentalRepository, transactionRepository);
            controller.Session["CurrentUser"] = customer.Username;
        }

        [Test]
        public void ShouldPopulateStatementModel()
        {
            AddMovieToCart("Bad Boys 2", 2);
            var currentRentals = new List<Rental>();
            currentRentals.AddRange(customer.Cart.Rentals);
            var viewResult = controller.CheckOut();
            viewResult.AssertViewRendered().ForView("Index");
            var viewModel = viewResult.Model as StatementViewModel;
            Assert.That(viewModel.Customer, Is.EqualTo(customer));
            Assert.That(viewModel.Statement, Contains.Substring("Rental Record for " + customer.DisplayName));
        }

        [Test]
        public void ShouldClearCartOnCheckout()
        {
            AddMovieToCart("Mad Max 2", 1);
            Assert.That(customer.Cart.Count, Is.EqualTo(1));
            controller.CheckOut();
            Assert.That(customer.Cart.Count, Is.EqualTo(0));
        }

        [Test]
        public void ShouldCreateTransactionWithSameCustomer()
        {
            controller.CheckOut();
            Assert.That(transactionRepository.FindById(0).Customer, Is.SameAs(customer));
        }

        [Test]
        public void ShouldRecordTransaction()
        {
            var topGun = new Movie("Top Gun", new RegularPrice());
            Period rentalPeriod = Period.Of(DateTime.Now, Duration.OfDays(2));
            customer.Cart.AddMovie(topGun, rentalPeriod, customer);
            controller.CheckOut();
            Assert.That(transactionRepository.FindById(0).Rentals.Count, Is.EqualTo(1));
            Assert.That(transactionRepository.FindById(0).Rentals.First().Movie, Is.EqualTo(topGun));
            Assert.That(transactionRepository.FindById(0).Rentals.First().Period, Is.EqualTo(rentalPeriod));
        }

        private void AddMovieToCart(string title, int rentedDays)
        {
            customer.Cart.AddMovie(new Movie(title, new NewReleasePrice()),
                                   Period.Of(DateTime.Now, Duration.OfDays(rentedDays)), customer);
        }
    }
}
