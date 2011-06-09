using System.Collections.Generic;
using System.Web.Mvc;
using MvcContrib.TestHelper;
using NUnit.Framework;
using VideoWorld.Controllers;
using VideoWorld.Models;
using VideoWorld.Repositories;

namespace UnitTests.Controllers
{
    public class HomePageControllerTests
    {
        private HomePageController controller;
        private ICustomerRepository customerRepository;
        private Customer customer;
        private MovieRepository movieRepository;

        [SetUp]
        public void SetUp()
        {
            customerRepository = new CustomerRepository();
            customer = new Customer("Test Customer", "test", "password");
            customerRepository.Add(customer);

            movieRepository = new MovieRepository();
            var movies = new List<Movie>
                             {
                                 new Movie("Avatar", new NewReleasePrice()),
                                 new Movie("Up In The Air", new RegularPrice())
                             };
            movieRepository.Add(movies);
            
            var builder = new TestControllerBuilder();
            controller = builder.CreateController<HomePageController>(customerRepository, movieRepository);
            controller.Session["CurrentUser"] = customer.Username;
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
            Assert.That(model.Movies.Count, Is.EqualTo(2));
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
