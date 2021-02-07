using CommandParser.Results;
using CommandParser.Results.Arguments.Coordinates;

namespace CommandParser.Parsers.Coordinates
{
    public class WorldCoordinateParser
    {
        private readonly IStringReader StringReader;
        private readonly bool UseBedrock;

        public WorldCoordinateParser(IStringReader stringReader, bool useBedrock)
        {
            StringReader = stringReader;
            UseBedrock = useBedrock;
        }

        public ReadResults ReadInteger(out WorldCoordinate result)
        {
            result = default;
            if (!StringReader.CanRead())
            {
                return ReadResults.Failure(CommandError.ExpectedBlockPosition().WithContext(StringReader));
            }
            if (StringReader.Peek() == '^')
            {
                return ReadResults.Failure(CommandError.MixedCoordinateType().WithContext(StringReader));
            }

            bool isRelative = IsRelative();

            double value = 0.0d;
            if (!StringReader.AtEndOfArgument())
            {
                if (UseBedrock && !IsNumberPart(StringReader.Peek()))
                {
                    result = new WorldCoordinate(value, isRelative);
                    return ReadResults.Success();
                }
                
                ReadResults readResults;
                if (isRelative)
                {
                    readResults = StringReader.ReadDouble(out value);
                }
                else
                {
                    readResults = StringReader.ReadInteger(out int integerValue);
                    value = integerValue;
                }
                if (!readResults.Successful) return readResults;
            }

            result = new WorldCoordinate(value, isRelative);
            return ReadResults.Success();
        }

        public ReadResults ReadDouble(out WorldCoordinate result)
        {
            result = default;
            if (!StringReader.CanRead())
            {
                return ReadResults.Failure(CommandError.ExpectedCoordinate().WithContext(StringReader));
            }
            if (StringReader.Peek() == '^')
            {
                return ReadResults.Failure(CommandError.MixedCoordinateType().WithContext(StringReader));
            }

            bool isRelative = IsRelative();
            int start = StringReader.GetCursor();

            if (StringReader.AtEndOfArgument() || (UseBedrock && !IsNumberPart(StringReader.Peek())))
            {
                result = new WorldCoordinate(isRelative ? 0.0d : 0.5d, isRelative);
                return ReadResults.Success();
            }

            ReadResults readResults = StringReader.ReadDouble(out double value);
            if (!readResults.Successful) return readResults;

            string number = StringReader.GetString()[start..StringReader.GetCursor()];
            if (!isRelative && !number.Contains('.')) value += 0.5d;

            result = new WorldCoordinate(value, isRelative);
            return ReadResults.Success();
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

        private static bool IsNumberPart(char c)
        {
            return c >= '0' && c <= '9' || c == '.' || c == '-';
        }
    }
}
