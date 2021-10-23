using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using Newtonsoft.Json;

namespace CommandParser.Arguments;

public class DoubleRangeArgument : IArgument<Range<double>> {
    [JsonProperty("loopable")]
    private readonly bool Loopable;

    public DoubleRangeArgument(bool loopable = false) {
        Loopable = loopable;
    }

    public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Range<double> result) {
        return new RangeParser<double>(Loopable).Read(reader, CommandError.InvalidDouble, out result);
    }
}
