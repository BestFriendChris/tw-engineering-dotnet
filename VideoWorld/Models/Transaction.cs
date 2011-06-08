using System;
using System.Collections.Generic;

namespace VideoWorld.Models
{
    public class Transaction
    {
        public Transaction(Customer customer, DateTime dateTime, IEnumerable<Rental> rentals)
        {
            Customer = customer;
            DateTime = dateTime;
            foreach (var rental in rentals)
            {
                if(!rental.Customer.Equals(customer))
                {
                    throw new ArgumentException("Rentals must be for the same customer");
                }
            }
            Rentals = new List<Rental>(rentals);
        }

        public Customer Customer { get; private set; }
        
        public DateTime DateTime { get; set; }

        public List<Rental> Rentals { get; private set; }
    }
}