using VideoWorld.Models;

namespace VideoWorld.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        bool ContainsUsername(string username);
    }
}