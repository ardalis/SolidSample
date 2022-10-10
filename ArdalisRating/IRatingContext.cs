namespace ArdalisRating
{
    public interface IRatingContext : ILogger, IRatingUpdater
    {
        // Removing both the logging options since it is available in ILogger
        //void Log(string message);
        string LoadPolicyFromFile();
        string LoadPolicyFromURI(string uri);
        Policy GetPolicyFromJsonString(string policyJson);
        Policy GetPolicyFromXmlString(string policyXml);
        Rater CreateRaterForPolicy(Policy policy, IRatingContext context);
        //void UpdateRating(decimal rating);
        RatingEngine Engine { get; set; }
        //ConsoleLogger Logger { get; }
    }
}
