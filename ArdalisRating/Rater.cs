namespace ArdalisRating
{
    public abstract class Rater
    {
        protected readonly IUpdateRating _updateRating;

        public ILogger Logger { get; set; } = new ConsoleLogger();

        public Rater(IUpdateRating updateRating)
        {
            _updateRating = updateRating;
        }

        public abstract void Rate(Policy policy);
    }
}
