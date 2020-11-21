﻿using CommandParser.Parsers.Coordinates;
using CommandParser.Results;
using CommandParser.Results.Arguments.Coordinates;

namespace CommandParser.Arguments
{
    public class AngleArgument : IArgument<Angle>
    {
        public ReadResults Parse(StringReader reader, out Angle result)
        {
            int start = reader.Cursor;
            ReadResults readResults = new AngleParser(reader).Read(out result);
            if (!readResults.Successful) return readResults;

            if (float.IsNaN(result.Value) || float.IsInfinity(result.Value))
            {
                reader.Cursor = start;
                return new ReadResults(false, CommandError.InvalidAngle().WithContext(reader));
            }
            return new ReadResults(true, null);
        }
    }
}
