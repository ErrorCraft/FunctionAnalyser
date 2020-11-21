using CommandParser.Parsers.Coordinates;
using CommandParser.Results;
using CommandParser.Results.Arguments.Coordinates;

namespace CommandParser.Arguments
{
    public class Vec3Argument : IArgument<ICoordinates>
    {
        public ReadResults Parse(StringReader reader, out ICoordinates result)
        {
            if (reader.CanRead() && reader.Peek() == '^')
            {
                return new LocalCoordinatesParser(reader).Parse(out result);
            }
            return new WorldCoordinatesParser(reader).ParseDouble(out result);
        }
    }
}
