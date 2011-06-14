using System.Collections.Generic;
using System.Web.Mvc;
using VideoWorld.Models;
using VideoWorld.Repositories;
using VideoWorld.Utils;
using VideoWorld.ViewModels;
using System.Linq;

namespace VideoWorld.Controllers
{
    [IsAdmin]
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
            return View("Index", GetCustomers());
        }

        private List<Customer> GetCustomers()
        {
            return customerRepository.SelectAll().OrderBy(customer => customer.Username).ToList();
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
            if (model.AllFieldsNotPopulated())
            {
                model.ErrorMessage = "Must enter all fields.";
            }

            if (movieRepository.HasMovieByTitle(model.Title))
            {
                model.ErrorMessage = string.Format(MOVIE_EXISTS_ERROR, model.Title);
            }

            if (model.ErrorMessage == null)
            {
                Movie movieToBeAdded = GetMovieToBeAdded(model);
                movieRepository.Add(movieToBeAdded);
                return Index();
            }

            return AddMovie(model);
        }

        private static Movie GetMovieToBeAdded(NewMovieViewModel model)
        {
            return Feature.DetailedMovies.IsEnabled()
                       ? new DetailedMovie(model.Title, new NewReleasePrice(), model.Director, model.Actor, model.Actress, model.Category)
                       : new Movie(model.Title, new NewReleasePrice());
        }

        public ViewResult AddMovie(NewMovieViewModel model)
        {
            return View("AddMovie", model);
        }
    }
}