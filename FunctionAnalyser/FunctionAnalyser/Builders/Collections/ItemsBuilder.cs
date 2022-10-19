using CommandParser.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ErrorCraft.PackAnalyser.Builders.Collections {
    public class ItemsBuilder : IBuilder<ItemsBuilder, Items> {
        [JsonProperty("parent")]
        private readonly string Parent;
        [JsonProperty("values")]
        private readonly HashSet<string> Values;

        public Items Build(Dictionary<string, ItemsBuilder> resources) {
            HashSet<string> all = new HashSet<string>(Values);
            ItemsBuilder builder = this;
            while (builder.Parent != null) {
                builder = resources[builder.Parent];
                foreach (string s in builder.Values) all.Add(s);
            }
            return new Items(all);
        }
    }
}
