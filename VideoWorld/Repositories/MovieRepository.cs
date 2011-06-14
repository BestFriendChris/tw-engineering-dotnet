using VideoWorld.Models;

namespace VideoWorld.Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public Movie ByTitle(string title)
        {
            return SelectUnique(movie => movie.Title == title);
        }

        public bool HasMovieByTitle(string title)
        {
            return Select(movie => movie.Title == title).Count > 0;
        }
    }
}