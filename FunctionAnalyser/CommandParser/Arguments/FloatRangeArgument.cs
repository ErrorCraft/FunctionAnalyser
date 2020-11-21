using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using Newtonsoft.Json;

namespace CommandParser.Arguments
{
    class FloatRangeArgument : IArgument<Range<float>>
    {
        [JsonProperty("loopable")]
        private readonly bool Loopable;

        public FloatRangeArgument(bool loopable = false)
        {
            Loopable = loopable;
        }

        public ReadResults Parse(StringReader reader, out Range<float> result)
        {
            return new RangeParser<float>(reader).Read(float.TryParse, CommandError.InvalidFloat, float.NegativeInfinity, float.PositiveInfinity, Loopable, out result);
        }
    }
}
