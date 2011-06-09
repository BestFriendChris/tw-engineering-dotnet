using System;
using System.IO;

namespace VideoWorld.Utils
{
    public class Duration
    {
        private const int INFINITE = Int32.MaxValue;
        private readonly int days;

        public int Days
        {
            get
            {
                if (days == INFINITE) throw new InvalidDataException("Duration is Infinite");
                return days;
            } 
        }

        public Duration Infinite { get { return new Duration(INFINITE); } }

        public Duration (int days)
        {
            this.days = days;
        }

        public static Duration OfDays(int days)
        {
            if (days < 0)
                throw new ArgumentException("Days must not be negative");
            return new Duration(days);
        }
        
        public bool IsInfinite()
        {
            return days == INFINITE;
        }

        public override String ToString()
        {
            if (days == INFINITE)
            {
                return "infinite";
            }
            return days.ToString();
        }

        public bool Equals(Duration other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.days == days;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Duration)) return false;
            return Equals((Duration) obj);
        }

        public override int GetHashCode()
        {
            return days;
        }
    }
}