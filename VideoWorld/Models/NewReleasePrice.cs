using System;

namespace VideoWorld.Models
{
    public class NewReleasePrice : IPrice
    {
        public decimal GetCharge(int daysRented)
        {
            return daysRented*3.00m;
        }

        public int GetFrequentRenterPoints(int daysRented)
        {
            // add bonus for a two day new release rental
            if (daysRented > 1)
                return 2;
            else
                return 1;
        }

        public string DisplayName
        {
            get { return "New Release"; }
        }
    }
}