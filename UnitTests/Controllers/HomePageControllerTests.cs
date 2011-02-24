using System;
using System.Web.Mvc;
using MvcContrib.TestHelper;
using NUnit.Framework;
using VideoWorld.Controllers;
using VideoWorld.Models;

namespace UnitTests.Controllers
{
    public class HomePageControllerTests
    {
        private HomePageController controller;
        private CustomerRepository customerRepository;
        private Customer customer;

        [SetUp]
        public void SetUp()
        {
            var builder = new TestControllerBuilder();
            customerRepository = new CustomerRepository();
            customer = new Customer("Test Customer");
            customerRepository.Add(customer);
            controller = builder.CreateController<HomePageController>(customerRepository);
            controller.Session["CurrentUser"] = customer.Name;
        }

        [Test]
        public void ShouldShowIndexView()
        {
            ViewResult result = controller.Index();
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void ViewShouldShowAListOfMovies()
        {
            ViewResult result = controller.Index();
            var model = (HomePageModel) result.Model;
            var movies = model.Movies;
            Assert.That(movies.Count, Is.EqualTo(3));
        }

        [Test]
        public void ModelShouldIncludeCart()
        {
            ViewResult result = controller.Index();
            var model = (HomePageModel)result.Model;
            Assert.That(model.Cart, Is.SameAs(customer.Cart));
        }
    }
}
