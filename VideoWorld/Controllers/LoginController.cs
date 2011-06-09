using System;
using System.Web.Mvc;
using System.Web.SessionState;
using VideoWorld.Models;
using VideoWorld.Repositories;
using VideoWorld.ViewModels;

namespace VideoWorld.Controllers
{
    public class LoginController : Controller
    {
        private const string USERNAME_EMPTY_ERROR = "Username cannot be empty";
        private const string INVALID_USERNAME_PASSWORD_ERROR = "Invalid username/password";
        private readonly ICustomerRepository customerRepository;

        public LoginController(ICustomerRepository repository)
        {
            customerRepository = repository;
        }

        public ViewResult Index()
        {
            return LoginView(new LoginViewModel { AllCustomers = customerRepository.SelectAllInAlphabeticalOrder()});
        }

        private ViewResult LoginView(LoginViewModel model)
        {
            return View("Index", model);
        }

        [AcceptVerbs(HttpVerbs.Post), ActionName("Index")]
        public ActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                var loginViewModel = new LoginViewModel
                                         {
                                             Username = username,
                                             ErrorMessage = USERNAME_EMPTY_ERROR,
                                             AllCustomers = customerRepository.SelectAllInAlphabeticalOrder()
                                         };
                return LoginView(loginViewModel);
            }

            var customer = customerRepository.SelectUnique(CustomerSpecification.ByUserNameAndPassword(username, password) );

            if (customer == null)
            {
                var loginViewModel = new LoginViewModel
                                         {
                                             Username = username,
                                             ErrorMessage = INVALID_USERNAME_PASSWORD_ERROR,
                                             AllCustomers = customerRepository.SelectAllInAlphabeticalOrder()
                                         };
                return LoginView(loginViewModel);
            }

            customer.Cart.Clear();

            Session["CurrentUser"] = username;
            return Redirect("/");
        }

        public RedirectResult Logoff()
        {
            Session.Remove("CurrentUser");
            return Redirect("/login");
        }
    }
}