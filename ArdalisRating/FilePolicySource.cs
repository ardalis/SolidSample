using System.IO;

namespace ArdalisRating
{
    public class FilePolicySource
    {
        public string GetPolicyFromSource()
        {
            return File.ReadAllText("policy.json");
        }
    }
}
