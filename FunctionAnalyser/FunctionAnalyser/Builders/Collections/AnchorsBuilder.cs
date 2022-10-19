using CommandParser.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ErrorCraft.PackAnalyser.Builders.Collections {
    public class AnchorsBuilder : IBuilder<AnchorsBuilder, Anchors> {
        [JsonProperty("parent")]
        private readonly string Parent;
        [JsonProperty("values")]
        private readonly HashSet<string> Values;

        public Anchors Build(Dictionary<string, AnchorsBuilder> resources) {
            HashSet<string> all = new HashSet<string>(Values);
            AnchorsBuilder builder = this;
            while (builder.Parent != null) {
                builder = resources[builder.Parent];
                foreach (string s in builder.Values) all.Add(s);
            }
            return new Anchors(all);
        }
    }
}
