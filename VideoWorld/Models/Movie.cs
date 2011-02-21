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

        public bool Equals(Movie other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._title, _title);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Movie)) return false;
            return Equals((Movie) obj);
        }

        public override int GetHashCode()
        {
            return (_title != null ? _title.GetHashCode() : 0);
        }

        public string Title
        {
            get { return _title; }
        }
    }
}