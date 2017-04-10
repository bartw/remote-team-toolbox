using Xunit;

namespace RemoteTeamToolbox.Core.Test
{
    public class EstimateTest
    {
        [Theory]
        [InlineData(SpecialEstimate.Empty)]
        [InlineData(SpecialEstimate.Break)]
        [InlineData(SpecialEstimate.Infinity)]
        [InlineData(SpecialEstimate.Unsure)]
        [InlineData(SpecialEstimate.Value)]
        public void GivenASpecialValue_WhenConstructingAnEstimate_ThenAEstimateWithThatSpecialValueAndValue0IsCreated(SpecialEstimate specialValue)
        {
            var estimate = new Estimate(specialValue);
            Assert.Equal(specialValue, estimate.SpecialValue);
            Assert.Equal(0m, estimate.Value);
        }

        [Fact]
        public void GivenADecimalValue_WhenConstructingAnEstimate_ThenAEstimateWithThatValueAndSpecialValueValueIsCreated()
        {
            var estimate = new Estimate(123m);
            Assert.Equal(SpecialEstimate.Value, estimate.SpecialValue);
            Assert.Equal(123m, estimate.Value);
        }
    }
}
