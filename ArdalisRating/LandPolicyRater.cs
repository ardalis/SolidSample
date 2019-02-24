namespace ArdalisRating
{
    public class LandPolicyRater : Rater
    {
        public LandPolicyRater(IRatingContext context)
            :base(context)
        {
        }

        public override void Rate(Policy policy)
        {
            _logger.Log("Rating LAND policy...");
            _logger.Log("Validating policy.");
            if (policy.BondAmount == 0 || policy.Valuation == 0)
            {
                _logger.Log("Land policy must specify Bond Amount and Valuation.");
                return;
            }
            if (policy.BondAmount < 0.8m * policy.Valuation)
            {
                _logger.Log("Insufficient bond amount.");
                return;
            }
            _context.UpdateRating(policy.BondAmount * 0.05m);
        }
    }
}
