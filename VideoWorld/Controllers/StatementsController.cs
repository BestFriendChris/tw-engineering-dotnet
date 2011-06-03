using System;
using System.Web.Mvc;
using VideoWorld.Models;
using VideoWorld.Repositories;

namespace VideoWorld.Controllers
{
    public class StatementsController : Controller
    {
        private readonly StatementRepository statementRepository;
        private readonly ICustomerRepository customerRepository;

        public StatementsController(StatementRepository statementRepository, ICustomerRepository customerRepository)
        {
            this.statementRepository = statementRepository;
            this.customerRepository = customerRepository;
        }

        [AcceptVerbs(HttpVerbs.Post), ActionName("Index")]
        public RedirectResult Create()
        {
            Customer customer = FindCustomer();
            var statement = new Statement(customer);
            int id = statementRepository.Add(statement);
            customer.Cart.Clear();
            return Redirect("/statements/" + id);
        }

        private Customer FindCustomer()
        {
            var currentUsername = (string) Session["CurrentUser"];
            return customerRepository.SelectUnique(customer => customer.Username.Equals(currentUsername));
        }

        public ViewResult Show(int id)
        {
            var statement = statementRepository.FindById(id);
            return View("Show", statement);
        }

        public ViewResult Index()
        {
            return View("Index", statementRepository.FindByCustomer(FindCustomer()));
        }
    }
}