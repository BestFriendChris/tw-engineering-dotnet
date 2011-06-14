using System;
using System.Web.Mvc;
using VideoWorld.Repositories;

namespace VideoWorld.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly TransactionRepository transactionRepository;

        public TransactionController(TransactionRepository transactionRepository, ICustomerRepository customerRepository) : base(customerRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Index()   
        {
            return View("Index", transactionRepository.TransactionsBy(LoggedInCustomer()));
        }
    }
}