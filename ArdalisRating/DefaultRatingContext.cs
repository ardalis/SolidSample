using System;

namespace ArdalisRating
{
    public class DefaultRatingContext : IRatingContext
    {
        public RatingEngine Engine { get; set; }

        //public ConsoleLogger Logger => new ConsoleLogger();

        // DIP made change
        private readonly IPolicySource _policySource;
        private readonly IPolicySerializer _policySerializer;
        private readonly ILogger _logger;

        public DefaultRatingContext(IPolicySource policySource, IPolicySerializer policySerializer, ILogger logger)
        {
            _policySource = policySource;
            _policySerializer = policySerializer;
            _logger = logger;
        }

        public Rater CreateRaterForPolicy(Policy policy, IRatingContext context)
        {
            //return new RaterFactory().Create(policy, context);
            return new RaterFactory(_logger).Create(policy);
        }

        public Policy GetPolicyFromJsonString(string policyJson)
        {
            //return new JsonPolicySerializer().GetPolicyFromJsonString(policyJson);
            return _policySerializer.GetPolicyFromJsonString(policyJson);
        }

        public Policy GetPolicyFromXmlString(string policyXml)
        {
            throw new NotImplementedException();
        }

        public string LoadPolicyFromFile()
        {
            // return new FilePolicySource().GetPolicyFromSource();
            return _policySource.GetPolicyFromSource();
        }

        public string LoadPolicyFromURI(string uri)
        {
            throw new NotImplementedException();
        }


        public void Log(string message)
        {
            new ConsoleLogger().Log(message);
        }
        public void UpdateRating(decimal rating)
        {
            Engine.Rating = rating;
        }
    }
}
