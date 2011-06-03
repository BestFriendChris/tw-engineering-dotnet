using System.Collections.Generic;
using VideoWorld.Models;

namespace VideoWorld.Repositories
{
    public class ListBasedCustomerRepository : ListBasedRepository<Customer>, ICustomerRepository
    {
        public bool ContainsUsername(string username)
        {
            return SelectSatisfying(customer => customer.Username.Equals(username)).Count > 0;
        }
    }
}