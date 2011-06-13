using System.Web.Mvc;
using VideoWorld.Models;
using VideoWorld.Repositories;
using VideoWorld.ViewModels;

namespace VideoWorld.Controllers
{
    public class AdminController : Controller
    {
        private const string PASSWORD_MATCH_ERROR = "Passwords must match";
        private const string USERNAME_EXISTS_ERROR = "Customer with username {0} already exists";
        private readonly ICustomerRepository customerRepository;

        public AdminController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public ViewResult Index()
        {
            return View("Index", customerRepository.SelectAllInAlphabeticalOrder());
        }

        [AcceptVerbs(HttpVerbs.Post), ActionName("NewCustomer")]
         public ActionResult AddNewCustomer(NewCustomerViewModel model)
         {
             if (!model.PasswordsMatch())
             {
                 model.PopulateWithError(PASSWORD_MATCH_ERROR);
                 return AddCustomer(model);
             }

            if(customerRepository.ContainsUsername(model.Username))
            {
                model.PopulateWithError(string.Format(USERNAME_EXISTS_ERROR, model.Username));
                return AddCustomer(model);
            }

            var customerToBeAdded = new Customer(model.DisplayName,model.Username,model.Password1);
            customerRepository.Add(customerToBeAdded);
            return Index();
         }

        public ViewResult AddCustomer(NewCustomerViewModel model)
        {
            return View("AddCustomer", model);
        }
    }

}