using System;
using System.Web.Mvc;
using System.Web.SessionState;
using VideoWorld.Models;

namespace VideoWorld.Controllers
{
    public class LoginController : Controller
    {
        private const string USERNAME_EMPTY_ERROR = "Username is empty";
        private CustomerRepository customerRepository;

        public LoginController(CustomerRepository repository)
        {
            customerRepository = repository;
        }

        public ViewResult Index()
        {
            return View("Index");
        }

        [AcceptVerbs(HttpVerbs.Post), ActionName("Index")]
        public ActionResult Login(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                TempData["errorMessage"] = USERNAME_EMPTY_ERROR;
                return Index();
            }

            var customer = customerRepository.FindByName(username);
            if (customer == null)
            {
                customer = new Customer(username, null, null);
                customerRepository.Add(customer);
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