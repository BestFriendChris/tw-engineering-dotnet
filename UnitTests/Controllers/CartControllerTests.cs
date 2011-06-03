using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MvcContrib.TestHelper;
using NUnit.Framework;
using UnitTests.Models;
using VideoWorld.Controllers;
using VideoWorld.Models;
using VideoWorld.Repositories;

namespace UnitTests.Controllers
{
    class CartControllerTests
    {
        private Customer customer;
        private ICustomerRepository customerRepository;
        private CartController controller;

        [SetUp]
        public void Setup()
        {
            customer = new Customer("John Smith", "jsmith", "password");
            customerRepository = new ListBasedCustomerRepository();
            customerRepository.Add(customer);
            var builder = new TestControllerBuilder();
            controller = builder.CreateController<CartController>(customerRepository);
            controller.Session["CurrentUser"] = customer.Username;
        }

        [Test]
        public void ShouldredirectToHomePageWhenAMovieIsAdded()
        {
            RedirectResult result = controller.RentMovie("Avatar", 1);
            Assert.That(result.Url, Is.EqualTo("/"));
        }

        [Test]
        public void ShouldAddMovieToCart()
        {
            controller.RentMovie("Avatar", 1);
            List<Rental> rentals = customer.Cart.Rentals;
            Assert.That(rentals.Any(r => r.Movie.Title == "Avatar"));
        }

        [Test]
        public void ShouldCreateRentalForOneDay()
        {
            controller.RentMovie("Avatar", 1);
            List<Rental> rentals = customer.Cart.Rentals;
            Assert.That(rentals.First(r => r.Movie.Title == "Avatar").Period, Is.EqualTo(1));
        }

        [Test]
        public void ShouldCreateRentalForMultipleDays()
        {
            controller.RentMovie("Avatar", 2);
            List<Rental> rentals = customer.Cart.Rentals;
            Assert.That(rentals.First(r => r.Movie.Title == "Avatar").Period, Is.EqualTo(2));
        }


        [Test]
        public void ShouldCountMultipleMovies()
        {
            controller.RentMovie("Avatar", 1);
            Assert.That(customer.Cart.Count, Is.EqualTo(1));
            controller.RentMovie("Waterworld", 1);
            Assert.That(customer.Cart.Count, Is.EqualTo(2));
        }

        [Test]
        public void IndexShouldShowCurrentCart()
        {
            ViewResult result = controller.Index();
            Assert.That(result.Model, Is.SameAs(customer.Cart));
        }
    }
}
