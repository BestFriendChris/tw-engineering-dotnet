using System.Collections.Generic;
using System.Web.Mvc;
using NUnit.Framework;
using VideoWorld.Controllers;
using VideoWorld.Models;

namespace UnitTests.Controllers
{
    public class HomePageControllerTests
    {
        [Test]
        public void ShouldShowIndexView()
        {
            var controller = new HomePageController();

            ViewResult result = controller.Index();
            Assert.That(result.ViewName, Is.EqualTo("Index"));

        }

        [Test]
        public void ViewShouldShowAListOfMovies()
        {
            var controller = new HomePageController();

            ViewResult result = controller.Index();
            var movies = (List<Movie>) result.Model;
            Assert.That(movies.Count, Is.EqualTo(3));
        }

    }
}
