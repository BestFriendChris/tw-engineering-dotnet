using System;
using System.Collections.Generic;
using VideoWorld.Utils;

namespace VideoWorld.Models
{
    public class Cart
    {
        private readonly List<Rental> rentals = new List<Rental>();

        public bool Contains(Rental movie)
        {
            return rentals.Contains(movie);
        }

        public int Count
        {
            get { return rentals.Count; }
        }

        public List<Rental> Rentals
        {
            get {
                return rentals;
            }
        }

        public void AddMovie(Movie movie, Period periodInDays, Customer customer)
        {
            rentals.Add(new Rental(movie, periodInDays,customer));
        }

        public void Clear()
        {
            rentals.Clear();
        }
    }
}