using CommandParser.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ErrorCraft.PackAnalyser.Builders.Collections {
    public class StructureMirrorsBuilder : IBuilder<StructureMirrorsBuilder, StructureMirrors> {
        [JsonProperty("parent")]
        private readonly string Parent;
        [JsonProperty("values")]
        private readonly HashSet<string> Values;

        public StructureMirrors Build(Dictionary<string, StructureMirrorsBuilder> resources) {
            HashSet<string> all = new HashSet<string>(Values);
            StructureMirrorsBuilder builder = this;
            while (builder.Parent != null) {
                builder = resources[builder.Parent];
                foreach (string s in builder.Values) all.Add(s);
            }
            return new StructureMirrors(all);
        }
    }
}
