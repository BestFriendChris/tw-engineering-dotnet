using System;
using System.Collections.Generic;
using Ninject;

namespace VideoWorld.Models
{
    public class Customer
    {
        private readonly Cart cart = new Cart();
        private int frequentRenterPoints = 0;

        public Customer(string displayName, string username, string password)
        {
            Password = password;
            DisplayName = displayName;
            Username = username;
        }

        [Inject]
        public Customer() : this ("Unknown Customer", null, null)
        {
        }

        public Cart Cart
        {
            get { return  cart; }
        }

        public string DisplayName { get; private set; }
        public string Username { get; private set; }
        private string Password { get; set; }

        public string Statement(List<Rental> newRentals)
        {
            String result = "Rental Record for " + DisplayName + "\n";

            decimal totalAmount = 0.00m;
            foreach (Rental rental in newRentals)
            {
                // show figures for this rental
                int rentalDays = rental.Period.Duration;

                result += "  " + rental.Movie.Title + "  -  $"
                          + rental.Movie.Price.GetCharge(rentalDays) + "\n";

                totalAmount += rental.Movie.Price.GetCharge(rentalDays);

                frequentRenterPoints += rental.Movie.Price.GetFrequentRenterPoints(rentalDays);
            }

            // add footer lines
            result += "Amount charged is $" + totalAmount + "\n";
            result += "You have a new total of " + frequentRenterPoints + " frequent renter points";
            return result;
        }

        public bool IsUsernameAndPasswordValid(string username, string password)
        {
            return Username.Equals(username) && Password.Equals(password);
        }
    }
}