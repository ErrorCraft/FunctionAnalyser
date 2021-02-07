using CommandParser.Results;
using CommandParser.Results.Arguments.Coordinates;

namespace CommandParser.Parsers.Coordinates
{
    public class AngleParser
    {
        private readonly IStringReader StringReader;

        public AngleParser(IStringReader stringReader)
        {
            StringReader = stringReader;
        }

        public ReadResults Read(out Angle result)
        {
            result = default;
            if (!StringReader.CanRead())
            {
                return ReadResults.Failure(CommandError.ExpectedAngle().WithContext(StringReader));
            }
            if (StringReader.Peek() == '^')
            {
                return ReadResults.Failure(CommandError.MixedCoordinateType().WithContext(StringReader));
            }

            bool isRelative = IsRelative();

            if (StringReader.AtEndOfArgument())
            {
                result = new Angle(0.0f, isRelative);
                return ReadResults.Success();
            }

            ReadResults readResults = StringReader.ReadFloat(out float value);
            if (readResults.Successful) result = new Angle(value, isRelative);
            return readResults;
        }

        private bool IsRelative()
        {
            if (StringReader.CanRead() && StringReader.Peek() == '~')
            {
                StringReader.Skip();
                return true;
            }
            return false;
        }
    }
}
