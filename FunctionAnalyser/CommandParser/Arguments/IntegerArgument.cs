using CommandParser.Results;
using Newtonsoft.Json;

namespace CommandParser.Arguments
{
    public class IntegerArgument : IArgument<int>
    {
        [JsonProperty("min")]
        private readonly int Minimum;
        [JsonProperty("max")]
        private readonly int Maximum;

        public IntegerArgument(int minimum = int.MinValue, int maximum = int.MaxValue)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out int result)
        {
            int start = reader.GetCursor();
			ReadResults readResults = reader.ReadInteger(out result);
            if (readResults.Successful)
            {
                if (result < Minimum)
                {
                    reader.SetCursor(start);
                    return new ReadResults(false, CommandError.IntegerTooLow(result, Minimum).WithContext(reader));
                }

                if (result > Maximum)
                {
                    reader.SetCursor(start);
                    return new ReadResults(false, CommandError.IntegerTooHigh(result, Maximum).WithContext(reader));
                }
            }

            return readResults;
        }
	}
}
