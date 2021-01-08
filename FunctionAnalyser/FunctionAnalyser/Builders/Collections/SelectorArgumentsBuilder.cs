using CommandParser.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FunctionAnalyser.Builders.Collections
{
    public class SelectorArgumentsBuilder
    {
        [JsonProperty("parent")]
        private readonly string Parent;
        [JsonProperty("values")]
        private readonly Dictionary<string, EntitySelectorOption> Values;

        public EntitySelectorOptions Build(Dictionary<string, SelectorArgumentsBuilder> resources)
        {
            Dictionary<string, EntitySelectorOption> all = new Dictionary<string, EntitySelectorOption>(Values);
            SelectorArgumentsBuilder builder = this;
            while (builder.Parent != null)
            {
                builder = resources[builder.Parent];
                foreach (KeyValuePair<string, EntitySelectorOption> pair in builder.Values) all.Add(pair.Key, pair.Value);
            }
            return new EntitySelectorOptions(all);
        }
    }
}
