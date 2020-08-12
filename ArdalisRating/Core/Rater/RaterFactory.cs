using System;

namespace ArdalisRating
{
    public class RaterFactory
    {
        private readonly IRatingUpdater _ratingUpdater;
        private readonly ILogger _logger;

        //public Rater Create(Policy policy, IRatingContext context)

        //public RaterFactory(IRatingUpdater ratingUpdater)
        public RaterFactory(ILogger logger)
        {
            //_ratingUpdater = ratingUpdater;
            _logger = logger;
        }
        public Rater Create(Policy policy)
        {
            try
            {
                /*
                return (Rater)Activator.CreateInstance(
                    Type.GetType($"ArdalisRating.{policy.Type}PolicyRater"),
                        new object[] { context });
                */
                return (Rater)Activator.CreateInstance(
                    Type.GetType($"ArdalisRating.{policy.Type}PolicyRater"),
                    new object[] { _logger });

            }
            catch
            {
                //return new UnknownPolicyRater(context);
                return new UnknownPolicyRater(_logger);
            }
        }
    }
}
