using CommandParser.Results;
using CommandParser.Results.Arguments.Coordinates;

namespace CommandParser.Parsers.Coordinates
{
    public class WorldCoordinatesParser
    {
        private readonly StringReader StringReader;
        private readonly int Start;

        public WorldCoordinatesParser(StringReader stringReader)
        {
            StringReader = stringReader;
            Start = stringReader.Cursor;
        }

        public ReadResults ParseInteger(out ICoordinates result)
        {
            result = default;
            WorldCoordinateParser worldCoordinateParser = new WorldCoordinateParser(StringReader);

            ReadResults readResults = worldCoordinateParser.ReadInteger(out WorldCoordinate x);
            if (!readResults.Successful) return readResults;
            if (!StringReader.AtEndOfArgument())
            {
                StringReader.Cursor = Start;
                return new ReadResults(false, CommandError.Vec3CoordinatesIncomplete().WithContext(StringReader));
            }
            StringReader.Skip();

            readResults = worldCoordinateParser.ReadInteger(out WorldCoordinate y);
            if (!readResults.Successful) return readResults;
            if (!StringReader.AtEndOfArgument())
            {
                StringReader.Cursor = Start;
                return new ReadResults(false, CommandError.Vec3CoordinatesIncomplete().WithContext(StringReader));
            }
            StringReader.Skip();

            readResults = worldCoordinateParser.ReadInteger(out WorldCoordinate z);
            if (readResults.Successful) result = new WorldCoordinates(x, y, z);
            return readResults;
        }

        public ReadResults ParseDouble(out ICoordinates result)
        {
            result = default;
            WorldCoordinateParser worldCoordinateParser = new WorldCoordinateParser(StringReader);

            ReadResults readResults = worldCoordinateParser.ReadDouble(out WorldCoordinate x);
            if (!readResults.Successful) return readResults;
            if (!StringReader.AtEndOfArgument())
            {
                StringReader.Cursor = Start;
                return new ReadResults(false, CommandError.Vec3CoordinatesIncomplete().WithContext(StringReader));
            }
            StringReader.Skip();

            readResults = worldCoordinateParser.ReadDouble(out WorldCoordinate y);
            if (!readResults.Successful) return readResults;
            if (!StringReader.AtEndOfArgument())
            {
                StringReader.Cursor = Start;
                return new ReadResults(false, CommandError.Vec3CoordinatesIncomplete().WithContext(StringReader));
            }
            StringReader.Skip();

            readResults = worldCoordinateParser.ReadDouble(out WorldCoordinate z);
            if (readResults.Successful) result = new WorldCoordinates(x, y, z);
            return readResults;
        }
    }
}
