using VideoWorld.Models;

namespace VideoWorld.Repositories
{
    public class ListBasedCustomerRepository : ListBasedRepository<Customer>, ICustomerRepository
    {
        public bool ContainsUsername(string username)
        {
            return SelectSatisfying(CustomerSpecification.ByUserName(username)).Count > 0;
        }
    }
}