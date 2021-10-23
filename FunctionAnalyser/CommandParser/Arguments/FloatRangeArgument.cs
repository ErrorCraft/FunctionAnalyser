using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using Newtonsoft.Json;

namespace CommandParser.Arguments;

public class FloatRangeArgument : IArgument<Range<float>> {
    [JsonProperty("loopable")]
    private readonly bool Loopable;

    public FloatRangeArgument(bool loopable = false) {
        Loopable = loopable;
    }

    public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Range<float> result) {
        return new RangeParser<float>(Loopable).Read(reader, CommandError.InvalidFloat, out result);
    }
}
