namespace ArdalisRating
{
    public abstract class Rater
    {
        //protected readonly IRatingContext _context;
        //protected readonly IRatingUpdater _ratingUpdater;

        //protected readonly ConsoleLogger _logger;



        // protected ILogger Logger { get; set; } = new ConsoleLogger();
        public ILogger Logger { get; set; }

        /*
        public Rater(ILogger logger)
        {
            Logger = logger;
        }
        */

        //public Rater(IRatingContext context)
        //public Rater(IRatingUpdater ratingUpdater, ILogger logger)
        //public Rater(IRatingUpdater ratingUpdater)
        public Rater(ILogger logger)
        {
            //_context = context;
            //_ratingUpdater = ratingUpdater;

            //_logger = _context.Logger;
            Logger = logger;
        }

        //public abstract void Rate(Policy policy);
        public abstract decimal Rate(Policy policy);
    }
}
