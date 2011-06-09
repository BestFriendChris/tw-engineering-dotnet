using VideoWorld.Models;

namespace VideoWorld.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Movie ByTitle(string title);
    }
}