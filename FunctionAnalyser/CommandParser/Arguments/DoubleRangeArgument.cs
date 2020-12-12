using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using Newtonsoft.Json;

namespace CommandParser.Arguments
{
    public class DoubleRangeArgument : IArgument<Range<double>>
    {
        [JsonProperty("loopable")]
        private readonly bool Loopable;

        public DoubleRangeArgument(bool loopable = false)
        {
            Loopable = loopable;
        }

        public ReadResults Parse(IStringReader reader, out Range<double> result)
        {
            return new RangeParser<double>(reader).Read(double.TryParse, CommandError.InvalidDouble, double.NegativeInfinity, double.PositiveInfinity, Loopable, out result);
        }
    }
}
