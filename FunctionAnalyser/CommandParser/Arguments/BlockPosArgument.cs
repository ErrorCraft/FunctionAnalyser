﻿using CommandParser.Parsers.Coordinates;
using CommandParser.Results;
using CommandParser.Results.Arguments.Coordinates;

namespace CommandParser.Arguments
{
    public class BlockPosArgument : IArgument<ICoordinates>
    {
        public ReadResults Parse(IStringReader reader, out ICoordinates result)
        {
            if (reader.CanRead() && reader.Peek() == '^')
            {
                return new LocalCoordinatesParser(reader).Parse(out result);
            }
            return new WorldCoordinatesParser(reader).ParseInteger(out result);
        }
    }
}
