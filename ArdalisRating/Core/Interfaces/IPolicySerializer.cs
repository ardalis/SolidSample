using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ArdalisRating
{
    public interface IPolicySerializer
    {
        Policy GetPolicyFromString(string policyString);
    }
}
