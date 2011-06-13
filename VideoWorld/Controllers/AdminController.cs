using System;
using System.Web.Mvc;
using VideoWorld.Models;
using VideoWorld.Repositories;
using VideoWorld.ViewModels;

namespace VideoWorld.Controllers
{
    public class AdminController : Controller
    {
        private const string PASSWORD_MATCH_ERROR = "Passwords must match";
        private const string USER_EXISTS_ERROR = "Customer with username '{0}' already exists.";
        private const string MOVIE_EXISTS_ERROR = "Movie '{0}' already exists.";
        private const string MANDATORY_FIELDS_ERROR = "Must enter all fields.";
        private readonly ICustomerRepository customerRepository;
        private readonly IMovieRepository movieRepository;

        public AdminController(ICustomerRepository customerRepository, IMovieRepository movieRepository)
        {
            this.customerRepository = customerRepository;
            this.movieRepository = movieRepository;
        }

        public ViewResult Index()
        {
            return View("Index", customerRepository.SelectAllInAlphabeticalOrder());
        }

        [AcceptVerbs(HttpVerbs.Post), ActionName("NewCustomer")]
         public ActionResult AddNewCustomer(NewCustomerViewModel model)
         {
            if(model.AllFieldsNotPopulated())
            {
                model.PopulateWithError(MANDATORY_FIELDS_ERROR);
            }

            if (!model.PasswordsMatch())
            {
                 model.PopulateWithError(PASSWORD_MATCH_ERROR);
            }

            if(customerRepository.ContainsUsername(model.Username))
            {
                model.PopulateWithError(string.Format(USER_EXISTS_ERROR, model.Username));
            }

            if (model.ErrorMessage == null)
            {
                var customerToBeAdded = new Customer(model.DisplayName, model.Username, model.Password1);
                customerRepository.Add(customerToBeAdded);
                return Index();
            }

            return AddCustomer(model);
         }

        public ViewResult AddCustomer(NewCustomerViewModel model)
        {
            return View("AddCustomer", model);
        }

        [AcceptVerbs(HttpVerbs.Post), ActionName("NewMovie")]
        public ActionResult AddNewMovie(NewMovieViewModel model)
        {
            if (string.IsNullOrEmpty(model.Title))
            {
                model.ErrorMessage = "Must enter all fields.";
            }

            if (movieRepository.HasMovieByTitle(model.Title))
            {
                model.ErrorMessage = string.Format(MOVIE_EXISTS_ERROR, model.Title);
            }

            if (model.ErrorMessage == null)
            {
                movieRepository.Add(new Movie(model.Title, new NewReleasePrice()));
                return Index();
            }

            return AddMovie(model);
        }

        public ViewResult AddMovie(NewMovieViewModel model)
        {
            return View("AddMovie", model);
        }
    }

}