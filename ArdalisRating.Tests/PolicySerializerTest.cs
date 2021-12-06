using FluentAssertions;
using Xunit;

namespace ArdalisRating.Tests
{
    public class PolicySerializerTest
    {
        [Fact]
        public void GetPolicyFromJsonString_Called_ReturnsDefaultPolicyFromEmptyJsonString()
        {
            var inputJson = "{}";
            var serializer = new JsonPolicySerializer();

            var result = serializer.GetPolicyFromJsonString(inputJson);
            
            result.Should().BeEquivalentTo(new Policy());
        }
    }
}
