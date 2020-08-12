using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Xunit;


namespace ArdalisRating.Tests
{
    // only for testing purpose
    public class FakeLogger : ILogger
    {
        public List<string> LoggedMessages { get; } = new List<string>();

        public void Log(string message)
        {
            LoggedMessages.Add(message);
        }
    }

    // Only for testing purpose
    public class FakePolicySource : IPolicySource
    {
        public string PolicyString { get; set; } = "";

        public string GetPolicyFromSource()
        {
            return PolicyString;
        }
    }

    public class RatingEngineRate
    {
        private readonly RatingEngine _engine;
        private readonly ILogger _logger;
        private readonly IPolicySource _policySource;
        private IRatingContext Context { get; set; }

        public RatingEngineRate()
        {
            _logger = new FakeLogger();
            _policySource = new FakePolicySource();
            _engine = new RatingEngine(_logger, _policySource);
            this.Context = new DefaultRatingContext(_policySource);
        }

        [Fact]
        public void ReturnsRatingOf10000For200000LandPolicy()
        {
            var policy = new Policy
            {
                Type = "Land",
                BondAmount = 200000,
                Valuation = 200000
            };
            string json = JsonConvert.SerializeObject(policy);
            //File.WriteAllText("policy.json", json);
            _policySource.PolicyString = json;

            //var engine = new RatingEngine();
            _engine.Rate();
            var result = _engine.Rating;

            Assert.Equal(10000, result);
        }

        [Fact]
        public void ReturnsRatingOf0For200000BondOn260000LandPolicy()
        {
            var policy = new Policy
            {
                Type = "Land",
                BondAmount = 200000,
                Valuation = 260000
            };
            string json = JsonConvert.SerializeObject(policy);
            File.WriteAllText("policy.json", json);

            // var _engine = new RatingEngine();
            _engine.Rate();
            var result = _engine.Rating;

            Assert.Equal(0, result);
        }

        [Fact]
        public void LogsStartingLoadingAndCompleting()
        {
            var policy = new Policy()
            {
                Type = "Land",
                BondAmount = 2000000,
                Valuation = 260000
            };

            string json = JsonConvert.SerializeObject(policy);
            File.WriteAllText("policy.json", json);

            _engine.Rate();
            var result = _engine.Rating;


            Assert.Contains(_logger.LoggedMessages, m => m == "Starting rate.");
            Assert.Contains(_logger.LoggedMessages, m => m == "Loading policy.");
            Assert.Contains(_logger.LoggedMessages, m => m == "Rating completed.");

        }
    }
}
