using System.Collections.Generic;
using VideoWorld.Repositories;

namespace VideoWorld.Models
{
    public class RentalRepository : ListBasedRepository<Rental>, IRentalRepository
    {
        public List<Rental> RentalForCustomer(Customer customer)
        {
            return SelectSatisfying(RentalSpecification.ByCustomer(customer));
        }
    }
}