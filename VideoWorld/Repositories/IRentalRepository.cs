using System.Collections.Generic;
using VideoWorld.Models;

namespace VideoWorld.Repositories
{
    public interface IRentalRepository : IRepository<Rental>
    {
        List<Rental> RentalForCustomer(Customer customer);
    }
}