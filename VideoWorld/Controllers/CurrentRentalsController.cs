using System.Collections.Generic;
using System.Web.Mvc;
using VideoWorld.Models;
using VideoWorld.Repositories;
using VideoWorld.ViewModels;

namespace VideoWorld.Controllers
{
    public class CurrentRentalsController : BaseController
    {
        private readonly IRentalRepository rentalRepository;

        public CurrentRentalsController(ICustomerRepository customerRepository, IRentalRepository rentalRepository) : base(customerRepository)
        {
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
                                              CurrentCustomer = LoggedInCustomer(),
                                              CurrentRentals = rentalRepository.CurrentRentalsFor(LoggedInCustomer())
                                          };
            return currentRentalsModel;
        }
    }
}