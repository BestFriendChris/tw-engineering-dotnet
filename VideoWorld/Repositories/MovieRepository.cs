using System;
using System.Collections.Generic;
using VideoWorld.Models;

namespace VideoWorld.Repositories
{
    public class MovieRepository : ListBasedRepository<Movie>, IMovieRepository
    {
        public Movie ByTitle(string title)
        {
            return SelectUnique(MovieSpecification.ByTitle(title));
        }
    }
}