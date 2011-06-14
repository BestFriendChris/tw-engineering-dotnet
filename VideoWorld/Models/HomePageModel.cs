using System;
using System.Collections.Generic;
using VideoWorld.Utils;

namespace VideoWorld.Models
{
    public class HomePageModel
    {
        public HomePageModel(List<Movie> movies, Customer customer)
        {
            Movies = new List<Movie>(movies);
            Cart = customer.Cart;
            ShowAdminLink = customer.IsAdmin;
        }

        public List<Movie> Movies { get; private set; }

        public List<DetailedMovie> DetailedMovies {get { if(ShowDetailedMovies) Movies.ForEach(movie => DetailedMovies.Add((DetailedMovie) movie));
            return DetailedMovies;
        }
        }

        public Cart Cart { get; private set; }

        public bool ShowAdminLink { get; private set; }

        public bool ShowDetailedMovies { get { return Feature.DetailedMovies.IsEnabled(); } }
    }
}