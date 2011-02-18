using System;

namespace VideoWorld.Models
{
    public class Movie
    {
        private readonly string _title;

        public Movie(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("title");
            }

            _title = title;
        }

        public string Title
        {
            get { return _title; }
        }
    }
}