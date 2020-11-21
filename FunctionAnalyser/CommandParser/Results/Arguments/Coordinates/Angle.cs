using static CommandParser.Providers.NumberProvider;

namespace CommandParser.Results.Arguments.Coordinates
{
    public class Angle
    {
        public float Value { get; }
        public bool IsRelative { get; }

        public Angle(float value, bool isRelative)
        {
            Value = value;
            IsRelative = isRelative;
        }

        public override string ToString()
        {
            if (IsRelative) return $"~{Value.ToString(NumberFormatInfo)}";
            else return $"{Value.ToString(NumberFormatInfo)}";
        }
    }
}
