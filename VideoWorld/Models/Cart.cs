using System;
using System.Collections.Generic;

namespace VideoWorld.Models
{
    public class Cart
    {
        private readonly List<Movie> movies = new List<Movie>();

        public bool Contains(Movie movie)
        {
            return movies.Contains(movie);
        }

        public int Count
        {
            get { return movies.Count; }
        }

        public void AddMovie(Movie movie)
        {
            movies.Add(movie);
        }
    }
}