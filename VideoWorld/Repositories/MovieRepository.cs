using VideoWorld.Models;

namespace VideoWorld.Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public Movie ByTitle(string title)
        {
            return SelectUnique(MovieSpecification.ByTitle(title));
        }

        public bool HasMovieByTitle(string title)
        {
            return SelectSatisfying(MovieSpecification.ByTitle(title)).Count > 0;
        }
    }
}