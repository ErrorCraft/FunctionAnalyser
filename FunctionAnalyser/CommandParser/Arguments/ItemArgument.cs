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

        [JsonProperty("use_bedrock")]
        private readonly bool UseBedrock;

        public ItemArgument(bool forTesting = false, bool useBedrock = false)
        {
            ForTesting = forTesting;
            UseBedrock = useBedrock;
        }

        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Item result)
        {
            return new ItemParser(reader, resources, ForTesting, UseBedrock).Parse(out result);
        }
    }
}
