using CommandParser.Tree;
using Newtonsoft.Json;

namespace CommandParser.Builders.Collections
{
    public class CommandsBuilder
    {
        [JsonProperty("parent")]
        private readonly string Parent;
        [JsonProperty("root")]
        private readonly RootNode Root;

        public string GetParent()
        {
            return Parent;
        }

        public RootNode GetRootNode()
        {
            return Root;
        }
    }
}
