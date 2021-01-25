using CommandParser.Parsers.Coordinates;
using CommandParser.Results;
using CommandParser.Results.Arguments.Coordinates;
using Newtonsoft.Json;

namespace CommandParser.Arguments
{
    public class ColumnPosArgument : IArgument<ICoordinates>
    {
        [JsonProperty("use_bedrock")]
        private readonly bool UseBedrock;

        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out ICoordinates result)
        {
            return new WorldCoordinatesParser(reader, UseBedrock).ParseIntegerFlat(out result);
        }
    }
}
