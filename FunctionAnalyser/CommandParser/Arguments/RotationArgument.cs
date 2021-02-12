using CommandParser.Minecraft.Coordinates;
using CommandParser.Parsers.Coordinates;
using CommandParser.Results;
using Newtonsoft.Json;

namespace CommandParser.Arguments {
    public class RotationArgument : IArgument<Rotation> {
        [JsonProperty("use_bedrock")]
        private readonly bool UseBedrock;

        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Rotation result) {
            return new RotationParser(reader, UseBedrock).Parse(out result);
        }
    }
}
