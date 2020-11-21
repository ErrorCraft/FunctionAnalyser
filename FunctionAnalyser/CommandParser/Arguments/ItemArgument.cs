using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using Newtonsoft.Json;

namespace CommandParser.Arguments
{
    public class ItemArgument : IArgument<Item>
    {
        [JsonProperty("for_testing")]
        private readonly bool ForTesting;

        public ItemArgument(bool forTesting = false)
        {
            ForTesting = forTesting;
        }

        public ReadResults Parse(StringReader reader, out Item result)
        {
            return new ItemParser(reader, ForTesting).Parse(out result);
        }
    }
}
