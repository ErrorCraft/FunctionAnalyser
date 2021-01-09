using CommandParser.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FunctionAnalyser.Builders.Collections
{
    public class EntitiesBuilder : IBuilder<EntitiesBuilder, Entities>
    {
        [JsonProperty("parent")]
        private readonly string Parent;
        [JsonProperty("values")]
        private readonly HashSet<string> Values;

        public Entities Build(Dictionary<string, EntitiesBuilder> resources)
        {
            HashSet<string> all = new HashSet<string>(Values);
            EntitiesBuilder builder = this;
            while (builder.Parent != null)
            {
                builder = resources[builder.Parent];
                foreach (string s in builder.Values) all.Add(s);
            }
            return new Entities(all);
        }
    }
}
