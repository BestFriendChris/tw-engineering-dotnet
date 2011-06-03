using System;
using System.Collections.Generic;
using System.Web.Mvc;
using VideoWorld.Models;
using VideoWorld.Repositories;

namespace VideoWorld.Controllers
{
    public class HomePageController : Controller
    {
        private readonly ICustomerRepository customers;

        public HomePageController(ICustomerRepository customers)
        {
            this.customers = customers;
        }

        public ViewResult Index()
        {
            var movieRepo = new MovieRepository();
            List<Movie> movies = movieRepo.FindAllMovies();

            var currentUsername = (string) Session["CurrentUser"];

            return View("Index", new HomePageModel(movies, customers.SelectUnique(CustomerSpecification.ByUserName(currentUsername))));
        }
    }
}