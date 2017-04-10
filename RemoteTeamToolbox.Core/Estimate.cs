namespace RemoteTeamToolbox.Core
{
    public class Estimate
    {
        public readonly SpecialEstimate SpecialValue;
        public readonly decimal Value;

        public Estimate(SpecialEstimate specialValue)
        {
            SpecialValue = specialValue;
            Value = 0m;
        }

        public Estimate(decimal value)
        {
            SpecialValue = SpecialEstimate.Value;
            Value = value;
        }
    }
}
