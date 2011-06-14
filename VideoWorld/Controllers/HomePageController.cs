using System;
using System.Collections.Generic;
using System.Web.Mvc;
using VideoWorld.Models;
using VideoWorld.Repositories;
using VideoWorld.Utils;

namespace VideoWorld.Controllers
{
    public class HomePageController : BaseController
    {
        private readonly IMovieRepository movieRepository;

        public HomePageController(ICustomerRepository customerRepository, IMovieRepository movieRepository) : base(customerRepository)
        {
            this.movieRepository = movieRepository;
        }

        public ViewResult Index()
        {
            var movies = movieRepository.SelectAll();
            return View("Index", new HomePageModel(movies, LoggedInCustomer()));
        }
    }
}