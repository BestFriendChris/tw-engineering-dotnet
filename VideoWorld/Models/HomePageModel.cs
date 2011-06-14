using System;
using System.Collections.Generic;
using VideoWorld.Utils;

namespace VideoWorld.Models
{
    public class HomePageModel
    {
        private readonly List<DetailedMovie> detailedMovies = new List<DetailedMovie>();

        public HomePageModel(IEnumerable<Movie> movies, Customer customer)
        {
            Movies = new List<Movie>(movies);
            Cart = customer.Cart;
            ShowAdminLink = customer.IsAdmin;
        }

        public List<Movie> Movies { get; private set; }

        public List<DetailedMovie> DetailedMovies
        {
            get
            {
                if (ShowDetailedMovies)
                {
                    Movies.ForEach(movie => detailedMovies.Add(movie as DetailedMovie));
                    return detailedMovies;
                }
                throw new NotSupportedException("Detailed Movies Feature is not enabled.");
            }
        }

        public Cart Cart { get; private set; }

        public bool ShowAdminLink { get; private set; }

        public bool ShowDetailedMovies { get { return Feature.DetailedMovies.IsEnabled(); } }
    }
}