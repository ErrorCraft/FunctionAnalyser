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

        public BlockArgument(bool forTesting = false)
        {
            ForTesting = forTesting;
        }

        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Block result)
        {
            return new BlockParser(reader, ForTesting, resources).Parse(out result);
        }
    }
}
