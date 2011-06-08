using System;
using System.Collections.Generic;
using System.Linq;
using VideoWorld.Repositories;

namespace VideoWorld.Models
{
    public class RentalRepository : ListBasedRepository<Rental>, IRentalRepository
    {
        public List<Rental> RentalsFor(Customer customer)
        {
            List<Rental> rentals = SelectSatisfying(RentalSpecification.ByCustomer(customer));
            List<Rental> rentalsFor = rentals.Where(EndDateBeforeNow()).ToList();
            return rentalsFor;
        }

        private static Func<Rental, bool> EndDateBeforeNow()
        {
            return rental => rental.Period.EndDate.CompareTo(DateTime.Now) > 0;
        }
    }
}