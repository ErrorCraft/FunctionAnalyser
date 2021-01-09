using CommandParser.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FunctionAnalyser.Builders.Collections
{
    public class BlocksBuilder : IBuilder<BlocksBuilder, Blocks>
    {
        [JsonProperty("parent")]
        private readonly string Parent;
        [JsonProperty("values")]
        private readonly Dictionary<string, BlockState> Values;

        public Blocks Build(Dictionary<string, BlocksBuilder> resources)
        {
            Dictionary<string, BlockState> all = new Dictionary<string, BlockState>(Values);
            BlocksBuilder builder = this;
            while (builder.Parent != null)
            {
                builder = resources[builder.Parent];
                foreach (KeyValuePair<string, BlockState> pair in builder.Values) all.Add(pair.Key, pair.Value);
            }
            return new Blocks(all);
        }
    }
}
