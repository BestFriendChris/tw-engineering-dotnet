using System;
using System.Web.Mvc;
using VideoWorld.Models;

namespace VideoWorld.Controllers
{
    public class CartController : Controller
    {
        private readonly CustomerRepository customerRepository;

        public CartController(CustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [AcceptVerbs(HttpVerbs.Post), ActionName("Index")]
        public RedirectResult RentMovie(string title)
        {
            var customer = FindCustomer();
            customer.Cart.AddMovie(new Movie(title, new RegularPrice()));
            return Redirect("/");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Index()
        {
            Customer customer = FindCustomer();
            return View("Index", customer.Cart);
        }

        private Customer FindCustomer()
        {
            return customerRepository.FindByName((string)Session["CurrentUser"]);
        }
    }
}