using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ninject.Web.Mvc;
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
                var movieToBeAdded = new DetailedMovie(model.Title, new NewReleasePrice(), model.Director, model.Actor, model.Actress, model.Category);
                movieRepository.Add(movieToBeAdded);
                return Index();
            }

            return AddMovie(model);
        }

        public ViewResult AddMovie(NewMovieViewModel model)
        {
            return View("AddMovie", model);
        }
    }

    public class IsAdminAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var loggedInUser = (string)filterContext.HttpContext.Session["CurrentUser"];
            var customerRepository = ((NinjectHttpApplication)filterContext.HttpContext.ApplicationInstance).Kernel.Get(typeof(ICustomerRepository)) as CustomerRepository;
            var customer = customerRepository.SelectUnique(cust => cust.Username == loggedInUser);

            if(Feature.AdminAccount.IsEnabled() && customer != null && customer.IsAdmin)
               return;
            filterContext.Result = new RedirectToRouteResult(
              new RouteValueDictionary
                    {
                        {"controller", "Error"},
                        {"action", "Index"}
                    });
        }
    }
}