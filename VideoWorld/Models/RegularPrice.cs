namespace VideoWorld.Models
{
    public class RegularPrice : IPrice
    {
        public decimal GetCharge(int daysRented)
        {
            decimal result = 2.00m;
            if (daysRented > 2)
                result += (daysRented - 2) * 1.50m;
            return result;
        }

        public int GetFrequentRenterPoints(int daysRented)
        {
            return 1;
        }

    }
}