using System;
using System.Web.Mvc;
using VideoWorld.Models;
using VideoWorld.Repositories;

namespace VideoWorld.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionRepository transactionRepository;
        private readonly ICustomerRepository customerRepository;

        public TransactionController(TransactionRepository transactionRepository, ICustomerRepository customerRepository)
        {
            this.transactionRepository = transactionRepository;
            this.customerRepository = customerRepository;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Index()   
        {
            return View("Index", transactionRepository.TransactionsBy(FindCustomer()));
        }

        private Customer FindCustomer()
        {
            var currentUsername = (string)Session["CurrentUser"];
            return customerRepository.SelectUnique(CustomerSpecification.ByUserName(currentUsername));
        }
    }
}