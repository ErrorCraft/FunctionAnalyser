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
            WorldCoordinateParser worldCoordinateParser = new WorldCoordinateParser(StringReader);

            ReadResults readResults = worldCoordinateParser.ReadInteger(out WorldCoordinate x);
            if (!readResults.Successful) return readResults;
            if (!StringReader.AtEndOfArgument())
            {
                if (!UseBedrock)
                {
                    StringReader.SetCursor(Start);
                    return new ReadResults(false, CommandError.Vec3CoordinatesIncomplete().WithContext(StringReader));
                }
            } else StringReader.Skip();

            readResults = worldCoordinateParser.ReadInteger(out WorldCoordinate y);
            if (!readResults.Successful) return readResults;
            if (!StringReader.AtEndOfArgument())
            {
                if (!UseBedrock)
                {
                    StringReader.SetCursor(Start);
                    return new ReadResults(false, CommandError.Vec3CoordinatesIncomplete().WithContext(StringReader));
                }
            } else StringReader.Skip();

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
                StringReader.SetCursor(Start);
                return new ReadResults(false, CommandError.Vec3CoordinatesIncomplete().WithContext(StringReader));
            }
            StringReader.Skip();

            readResults = worldCoordinateParser.ReadDouble(out WorldCoordinate y);
            if (!readResults.Successful) return readResults;
            if (!StringReader.AtEndOfArgument())
            {
                StringReader.SetCursor(Start);
                return new ReadResults(false, CommandError.Vec3CoordinatesIncomplete().WithContext(StringReader));
            }
            StringReader.Skip();

            readResults = worldCoordinateParser.ReadDouble(out WorldCoordinate z);
            if (readResults.Successful) result = new WorldCoordinates(x, y, z);
            return readResults;
        }
    }
}
