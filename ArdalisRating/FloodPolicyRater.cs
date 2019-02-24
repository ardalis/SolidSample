namespace ArdalisRating
{
    public class FloodPolicyRater : Rater
    {
        public FloodPolicyRater(IRatingContext context)
            : base(context)
        {
        }

        public override void Rate(Policy policy)
        {
            _logger.Log("Rating FLOOD policy...");
            _logger.Log("Validating policy.");
            if (policy.BondAmount == 0 || policy.Valuation == 0)
            {
                _logger.Log("Flood policy must specify Bond Amount and Valuation.");
                return;
            }
            if (policy.ElevationAboveSeaLevelFeet <= 0)
            {
                _logger.Log("Flood policy is not available for elevations at or below sea level.");
                return;
            }
            if (policy.BondAmount < 0.8m * policy.Valuation)
            {
                _logger.Log("Insufficient bond amount.");
                return;
            }
            decimal multiple = 1.0m;
            if(policy.ElevationAboveSeaLevelFeet < 100)
            {
                multiple = 2.0m;
            } else if (policy.ElevationAboveSeaLevelFeet < 500)
            {
                multiple = 1.5m;
            } else if (policy.ElevationAboveSeaLevelFeet < 1000)
            {
                multiple = 1.1m;
            }
            _context.UpdateRating(policy.BondAmount * 0.05m * multiple);
        }
    }
}
