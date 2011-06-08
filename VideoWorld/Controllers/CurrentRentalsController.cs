using System;
using System.Collections.Generic;
using System.Web.Mvc;
using VideoWorld.Models;
using VideoWorld.Repositories;
using VideoWorld.ViewModels;
using System.Linq;

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
                                              CurrentRentals = GetCurrentRentals()
                                          };
            return currentRentalsModel;
        }

        private List<Rental> GetCurrentRentals()
        {
            return rentalRepository.RentalsFor(GetCurrentCustomer());
        }

        private Customer GetCurrentCustomer()
        {
            var customerName = (String) Session["CurrentUser"];
            return customerRepository.SelectUnique(CustomerSpecification.ByUserName(customerName));
        }
    }
}