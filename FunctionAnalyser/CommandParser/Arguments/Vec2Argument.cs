using CommandParser.Parsers.Coordinates;
using CommandParser.Results;
using CommandParser.Results.Arguments.Coordinates;
using Newtonsoft.Json;

namespace CommandParser.Arguments
{
    public class Vec2Argument : IArgument<ICoordinates>
    {
        [JsonProperty("use_bedrock")]
        private readonly bool UseBedrock;

        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out ICoordinates result)
        {
            result = default;
            if (!reader.CanRead())
            {
                return new ReadResults(false, CommandError.Vec2CoordinatesIncomplete().WithContext(reader));
            }

            int start = reader.GetCursor();
            WorldCoordinateParser worldCoordinateParser = new WorldCoordinateParser(reader);

            ReadResults readResults = worldCoordinateParser.ReadDouble(out WorldCoordinate x);
            if (!readResults.Successful) return readResults;
            if (!reader.AtEndOfArgument())
            {
                if (!UseBedrock)
                {
                    reader.SetCursor(start);
                    return new ReadResults(false, CommandError.Vec2CoordinatesIncomplete().WithContext(reader));
                }
            } else reader.Skip();

            readResults = worldCoordinateParser.ReadDouble(out WorldCoordinate z);
            if (readResults.Successful) result = new WorldCoordinates(x, new WorldCoordinate(0.0d, true), z);
            return readResults;
        }
    }
}
