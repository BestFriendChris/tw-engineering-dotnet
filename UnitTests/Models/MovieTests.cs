using NUnit.Framework;
using VideoWorld.Models;

namespace UnitTests.Models
{
    public class MovieTests
    {
        [TestCase(null)]
        [TestCase("")]
        public void ShouldRequireTitle(string invalidTitle)
        {
            Assert.That(() => new Movie(invalidTitle), Throws.Exception);
        }

        [Test]
        public void ShouldContainTitle()
        {
            var movie = new Movie("Avatar");
            Assert.That(movie.Title, Is.EqualTo("Avatar"));
        }
    }
}