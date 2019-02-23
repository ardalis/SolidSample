using System;

namespace ArdalisRating
{
    public abstract class Rater
    {
        protected readonly RatingEngine _engine;
        protected readonly ConsoleLogger _logger;

        public Rater(RatingEngine engine, ConsoleLogger logger)
        {
            _engine = engine;
            _logger = logger;
        }

        public abstract void Rate(Policy policy);
    }

    public class AutoPolicyRater : Rater
    {
        public AutoPolicyRater(RatingEngine engine, ConsoleLogger logger)
            : base(engine, logger)
        {
        }

        public override void Rate(Policy policy)
        {
            _logger.Log("Rating AUTO policy...");
            _logger.Log("Validating policy.");
            if (String.IsNullOrEmpty(policy.Make))
            {
                _logger.Log("Auto policy must specify Make");
                return;
            }
            if (policy.Make == "BMW")
            {
                if (policy.Deductible < 500)
                {
                    _engine.Rating = 1000m;
                }
                _engine.Rating = 900m;
            }
        }
    }
}
