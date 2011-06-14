using NUnit.Framework;
using VideoWorld.Models;

namespace UnitTests.Models
{
    [TestFixture]
    public class DetailedMovieTests
    {
        [Test]
        public void ShouldContainMovieDetails()
        {
            var movie = new DetailedMovie("Avatar", new NewReleasePrice(), "James Cameron", "Sam Worthington", "Zoe Saldana", "Fiction");
            Assert.That(movie.Title, Is.EqualTo("Avatar"));
            Assert.That(movie.Director, Is.EqualTo("James Cameron"));
            Assert.That(movie.Actor, Is.EqualTo("Sam Worthington"));
            Assert.That(movie.Actress, Is.EqualTo("Zoe Saldana"));
            Assert.That(movie.Category, Is.EqualTo("Fiction"));
        }
    }
}