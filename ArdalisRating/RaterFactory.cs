using System;

namespace ArdalisRating
{
    public class RaterFactory
    {
        private readonly IRatingUpdater _ratingUpdater;

        public RaterFactory(IRatingUpdater ratingUpdater)
        {
            _ratingUpdater = ratingUpdater;
        }

        public Rater Create(Policy policy)
        {
            try
            {
                return (Rater)Activator.CreateInstance(
                    Type.GetType($"ArdalisRating.{policy.Type}PolicyRater"),
                        new object[] { _ratingUpdater });
            }
            catch
            {
                return new UnknownPolicyRater(_ratingUpdater);
            }
        }
    }
}
