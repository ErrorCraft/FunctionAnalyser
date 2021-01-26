using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using Newtonsoft.Json;

namespace CommandParser.Arguments
{
    public class BlockArgument : IArgument<Block>
    {
        [JsonProperty("for_testing")]
        private readonly bool ForTesting;

        [JsonProperty("use_bedrock")]
        private readonly bool UseBedrock;

        public BlockArgument(bool forTesting = false, bool useBedrock = false)
        {
            ForTesting = forTesting;
            UseBedrock = useBedrock;
        }

        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Block result)
        {
            return new BlockParser(reader, ForTesting, resources, UseBedrock).Parse(out result);
        }
    }
}
