using System.Web;
using System.Web.Mvc;
using VideoWorld.Repositories;

namespace VideoWorld.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICustomerRepository customerRepository;

        public AdminController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public ViewResult Index()
        {
            return View("Index", customerRepository.SelectAllInAlphabeticalOrder());
        }
    }
}