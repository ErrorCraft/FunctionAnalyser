using CommandParser.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ErrorCraft.PackAnalyser.Builders.Collections {
    public class ColoursBuilder : IBuilder<ColoursBuilder, Colours> {
        [JsonProperty("parent")]
        private readonly string Parent;
        [JsonProperty("values")]
        private readonly HashSet<string> Values;

        public Colours Build(Dictionary<string, ColoursBuilder> resources) {
            HashSet<string> all = new HashSet<string>(Values);
            ColoursBuilder builder = this;
            while (builder.Parent != null) {
                builder = resources[builder.Parent];
                foreach (string s in builder.Values) all.Add(s);
            }
            return new Colours(all);
        }
    }
}
