namespace ArdalisRating
{
    /// <summary>
    /// The RatingEngine reads the policy application details from a file and produces a numeric 
    /// rating value based on the details.
    /// </summary>
    public class RatingEngine
    {
        private readonly ILogger _logger;
        private readonly IPolicySource _policySource;
        private readonly IPolicySerializer _policySerializer;

        public IRatingContext Context { get; set; }
        public decimal Rating { get; set; }

        public RatingEngine(ILogger logger,
            IPolicySource policySource,
            IPolicySerializer policySerializer)
        {
            _logger = logger;
            _policySource = policySource;
            _policySerializer = policySerializer;
            Context = new DefaultRatingContext(_policySource, _policySerializer);
            Context.Engine = this;
        }

        public void Rate()
        {
            _logger.Log("Starting rate.");

            _logger.Log("Loading policy.");

            string policyJson = _policySource.GetPolicyFromSource();

            var policy = _policySerializer.GetPolicyFromString(policyJson);

            var rater = Context.CreateRaterForPolicy(policy, Context);

            rater.Rate(policy);

            _logger.Log("Rating completed.");
        }
    }
}
