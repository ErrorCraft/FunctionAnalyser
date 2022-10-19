using CommandParser.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ErrorCraft.PackAnalyser.Builders.Collections {
    public class OperationsBuilder : IBuilder<OperationsBuilder, Operations> {
        [JsonProperty("parent")]
        private readonly string Parent;
        [JsonProperty("values")]
        private readonly HashSet<string> Values;

        public Operations Build(Dictionary<string, OperationsBuilder> resources) {
            HashSet<string> all = new HashSet<string>(Values);
            OperationsBuilder builder = this;
            while (builder.Parent != null) {
                builder = resources[builder.Parent];
                foreach (string s in builder.Values) all.Add(s);
            }
            return new Operations(all);
        }
    }
}
