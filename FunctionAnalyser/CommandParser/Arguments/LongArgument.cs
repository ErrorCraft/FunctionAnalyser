using CommandParser.Results;
using Newtonsoft.Json;

namespace CommandParser.Arguments
{
    public class LongArgument : IArgument<long>
    {
        [JsonProperty("min")]
        private readonly long Minimum;
        [JsonProperty("max")]
        private readonly long Maximum;

        public LongArgument(long minimum = long.MinValue, long maximum = long.MaxValue)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public ReadResults Parse(StringReader reader, out long result)
        {
            int start = reader.Cursor;
            ReadResults readResults = reader.ReadLong(out result);
            if (readResults.Successful)
            {
                if (result < Minimum)
                {
                    reader.Cursor = start;
                    return new ReadResults(false, CommandError.LongTooLow(result, Minimum).WithContext(reader));
                }

                if (result > Maximum)
                {
                    reader.Cursor = start;
                    return new ReadResults(false, CommandError.LongTooHigh(result, Maximum).WithContext(reader));
                }
            }

            return readResults;
        }
    }
}
