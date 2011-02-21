using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using UnitTests.Models;
using VideoWorld.Controllers;
using VideoWorld.Models;

namespace UnitTests.Controllers
{
    class CartControllerTests
    {
        [Test]
        public void ShouldredirectToHomePageWhenAMovieIsAdded()
        {
            var controller = new CartController(new Customer());

            RedirectResult result = controller.Index("Avatar");
            Assert.That(result.Url, Is.EqualTo("/"));
        }

        [Test]
        public void ShouldAddMovieToCart()
        {
            var customer = new Customer();
            var controller = new CartController(customer);
            controller.Index("Avatar");
            Assert.That(customer.Cart.Contains(new Movie("Avatar")));
        }

        [Test]
        public void ShouldCountMultipleMovies()
        {
            var customer = new Customer();
            var controller = new CartController(customer);
            controller.Index("Avatar");
            Assert.That(customer.Cart.Count, Is.EqualTo(1));
            controller.Index("Waterworld");
            Assert.That(customer.Cart.Count, Is.EqualTo(2));
        }
    }
}
