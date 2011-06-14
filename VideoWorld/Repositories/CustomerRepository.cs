using System;
using System.Collections.Generic;
using VideoWorld.Models;
using System.Linq;

namespace VideoWorld.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public bool ContainsUsername(string username)
        {
            return Select(customer => customer.Username == username).Count > 0;
        }

        public List<Customer> SelectAllInAlphabeticalOrder()
        {
            return SelectAll().OrderBy(customer => customer.Username).ToList();
        }
    }
}