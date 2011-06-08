using System;
using VideoWorld.Utils;

namespace VideoWorld.Models
{
    public class Rental 
    {
        private readonly Movie movie;
        private readonly Period periodInDays;
        private readonly Customer customer;

        public Rental(Movie movie, Period periodInDays, Customer customer)
        {
            this.movie = movie;
            this.periodInDays = periodInDays;
            this.customer = customer;
        }


        public Movie Movie
        {
            get {
                return movie;
            }
        }

        public Period Period
        {
            get { return periodInDays; }
        }

        public Customer Customer
        {
            get { return customer; }
        }
    }
}