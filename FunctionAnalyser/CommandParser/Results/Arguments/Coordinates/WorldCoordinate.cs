using static CommandParser.Providers.NumberProvider;

namespace CommandParser.Results.Arguments.Coordinates
{
    public class WorldCoordinate
    {
        public double Value { get; }
        public bool IsRelative { get; }

        public WorldCoordinate(double value, bool isRelative)
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
