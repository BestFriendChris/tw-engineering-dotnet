using System.Collections.Generic;
using VideoWorld.Models;

namespace VideoWorld.Repositories
{
    public class CustomerRepository : ListBasedRepository<Customer>, ICustomerRepository
    {
        public bool ContainsUsername(string username)
        {
            return SelectSatisfying(CustomerSpecification.ByUserName(username)).Count > 0;
        }

    }
}