using System;
using System.Web.Mvc;
using VideoWorld.Models;
using VideoWorld.Repositories;
using VideoWorld.ViewModels;

namespace VideoWorld.Controllers
{
    public class CurrentRentalsController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IRentalRepository rentalRepository;

        public CurrentRentalsController(ICustomerRepository customerRepository, IRentalRepository rentalRepository)
        {
            this.customerRepository = customerRepository;
            this.rentalRepository = rentalRepository;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Index()
        {
            return View("Index", GetCurrentRentalsModel());
        }

        private CurrentRentalsModel GetCurrentRentalsModel()
        {
            var currentRentalsModel = new CurrentRentalsModel
                                          {
                                              CurrentCustomer = GetCurrentCustomer(),
                                              CurrentRentals = rentalRepository.RentalForCustomer(GetCurrentCustomer())
                                          };
            return currentRentalsModel;
        }

        private Customer GetCurrentCustomer()
        {
            var customerName = (String) Session["CurrentUser"];
            return customerRepository.SelectUnique(CustomerSpecification.ByUserName(customerName));
        }
    }
}