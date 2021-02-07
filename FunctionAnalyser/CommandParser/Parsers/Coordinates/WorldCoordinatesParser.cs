using CommandParser.Results;
using CommandParser.Results.Arguments.Coordinates;

namespace CommandParser.Parsers.Coordinates
{
    public class WorldCoordinatesParser
    {
        private readonly IStringReader StringReader;
        private readonly int Start;
        private readonly bool UseBedrock;

        public WorldCoordinatesParser(IStringReader stringReader, bool useBedrock)
        {
            StringReader = stringReader;
            Start = stringReader.GetCursor();
            UseBedrock = useBedrock;
        }

        public ReadResults ParseInteger(out ICoordinates result)
        {
            result = default;
            WorldCoordinateParser worldCoordinateParser = new WorldCoordinateParser(StringReader, UseBedrock);

            ReadResults readResults = worldCoordinateParser.ReadInteger(out WorldCoordinate x);
            if (!readResults.Successful) return readResults;
            if (UseBedrock) StringReader.SkipWhitespace();
            else
            {
                if (!StringReader.AtEndOfArgument())
                {
                    StringReader.SetCursor(Start);
                    return ReadResults.Failure(CommandError.Vec3CoordinatesIncomplete().WithContext(StringReader));
                }
                StringReader.Skip();
            }

            readResults = worldCoordinateParser.ReadInteger(out WorldCoordinate y);
            if (!readResults.Successful) return readResults;
            if (UseBedrock) StringReader.SkipWhitespace();
            else
            {
                if (!StringReader.AtEndOfArgument())
                {
                    StringReader.SetCursor(Start);
                    return ReadResults.Failure(CommandError.Vec3CoordinatesIncomplete().WithContext(StringReader));
                }
                StringReader.Skip();
            }

            readResults = worldCoordinateParser.ReadInteger(out WorldCoordinate z);
            if (readResults.Successful) result = new WorldCoordinates(x, y, z);
            return readResults;
        }

        public ReadResults ParseIntegerFlat(out ICoordinates result)
        {
            result = default;
            WorldCoordinateParser worldCoordinateParser = new WorldCoordinateParser(StringReader, UseBedrock);

            ReadResults readResults = worldCoordinateParser.ReadInteger(out WorldCoordinate x);
            if (!readResults.Successful) return readResults;
            if (UseBedrock) StringReader.SkipWhitespace();
            else
            {
                if (!StringReader.AtEndOfArgument())
                {
                    StringReader.SetCursor(Start);
                    return ReadResults.Failure(CommandError.Vec2CoordinatesIncomplete().WithContext(StringReader));
                }
                StringReader.Skip();
            }

            readResults = worldCoordinateParser.ReadInteger(out WorldCoordinate z);
            if (readResults.Successful) result = new WorldCoordinates(x, new WorldCoordinate(0.0d, true), z);
            return readResults;
        }

        public ReadResults ParseDouble(out ICoordinates result)
        {
            result = default;
            WorldCoordinateParser worldCoordinateParser = new WorldCoordinateParser(StringReader, UseBedrock);

            ReadResults readResults = worldCoordinateParser.ReadDouble(out WorldCoordinate x);
            if (!readResults.Successful) return readResults;
            if (UseBedrock) StringReader.SkipWhitespace();
            else
            {
                if (!StringReader.AtEndOfArgument())
                {
                    StringReader.SetCursor(Start);
                    return ReadResults.Failure(CommandError.Vec3CoordinatesIncomplete().WithContext(StringReader));
                }
                StringReader.Skip();
            }

            readResults = worldCoordinateParser.ReadDouble(out WorldCoordinate y);
            if (!readResults.Successful) return readResults;
            if (UseBedrock) StringReader.SkipWhitespace();
            else
            {
                if (!StringReader.AtEndOfArgument())
                {
                    StringReader.SetCursor(Start);
                    return ReadResults.Failure(CommandError.Vec3CoordinatesIncomplete().WithContext(StringReader));
                }
                StringReader.Skip();
            }

            readResults = worldCoordinateParser.ReadDouble(out WorldCoordinate z);
            if (readResults.Successful) result = new WorldCoordinates(x, y, z);
            return readResults;
        }

        public ReadResults ParseDoubleFlat(out ICoordinates result)
        {
            result = default;
            WorldCoordinateParser worldCoordinateParser = new WorldCoordinateParser(StringReader, UseBedrock);

            ReadResults readResults = worldCoordinateParser.ReadDouble(out WorldCoordinate x);
            if (!readResults.Successful) return readResults;
            if (UseBedrock) StringReader.SkipWhitespace();
            else
            {
                if (!StringReader.AtEndOfArgument())
                {
                    StringReader.SetCursor(Start);
                    return ReadResults.Failure(CommandError.Vec2CoordinatesIncomplete().WithContext(StringReader));
                }
                StringReader.Skip();
            }

            readResults = worldCoordinateParser.ReadDouble(out WorldCoordinate z);
            if (readResults.Successful) result = new WorldCoordinates(x, new WorldCoordinate(0.0d, true), z);
            return readResults;
        }
    }
}
