using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ArdalisRating
{

    public class JsonPolicySerializer : IPolicySerializer
    {
        public Policy GetPolicyFromString(string policyString)
        {
            return JsonConvert.DeserializeObject<Policy>(policyString,
                new StringEnumConverter());
        }
    }
}
