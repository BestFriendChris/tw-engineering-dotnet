using System;

namespace VideoWorld.Models
{
    public class Rental 
    {
        private readonly Movie movie;
        private readonly Period periodInDays;

        public Rental(Movie movie, Period periodInDays)
        {
            this.movie = movie;
            this.periodInDays = periodInDays;
        }


        public Movie Movie
        {
            get {
                return movie;
            }
        }

        public Period Period
        {
            get { return periodInDays; }
        }
    }
}