using System;
using System.Web.Mvc;
using VideoWorld.Models;
using VideoWorld.Repositories;
using VideoWorld.ViewModels;

namespace VideoWorld.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IRentalRepository rentalRepository;
        private readonly TransactionRepository transactionRepository;

        public CheckoutController(ICustomerRepository customerRepository, IRentalRepository rentalRepository, TransactionRepository transactionRepository)
        {
            this.customerRepository = customerRepository;
            this.rentalRepository = rentalRepository;
            this.transactionRepository = transactionRepository;
        }

        [AcceptVerbs(HttpVerbs.Post), ActionName("Index")]
        public ViewResult CheckOut()
        {
            var customer = FindCustomer();
            rentalRepository.Add(customer.Cart.Rentals);
            var transaction = new Transaction(customer, DateTime.Now, customer.Cart.Rentals);
            transactionRepository.Add(transaction);
            var model = new StatementViewModel {Statement = customer.Statement(customer.Cart.Rentals), Customer = customer};
            customer.Cart.Clear();
            return View("Index", model);
        }

        private Customer FindCustomer()
        {
            var currentUsername = (string)Session["CurrentUser"];
            return customerRepository.SelectUnique(customer => customer.Username == currentUsername);
        }
    }
}