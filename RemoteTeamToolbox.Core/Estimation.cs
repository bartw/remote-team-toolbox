using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RemoteTeamToolbox.Core
{
    public class Estimation
    {
        private Dictionary<Guid, Estimate> _estimations;

        public Estimation(IEnumerable<Guid> subscribers)
        {
            if (subscribers == null || !subscribers.Any())
            {
                throw new ArgumentException("Missing subscribers.");
            }
            _estimations = subscribers.ToDictionary(guid => guid, guid => new Estimate(SpecialEstimate.Empty));
        }

        public void SetEstimateFor(Guid subscriber, Estimate value)
        {
            if (!_estimations.ContainsKey(subscriber))
            {
                throw new KeyNotFoundException();
            }
            _estimations[subscriber] = value;
        }

        public Estimate GetEstimateFor(Guid subscriber)
        {
            return _estimations[subscriber];
        }

        public bool IsEstimationComplete()
        {
            return _estimations.Values.All(value => value.SpecialValue != SpecialEstimate.Empty);
        }

        public ReadOnlyDictionary<Guid, Estimate> GetEstimates()
        {
            return new ReadOnlyDictionary<Guid, Estimate>(_estimations);
        }
    }
}
