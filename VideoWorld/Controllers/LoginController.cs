using System;
using System.Web.Mvc;
using System.Web.SessionState;
using VideoWorld.Models;

namespace VideoWorld.Controllers
{
    public class LoginController : Controller
    {
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
        public RedirectResult Login(string username)
        {
            Customer customer = customerRepository.FindByName(username);
            if (customer == null)
            {
                customer = new Customer(username);
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