using System;
using System.Collections.Generic;
using System.Web.Mvc;
using VideoWorld.Models;

namespace VideoWorld.Controllers
{
    public class HomePageController : Controller
    {
        private readonly CustomerRepository customers;

        public HomePageController(CustomerRepository customers)
        {
            this.customers = customers;
        }

        public ViewResult Index()
        {
            var movieRepo = new MovieRepository();
            List<Movie> movies = movieRepo.FindAllMovies();

            string customerName = (string) Session["CurrentUser"];

            return View("Index", new HomePageModel(movies, customers.FindByName(customerName)));
        }
    }
}