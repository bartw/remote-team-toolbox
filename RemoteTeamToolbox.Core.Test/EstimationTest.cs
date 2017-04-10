using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RemoteTeamToolbox.Core.Test
{
    public class EstimationTest
    {
        [Fact]
        public void GivenNull_WhenCreatingAnEstimation_ThenAnArgumentExceptionIsThrown()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Estimation(null));
            Assert.Equal("Missing subscribers.", exception.Message);
        }

        [Fact]
        public void GivenAnEmptyListOfSubscribers_WhenCreatingAnEstimation_ThenAnArgumentExceptionIsThrown()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Estimation(Enumerable.Empty<Guid>()));
            Assert.Equal("Missing subscribers.", exception.Message);
        }

        [Fact]
        public void GivenAListOfSubscribers_WhenCreatingAnEstimation_ThenAnEstimationIsCreatedWithEmptyEstimates()
        {
            var key = Guid.NewGuid();
            
            var estimation = new Estimation(new[] { key });

            Assert.Equal(SpecialEstimate.Empty, estimation.GetEstimateFor(key).SpecialValue);
            Assert.Equal(0m, estimation.GetEstimateFor(key).Value);
        }

        [Fact]
        public void GivenAnEstimation_WhenGetEstimationForUnkownKey_ThenAKeyNotFoundExceptionIsThrown()
        {
            var estimation = new Estimation(new[] { Guid.NewGuid() });
            var exception = Assert.Throws<KeyNotFoundException>(() => estimation.GetEstimateFor(Guid.NewGuid()));
        }

        [Fact]
        public void GivenAnEstimation_WhenSetEstimationForUnkownKey_ThenAKeyNotFoundExceptionIsThrown()
        {
            var estimation = new Estimation(new[] { Guid.NewGuid() });
            var exception = Assert.Throws<KeyNotFoundException>(() => estimation.SetEstimateFor(Guid.NewGuid(), new Estimate(SpecialEstimate.Empty)));
        }

        [Fact]
        public void GivenAnEstimation_WhenSetEstimationForKey_ThenTheEstimationForThatKeyIsSet()
        {
            var key = Guid.NewGuid();
            var estimate = new Estimate(SpecialEstimate.Break);
            var estimation = new Estimation(new[] { key });
            
            estimation.SetEstimateFor(key, estimate);

            Assert.Equal(estimate, estimation.GetEstimateFor(key));
        }

        // [Theory]
        // [InlineData(, false)]
        // [InlineData(new Estimate(SpecialEstimate.Empty), new Estimate(SpecialEstimate.Infinity), new Estimate(SpecialEstimate.Empty), false)]
        // [InlineData(new Estimate(SpecialEstimate.Empty), new Estimate(SpecialEstimate.Empty), new Estimate(13m), false)]
        // [InlineData(new Estimate(SpecialEstimate.Unsure), new Estimate(SpecialEstimate.Infinity), new Estimate(SpecialEstimate.Break), true)]
        // [InlineData(new Estimate(22m), new Estimate(2m), new Estimate(13m), true)]
        [Fact]
        public void GivenAnAllEmptyEstimation_WhenIsEstimationComplete_ThenFalseIsReturned()
        {
            var subscribers = new [] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()};
            var estimation = new Estimation(subscribers);
            
            Assert.False(estimation.IsEstimationComplete());
        }

        [Fact]
        public void GivenAnEstimationWhenSomeEstimatesNonEmpty_WhenIsEstimationComplete_ThenFalseIsReturned()
        {
            var subscribers = new [] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()};
            var estimation = new Estimation(subscribers);

            estimation.SetEstimateFor(subscribers[1], new Estimate(SpecialEstimate.Break));
            estimation.SetEstimateFor(subscribers[2], new Estimate(5m));
            
            Assert.False(estimation.IsEstimationComplete());
        }

        [Fact]
        public void GivenAnEstimationWhenAllValueEstimates_WhenIsEstimationComplete_ThenTrueIsReturned()
        {
            var subscribers = new [] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()};
            var estimation = new Estimation(subscribers);

            estimation.SetEstimateFor(subscribers[0], new Estimate(1m));
            estimation.SetEstimateFor(subscribers[1], new Estimate(2m));
            estimation.SetEstimateFor(subscribers[2], new Estimate(3m));
            
            Assert.True(estimation.IsEstimationComplete());
        }

        [Fact]
        public void GivenAnEstimationWhenAllSpecialValueEstimates_WhenIsEstimationComplete_ThenTrueIsReturned()
        {
            var subscribers = new [] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()};
            var estimation = new Estimation(subscribers);

            estimation.SetEstimateFor(subscribers[0], new Estimate(SpecialEstimate.Break));
            estimation.SetEstimateFor(subscribers[1], new Estimate(SpecialEstimate.Infinity));
            estimation.SetEstimateFor(subscribers[2], new Estimate(SpecialEstimate.Unsure));
            
            Assert.True(estimation.IsEstimationComplete());
        }
    }
}
