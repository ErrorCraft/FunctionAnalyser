using CommandParser.Results;
using CommandParser.Results.Arguments.Coordinates;

namespace CommandParser.Parsers.Coordinates
{
    public class LocalCoordinatesParser
    {
        private readonly IStringReader StringReader;
        private readonly int Start;

        public LocalCoordinatesParser(IStringReader stringReader)
        {
            StringReader = stringReader;
            Start = stringReader.GetCursor();
        }

        public ReadResults Parse(out ICoordinates result)
        {
            result = default;

            ReadResults readResults = ReadDouble(out double left);
            if (!readResults.Successful) return readResults;
            if (!StringReader.AtEndOfArgument())
            {
                StringReader.SetCursor(Start);
                return new ReadResults(false, CommandError.Vec3CoordinatesIncomplete().WithContext(StringReader));
            }
            StringReader.Skip();

            readResults = ReadDouble(out double up);
            if (!readResults.Successful) return readResults;
            if (!StringReader.AtEndOfArgument())
            {
                StringReader.SetCursor(Start);
                return new ReadResults(false, CommandError.Vec3CoordinatesIncomplete().WithContext(StringReader));
            }
            StringReader.Skip();

            readResults = ReadDouble(out double forwards);
            if (readResults.Successful) result = new LocalCoordinates(left, up, forwards);
            return readResults;
        }

        private ReadResults ReadDouble(out double result)
        {
            result = default;
            if (!StringReader.CanRead())
            {
                return new ReadResults(false, CommandError.ExpectedCoordinate().WithContext(StringReader));
            }
            if (StringReader.Peek() != '^')
            {
                return new ReadResults(false, CommandError.MixedCoordinateType().WithContext(StringReader));
            }
            StringReader.Skip();
            if (StringReader.AtEndOfArgument())
            {
                result = 0.0d;
                return new ReadResults(true, null);
            } else
            {
                return StringReader.ReadDouble(out result);
            }
        }
    }
}
