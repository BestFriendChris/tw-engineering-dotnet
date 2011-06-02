using System.Collections.Generic;
using System.Collections.ObjectModel;
using VideoWorld.Models;

namespace VideoWorld.Repositories
{
    public interface ICustomerRepository
    {
        void Add(Customer entity);

        void Add(Collection<Customer> entities);

        IList<Customer> SelectAll();

        IList<Customer> SelectAll(Comparer<Customer> orderBy);

        IList<Customer> SelectSatisfying(Specification<Customer> specification);

        IList<Customer> SelectSatisfying(Specification<Customer> specification, Comparer<Customer> comparator);

        Customer SelectUnique(Specification<Customer> specification);

        bool ContainsUsername(string username);
    }
}