using System;
using VideoWorld.Utils;

namespace VideoWorld.ViewModels
{
    public class NewMovieViewModel
    {
        public string Title { get; set; }

        public string ErrorMessage { get; set; }

        public string Director { get; set; }

        public string Actor { get; set; }

        public string Actress { get; set; }

        public string Category { get; set; }

        public bool ShowDetailedMovies { get { return Feature.DetailedMovies.IsEnabled(); } }

        public bool AllFieldsNotPopulated()
        {
            return ShowDetailedMovies ? 
                string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Director) || string.IsNullOrEmpty(Actor) || string.IsNullOrEmpty(Actress) || string.IsNullOrEmpty(Category)
                : 
                string.IsNullOrEmpty(Title);
        }
    }
}