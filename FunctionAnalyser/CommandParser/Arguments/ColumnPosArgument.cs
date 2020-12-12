using CommandParser.Parsers.Coordinates;
using CommandParser.Results;
using CommandParser.Results.Arguments.Coordinates;

namespace CommandParser.Arguments
{
    public class ColumnPosArgument : IArgument<ICoordinates>
    {
        public ReadResults Parse(IStringReader reader, out ICoordinates result)
        {
            result = default;
            if (!reader.CanRead())
            {
                return new ReadResults(false, CommandError.Vec2CoordinatesIncomplete().WithContext(reader));
            }

            int start = reader.GetCursor();
            WorldCoordinateParser worldCoordinateParser = new WorldCoordinateParser(reader);

            ReadResults readResults = worldCoordinateParser.ReadInteger(out WorldCoordinate x);
            if (!readResults.Successful) return readResults;
            if (!reader.AtEndOfArgument())
            {
                reader.SetCursor(start);
                return new ReadResults(false, CommandError.Vec2CoordinatesIncomplete().WithContext(reader));
            }
            reader.Skip();

            readResults = worldCoordinateParser.ReadInteger(out WorldCoordinate z);
            if (readResults.Successful) result = new WorldCoordinates(x, new WorldCoordinate(0.0d, true), z);
            return readResults;
        }
    }
}
