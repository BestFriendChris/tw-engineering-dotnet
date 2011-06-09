using System;
using System.Collections.Generic;

namespace VideoWorld.Models
{
    public class HomePageModel
    {
        public HomePageModel(IEnumerable<Movie> movies, Customer customer)
        {
            Movies = new List<Movie>(movies);
            Cart = customer.Cart;
            ShowAdminLink = customer.IsAdmin;
        }

        public List<Movie> Movies { get; private set; }

        public Cart Cart { get; private set; }

        public bool ShowAdminLink { get; private set; }
    }
}