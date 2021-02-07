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

        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out float result)
        {
            int start = reader.GetCursor();
            ReadResults readResults = reader.ReadFloat(out result);
            if (readResults.Successful)
            {
                if (result < Minimum)
                {
                    reader.SetCursor(start);
                    return ReadResults.Failure(CommandError.FloatTooLow(result, Minimum).WithContext(reader));
                }

                if (result > Maximum)
                {
                    reader.SetCursor(start);
                    return ReadResults.Failure(CommandError.FloatTooHigh(result, Maximum).WithContext(reader));
                }
            }

            return readResults;
        }
    }
}
