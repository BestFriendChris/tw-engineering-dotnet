namespace VideoWorld.Models
{
    public interface IPrice
    {
        decimal GetCharge(int daysRented);
        int GetFrequentRenterPoints(int daysRented);
    }
}