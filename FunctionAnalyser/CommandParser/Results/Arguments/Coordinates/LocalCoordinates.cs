using static CommandParser.Providers.NumberProvider;

namespace CommandParser.Results.Arguments.Coordinates
{
    public class LocalCoordinates : ICoordinates
    {
        public double Left { get; }
        public double Up { get; }
        public double Forwards { get; }

        public LocalCoordinates(double left, double up, double forwards)
        {
            Left = left;
            Up = up;
            Forwards = forwards;
        }

        public bool IsXRelative()
        {
            return true;
        }

        public bool IsYRelative()
        {
            return true;
        }

        public bool IsZRelative()
        {
            return true;
        }

        public override string ToString()
        {
            return $"^{Left.ToString(NumberFormatInfo)} ^{Up.ToString(NumberFormatInfo)} ^{Forwards.ToString(NumberFormatInfo)}";
        }
    }
}
