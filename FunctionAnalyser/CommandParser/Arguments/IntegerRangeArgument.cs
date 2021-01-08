using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using Newtonsoft.Json;

namespace CommandParser.Arguments
{
    public class IntegerRangeArgument : IArgument<Range<int>>
    {
        [JsonProperty("loopable")]
        private readonly bool Loopable;

        public IntegerRangeArgument(bool loopable = false)
        {
            Loopable = loopable;
        }

        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Range<int> result)
        {
            return new RangeParser<int>(reader).Read(int.TryParse, CommandError.InvalidInteger, int.MinValue, int.MaxValue, Loopable, out result);
        }
    }
}
