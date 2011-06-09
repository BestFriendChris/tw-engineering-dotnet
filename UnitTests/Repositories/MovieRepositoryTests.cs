using NUnit.Framework;
using VideoWorld.Models;
using VideoWorld.Repositories;

namespace UnitTests.Repositories
{
    public class MovieRepositoryTests
    {
        private IMovieRepository repository;
        private readonly Movie movieXMen = new Movie("X Men", new NewReleasePrice());
        private readonly Movie movieBadBoys2 = new Movie("Bad Boys 2", new RegularPrice());

        [SetUp]
        public void SetUp()
        {
            repository = new MovieRepository();
            repository.Add(movieXMen);
            repository.Add(movieBadBoys2);
        }

        [Test]
        public void ShouldHaveThreeMovies()
        {
            
            var movies = repository.SelectAll();
            Assert.AreEqual(2, movies.Count);
        }

        [Test]
        public void ShouldSelectMovieByTitle()
        {
            var xMen = repository.ByTitle("X Men");
            Assert.That(xMen, Is.EqualTo(movieXMen));
        }

    }
}