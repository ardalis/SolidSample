namespace ArdalisRating
{
    public class UnknownPolicyRater : Rater
    {
        /*
        public UnknownPolicyRater(IRatingContext context)
            : base(context)
        {
        }
        */

        /*
        public UnknownPolicyRater(IRatingUpdater ratingUpdater)
            : base(ratingUpdater)
        {

        }
        */
        public UnknownPolicyRater(ILogger logger)
            : base(logger)
        {

        }


        public override decimal Rate(Policy policy)
        {
            Logger.Log("Unknown policy type");
            return 0m;
        }
    }
}
