using System;
using System.Web.Mvc;
using VideoWorld.Models;
using VideoWorld.Repositories;
using VideoWorld.Utils;

namespace VideoWorld.Controllers
{
    public class CartController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMovieRepository movieRepository;

        public CartController(ICustomerRepository customerRepository, IMovieRepository movieRepository)
        {
            this.customerRepository = customerRepository;
            this.movieRepository = movieRepository;
        }

        [AcceptVerbs(HttpVerbs.Post), ActionName("Index")]
        public RedirectResult RentMovie(string title, int numberOfDays)
        {
            var customer = FindCustomer();
            Period periodInDays = Period.Of(DateTime.Now, new Duration(numberOfDays));
            var movie = movieRepository.ByTitle(title);
            customer.Cart.AddMovie(movie, periodInDays, customer);
            return Redirect("/");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Index()
        {
            Customer customer = FindCustomer();
            return View("Index", customer.Cart);
        }

        private Customer FindCustomer()
        {
            var currentUsername = (string)Session["CurrentUser"];
            return customerRepository.SelectUnique(customer => customer.Username == currentUsername);
        }
    }
}