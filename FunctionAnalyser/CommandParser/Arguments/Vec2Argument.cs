using CommandParser.Minecraft.Coordinates;
using CommandParser.Parsers.Coordinates;
using CommandParser.Results;
using Newtonsoft.Json;

namespace CommandParser.Arguments {
    public class Vec2Argument : IArgument<ICoordinates> {
        [JsonProperty("use_bedrock")]
        private readonly bool UseBedrock;

        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out ICoordinates result) {
            return new WorldCoordinatesParser(reader, UseBedrock).ParseDoubleFlat(out result);
        }
    }
}
