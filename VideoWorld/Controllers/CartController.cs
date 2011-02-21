using System;
using System.Web.Mvc;
using VideoWorld.Models;

namespace VideoWorld.Controllers
{
    public class CartController : Controller
    {
        private readonly Customer customer;

        public CartController(Customer customer)
        {
            this.customer = customer;
        }

        public RedirectResult Index(string title)
        {
            customer.Cart.AddMovie(new Movie(title));
            return Redirect("/");
        }
    }
}