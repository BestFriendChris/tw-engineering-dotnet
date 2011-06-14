using System;
using System.Web.Mvc;
using VideoWorld.Models;
using VideoWorld.Repositories;
using VideoWorld.Utils;

namespace VideoWorld.Controllers
{
    public class CartController : BaseController
    {
        private readonly IMovieRepository movieRepository;

        public CartController(ICustomerRepository customerRepository, IMovieRepository movieRepository) : base(customerRepository)
        {
            this.movieRepository = movieRepository;
        }

        [AcceptVerbs(HttpVerbs.Post), ActionName("Index")]
        public RedirectResult RentMovie(string title, int numberOfDays)
        {
            var customer = LoggedInCustomer();
            Period periodInDays = Period.Of(DateTime.Now, new Duration(numberOfDays));
            var movie = movieRepository.ByTitle(title);
            customer.Cart.AddMovie(movie, periodInDays, customer);
            return Redirect("/");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Index()
        {
            Customer customer = LoggedInCustomer();
            return View("Index", customer.Cart);
        }
    }
}