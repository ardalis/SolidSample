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
        private readonly RaterFactory _factory;


        //public IRatingContext Context { get; set; } = new DefaultRatingContext();
        public IRatingContext Context { get; set; }
        public decimal Rating { get; set; }

        /*
        public RatingEngine() : this(new ConsoleLogger())
        {
            // default Ctor; 
            // chaining ctor to include the one parameter constructor
        }
        */

        public RatingEngine(ILogger logger, IPolicySource policySource, IPolicySerializer policySerializer, RaterFactory factory)
        {
            _policySource = policySource;
            _policySerializer = policySerializer;
            //Context = new DefaultRatingContext(_policySource, _policySerializer, logger);
            //Context.Engine = this;
            _logger = logger;
            _factory = factory;
        }

        public void Rate()
        {
            _logger.Log("Starting rate.");

            // Rather than using the Context instance, explicitly adding dependecies as constructor
            // Context.Log("Loading policy.");
            _logger.Log("Loading policy.");

            //string policyJson = Context.LoadPolicyFromFile();
            string policyJson = _policySource.GetPolicyFromSource();

            //var policy = Context.GetPolicyFromJsonString(policyJson);
            var policy = _policySerializer.GetPolicyFromJsonString(policyJson);


            //var rater = Context.CreateRaterForPolicy(policy, Context);
            var rater = _factory.Create(policy);

            rater.Rate(policy);

            _logger.Log("Rating completed.");
        }
    }
}
