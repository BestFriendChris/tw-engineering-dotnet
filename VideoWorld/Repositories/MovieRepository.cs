using System;
using System.Collections.Generic;
using VideoWorld.Models;

namespace VideoWorld.Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public Movie ByTitle(string title)
        {
            return SelectUnique(MovieSpecification.ByTitle(title));
        }
    }
}