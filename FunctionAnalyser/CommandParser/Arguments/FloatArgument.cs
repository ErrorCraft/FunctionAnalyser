using CommandParser.Results;
using Newtonsoft.Json;

namespace CommandParser.Arguments
{
    public class FloatArgument : IArgument<float>
    {
        [JsonProperty("min")]
        private readonly float Minimum;
        [JsonProperty("max")]
        private readonly float Maximum;

        public FloatArgument(float minimum = float.MinValue, float maximum = float.MaxValue)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public ReadResults Parse(StringReader reader, out float result)
        {
            int start = reader.Cursor;
            ReadResults readResults = reader.ReadFloat(out result);
            if (readResults.Successful)
            {
                if (result < Minimum)
                {
                    reader.Cursor = start;
                    return new ReadResults(false, CommandError.FloatTooLow(result, Minimum).WithContext(reader));
                }

                if (result > Maximum)
                {
                    reader.Cursor = start;
                    return new ReadResults(false, CommandError.FloatTooHigh(result, Maximum).WithContext(reader));
                }
            }

            return readResults;
        }
    }
}
