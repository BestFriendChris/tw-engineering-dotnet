using System.Collections.Generic;
using VideoWorld.Models;

namespace VideoWorld.ViewModels
{
    public class CurrentRentalsModel
    {
        public Customer CurrentCustomer { get; set; }
        public List<Rental> CurrentRentals { get; set; }
    }
}