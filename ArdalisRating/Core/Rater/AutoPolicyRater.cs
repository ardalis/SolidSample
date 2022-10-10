using System;

namespace ArdalisRating
{
    public class AutoPolicyRater : Rater
    {
        //public AutoPolicyRater(IRatingContext context)
        /*
         public AutoPolicyRater(IRatingUpdater ratingUpdater, ILogger logger)
             : base(ratingUpdater, logger)
         {

         }
        */
        public AutoPolicyRater(ILogger logger)
            : base(logger) { }


        //public override void Rate(Policy policy)
        public override decimal Rate(Policy policy)
        {
            Logger.Log("Rating AUTO policy...");
            Logger.Log("Validating policy.");
            if (String.IsNullOrEmpty(policy.Make))
            {
                Logger.Log("Auto policy must specify Make");
                return 0m;
            }
            if (policy.Make == "BMW")
            {
                if (policy.Deductible < 500)
                {
                    //_ratingUpdater.UpdateRating(1000m);
                    return 1000m;
                }
                //_ratingUpdater.UpdateRating(900m);
                return 900m;
            }

            return 0m;
        }
    }
}
