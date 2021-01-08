using CommandParser.Tree;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FunctionAnalyser.Builders.Collections
{
    public class CommandsBuilder
    {
        [JsonProperty("parent")]
        private readonly string Parent;
        [JsonProperty("root")]
        private readonly RootNode Root;

        public RootNode Build(Dictionary<string, CommandsBuilder> resources)
        {
            RootNode all = new RootNode();
            all.Merge(Root);

            CommandsBuilder builder = this;
            while (builder.Parent != null)
            {
                builder = resources[builder.Parent];
                all.Merge(builder.Root);
            }
            return all;
        }
    }
}
