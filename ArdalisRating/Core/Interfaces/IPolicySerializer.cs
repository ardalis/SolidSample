namespace ArdalisRating
{
    public interface IPolicySerializer
    {
        public Policy GetPolicyFromJsonString(string jsonString);
    }
}
