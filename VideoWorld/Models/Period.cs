namespace VideoWorld.Models
{
    public class Period
    {
        public Period (int duration)
        {
            Duration = duration;
        }

        public int Duration { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", Duration, Duration == 1 ? "day" : "days");
        }
    }
}