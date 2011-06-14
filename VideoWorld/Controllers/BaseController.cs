using System.Web.Mvc;
using VideoWorld.Models;
using VideoWorld.Repositories;

namespace VideoWorld.Controllers
{
    public class BaseController : Controller
    {
        private readonly ICustomerRepository customerRepository;

        public BaseController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public Customer LoggedInCustomer()
        {
            var currentUsername = (string)Session["CurrentUser"];
            return customerRepository.SelectUnique(customer => customer.Username == currentUsername);
        }
    }
}