using System;
using VideoWorld.Utils;

namespace VideoWorld.Models
{
    public class DetailedMovie : Movie
    {
        public string Director { get; private set; }
        public string Actor { get; private set; }
        public string Actress { get; private set; }
        public string Category { get; private set; }

        public DetailedMovie(string title, IPrice price, string director, string actor, string actress, string category) : base(title, price)
        {
            if (!Feature.DetailedMovies.IsEnabled())
            {
                throw new NotSupportedException("Detailed Movies Feature not enabled");
            }
            Director = director;
            Actor = actor;
            Actress = actress;
            Category = category;
        }


    }
}