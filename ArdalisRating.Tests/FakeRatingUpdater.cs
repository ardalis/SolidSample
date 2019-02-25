namespace ArdalisRating.Tests
{
    public class FakeRatingUpdater : IUpdateRating
    {
        public decimal? NewRating { get; private set; }
        public void UpdateRating(decimal rating)
        {
            NewRating = rating;
        }
    }
}
