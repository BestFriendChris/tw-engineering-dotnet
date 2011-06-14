using System;
using System.Collections.Generic;
using System.Linq;
using VideoWorld.Models;

namespace VideoWorld.Repositories
{
    public class RentalRepository : BaseRepository<Rental>, IRentalRepository
    {
        public List<Rental> AllRentalsFor(Customer customer)
        {
            return Select(rental => rental.Customer.Equals(customer));
        }

        public List<Rental> CurrentRentalsFor(Customer customer)
        {
            return AllRentalsFor(customer).Where(EndDateBeforeNow()).ToList();
        }

        private static Func<Rental, bool> EndDateBeforeNow()
        {
            return rental => rental.Period.EndDate.CompareTo(DateTime.Now) > 0;
        }
    }
}