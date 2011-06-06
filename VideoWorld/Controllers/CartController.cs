using System;
using System.Web.Mvc;
using VideoWorld.Models;
using VideoWorld.Repositories;

namespace VideoWorld.Controllers
{
    public class CartController : Controller
    {
        private readonly ICustomerRepository customerRepository;

        public CartController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [AcceptVerbs(HttpVerbs.Post), ActionName("Index")]
        public RedirectResult RentMovie(string title, int numberOfDays)
        {
            var customer = FindCustomer();
            customer.Cart.AddMovie(new Movie(title, new RegularPrice()), new Period(numberOfDays), customer);
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
            var currentUsername = (string)Session["CurrentUser"];
            return customerRepository.SelectUnique(CustomerSpecification.ByUserName(currentUsername));
        }
    }
}