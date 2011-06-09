using System;
using System.Collections.Generic;
using Ninject;
using VideoWorld.Utils;

namespace VideoWorld.Models
{
    public class Customer
    {
        private readonly Cart cart = new Cart();
        private int frequentRenterPoints = 0;

        public string DisplayName { get; private set; }
        public string Username { get; private set; }
        private string Password { get; set; }

        public Cart Cart { get { return cart; } }

        public bool IsAdmin { get; private set; }

        public Customer(string displayName, string username, string password) : this(displayName, username, password, false) { }

        [Inject]
        public Customer() : this ("Unknown Customer", null, null)
        {
        }

        private Customer(string displayName, string username, string password, bool isAdmin)
        {
            Password = password;
            IsAdmin = isAdmin;
            DisplayName = displayName;
            Username = username;
        }

        public string Statement(List<Rental> newRentals)
        {
            String result = "Rental Record for " + DisplayName + "\n";

            decimal totalAmount = 0.00m;
            foreach (Rental rental in newRentals)
            {
                // show figures for this rental
                int rentalDays = rental.Period.Duration.Days;

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

        public static Customer CreateAdminUser(string displayName, string username, string password)
        {
            if (!Feature.AdminAccount.IsEnabled())
                throw new NotSupportedException("Admin account feature is not enabled");
            return new Customer(displayName, username, password, true);
        }
    }
}