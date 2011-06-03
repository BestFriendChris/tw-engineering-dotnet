using System.Collections.Generic;
using VideoWorld.Models;

namespace VideoWorld.Repositories
{
    public interface ICustomerRepository
    {
        void Add(Customer entity);

        void Add(IList<Customer> entities);

        List<Customer> SelectAll();

        List<Customer> SelectAll(Comparer<Customer> orderBy);

        List<Customer> SelectSatisfying(Specification<Customer> specification);

        List<Customer> SelectSatisfying(Specification<Customer> specification, Comparer<Customer> comparator);

        Customer SelectUnique(Specification<Customer> specification);

        bool ContainsUsername(string username);
    }
}