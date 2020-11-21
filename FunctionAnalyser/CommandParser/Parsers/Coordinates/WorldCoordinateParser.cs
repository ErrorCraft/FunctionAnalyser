using CommandParser.Results;
using CommandParser.Results.Arguments.Coordinates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandParser.Parsers.Coordinates
{
    public class WorldCoordinateParser
    {
        private readonly StringReader StringReader;

        public WorldCoordinateParser(StringReader stringReader)
        {
            StringReader = stringReader;
        }

        public ReadResults ReadInteger(out WorldCoordinate result)
        {
            result = default;
            if (!StringReader.CanRead())
            {
                return new ReadResults(false, CommandError.ExpectedBlockPosition().WithContext(StringReader));
            }
            if (StringReader.Peek() == '^')
            {
                return new ReadResults(false, CommandError.MixedCoordinateType().WithContext(StringReader));
            }

            bool isRelative = IsRelative();

            double value = 0.0d;
            if (!StringReader.AtEndOfArgument())
            {
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
            return new ReadResults(true, null);
        }

        public ReadResults ReadDouble(out WorldCoordinate result)
        {
            result = default;
            if (!StringReader.CanRead())
            {
                return new ReadResults(false, CommandError.ExpectedCoordinate().WithContext(StringReader));
            }
            if (StringReader.Peek() == '^')
            {
                return new ReadResults(false, CommandError.MixedCoordinateType().WithContext(StringReader));
            }

            bool isRelative = IsRelative();
            int start = StringReader.Cursor;

            if (StringReader.AtEndOfArgument())
            {
                result = new WorldCoordinate(isRelative ? 0.0d : 0.5d, isRelative);
                return new ReadResults(true, null);
            }

            ReadResults readResults = StringReader.ReadDouble(out double value);
            if (!readResults.Successful) return readResults;

            string number = StringReader.Command[start..StringReader.Cursor];
            if (!isRelative && !number.Contains('.')) value += 0.5d; // b1 is "correct the number" thing

            result = new WorldCoordinate(value, isRelative);
            return new ReadResults(true, null);
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
