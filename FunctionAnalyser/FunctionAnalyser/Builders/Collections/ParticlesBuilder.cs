using CommandParser.Collections;
using CommandParser.Tree;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ErrorCraft.PackAnalyser.Builders.Collections {
    public class ParticlesBuilder : IBuilder<ParticlesBuilder, Particles> {
        [JsonProperty("parent")]
        private readonly string Parent;
        [JsonProperty("values")]
        private readonly Dictionary<string, RootNode> Values;

        public Particles Build(Dictionary<string, ParticlesBuilder> resources) {
            Dictionary<string, RootNode> all = new Dictionary<string, RootNode>(Values);
            ParticlesBuilder builder = this;
            while (builder.Parent != null) {
                builder = resources[builder.Parent];
                foreach (KeyValuePair<string, RootNode> pair in builder.Values) all.Add(pair.Key, pair.Value);
            }
            return new Particles(all);
        }
    }
}
