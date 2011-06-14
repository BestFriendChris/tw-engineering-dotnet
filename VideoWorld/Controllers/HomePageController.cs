using System;
using System.Collections.Generic;
using System.Web.Mvc;
using VideoWorld.Models;
using VideoWorld.Repositories;

namespace VideoWorld.Controllers
{
    public class HomePageController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMovieRepository movieRepository;

        public HomePageController(ICustomerRepository customerRepository, IMovieRepository movieRepository)
        {
            this.customerRepository = customerRepository;
            this.movieRepository = movieRepository;
        }

        public ViewResult Index()
        {
            var movies = movieRepository.SelectAll();

            var currentUsername = (string) Session["CurrentUser"];

            return View("Index", new HomePageModel(movies, customerRepository.SelectUnique(customer => customer.Username == currentUsername)));
        }
    }
}