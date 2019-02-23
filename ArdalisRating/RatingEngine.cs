using System;

namespace ArdalisRating
{
    /// <summary>
    /// The RatingEngine reads the policy application details from a file and produces a numeric 
    /// rating value based on the details.
    /// </summary>
    public class RatingEngine
    {
        public ConsoleLogger Logger { get; set; } = new ConsoleLogger();
        public FilePolicySource PolicySource { get; set; } = new FilePolicySource();
        public JsonPolicySerializer PolicySerializer { get; set; } = new JsonPolicySerializer();
        public decimal Rating { get; set; }
        public void Rate()
        {
            Logger.Log("Starting rate.");

            Logger.Log("Loading policy.");

            string policyJson = PolicySource.GetPolicyFromSource();

            var policy = PolicySerializer.GetPolicyFromJsonString(policyJson);

            switch (policy.Type)
            {
                case PolicyType.Auto:
                    var rater = new AutoPolicyRater(this, this.Logger);
                    rater.Rate(policy);
                    break;

                case PolicyType.Land:
                    var rater2 = new LandPolicyRater(this, this.Logger);
                    rater2.Rate(policy);
                    break;

                case PolicyType.Life:
                    var rater3 = new LifePolicyRater(this, this.Logger);
                    rater3.Rate(policy);
                    break;

                default:
                    Logger.Log("Unknown policy type");
                    break;
            }

            Logger.Log("Rating completed.");
        }
    }
}
