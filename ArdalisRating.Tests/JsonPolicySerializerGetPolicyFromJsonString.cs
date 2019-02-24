using Xunit;

namespace ArdalisRating.Tests
{
    public class JsonPolicySerializerGetPolicyFromJsonString
    {
        [Fact]
        public void ReturnsDefaultPolicyFromEmptyJsonString()
        {
            var inputJson = "{}";
            var serializer = new JsonPolicySerializer();

            var result = serializer.GetPolicyFromJsonString(inputJson);

            var policy = new Policy();
            AssertPoliciesEqual(result, policy);
        }

        [Fact]
        public void ReturnsSimpleAutoPolicyFromValidJsonString()
        {
            var inputJson = @"{
  ""type"": ""Auto"",
  ""make"": ""BMW""
}
";
            var serializer = new JsonPolicySerializer();

            var result = serializer.GetPolicyFromJsonString(inputJson);

            var policy = new Policy {
                Type = "Auto",
                Make = "BMW" };
            AssertPoliciesEqual(result, policy);
        }

        private static void AssertPoliciesEqual(Policy result, Policy policy)
        {
            Assert.Equal(policy.Address, result.Address);
            Assert.Equal(policy.Amount, result.Amount);
            Assert.Equal(policy.BondAmount, result.BondAmount);
            Assert.Equal(policy.DateOfBirth, result.DateOfBirth);
            Assert.Equal(policy.Deductible, result.Deductible);
            Assert.Equal(policy.FullName, result.FullName);
            Assert.Equal(policy.IsSmoker, result.IsSmoker);
            Assert.Equal(policy.Make, result.Make);
            Assert.Equal(policy.Miles, result.Miles);
            Assert.Equal(policy.Model, result.Model);
            Assert.Equal(policy.Type, result.Type);
            Assert.Equal(policy.Valuation, result.Valuation);
            Assert.Equal(policy.Year, result.Year);
        }
    }
}
