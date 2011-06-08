using System;

namespace VideoWorld.Utils
{
    public class Period
    {
        public DateTime StartDate { get; private set; }
        
        public DateTime EndDate { get; private set; }

        public Duration Duration { get; private set; }

        public Period(DateTime startDate, Duration duration, DateTime endDate)
        {
            StartDate = startDate;
            Duration = duration;
            EndDate = endDate;
        }

        public static Period Of (DateTime startDate, Duration duration)
        {
            DateTime endDate = new DateTime(startDate.Ticks).AddDays(duration.Days);
            return new Period(startDate, duration, endDate);
        }

        public bool Equals(Period other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.StartDate.Equals(StartDate) && other.EndDate.Equals(EndDate) && Equals(other.Duration, Duration);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Period)) return false;
            return Equals((Period) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = StartDate.GetHashCode();
                result = (result*397) ^ EndDate.GetHashCode();
                result = (result*397) ^ (Duration != null ? Duration.GetHashCode() : 0);
                return result;
            }
        }
    }
}