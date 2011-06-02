using System.Collections.Generic;
using VideoWorld.Models;

namespace VideoWorld.Repositories
{
    public class ListBasedCustomerRepository : ListBasedRepository<Customer>, ICustomerRepository
    {
        public IList<Customer> SelectAll()
        {
            return base.SelectAll();
        }

        public IList<Customer> SelectAll(Comparer<Customer> orderBy)
        {
            return base.SelectAll(orderBy);
        }

        public IList<Customer> SelectSatisfying(Specification<Customer> specification)
        {
            return base.SelectSatisfying(specification);
        }

        public IList<Customer> SelectSatisfying(Specification<Customer> specification, Comparer<Customer> comparator)
        {
            return base.SelectSatisfying(specification, comparator);
        }

        public Customer SelectUnique(Specification<Customer> specification)
        {
            return base.SelectUnique(specification);
        }

        public bool ContainsUsername(string username)
        {
            return SelectSatisfying(customer => customer.Username.Equals(username)).Count > 0;
        }
    }
}