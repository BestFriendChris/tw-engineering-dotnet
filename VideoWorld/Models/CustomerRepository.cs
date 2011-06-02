using System;
using System.Collections.Generic;

namespace VideoWorld.Models
{
    public class CustomerRepository
    {
        private Dictionary<string, Customer> customers = new Dictionary<string, Customer>();
        //public Customer 
        public Customer FindByName(string customerName)
        {
            Customer customer;
            if (customers.TryGetValue(customerName, out customer))
            {
                return customer;
            }

            return null;
        }

        public void Add(Customer customer)
        {
            this.customers.Add(customer.DisplayName, customer);
        }
    }
}