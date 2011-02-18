using System.Collections.Generic;

namespace VideoWorld.Models
{
    public class MovieRepository
    {
        public List<Movie> FindAllMovies()
        {
            return new List<Movie>()
                       {
                           new Movie("Avatar"),
                           new Movie("Up in the Air"),
                           new Movie("Finding Nemo")
                       };
        }
    }
}