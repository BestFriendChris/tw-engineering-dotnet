using System;
using System.Collections.Generic;
using VideoWorld.Models;

namespace VideoWorld.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public bool ContainsUsername(string username)
        {
            return SelectSatisfying(CustomerSpecification.ByUserName(username)).Count > 0;
        }

        public List<Customer> SelectAllInAlphabeticalOrder()
        {
            return SelectAll(new CustomerComparator());
        }
    }

    public class CustomerComparator : Comparer<Customer>
    {
        public override int Compare(Customer customer1, Customer customer2)
        {
            return customer1 == customer2 ? 0 : customer1.Username.CompareTo(customer2.Username);
        }
    }
}