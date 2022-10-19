using CommandParser.Collections;
using CommandParser.Parsers.ComponentParser.ComponentArguments;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ErrorCraft.PackAnalyser.Builders.Collections {
    public class ComponentsBuilder : IBuilder<ComponentsBuilder, Components> {
        [JsonProperty("parent")]
        private readonly string Parent;
        [JsonProperty("root")]
        private readonly ComponentArgument Root;
        [JsonProperty("primary")]
        private readonly Dictionary<string, ComponentArgument> Primary;
        [JsonProperty("optional")]
        private readonly Dictionary<string, ComponentArgument> Optional;

        public Components Build(Dictionary<string, ComponentsBuilder> resources) {
            ComponentArgument root = Root;
            Dictionary<string, ComponentArgument> primary = new Dictionary<string, ComponentArgument>(Primary);
            Dictionary<string, ComponentArgument> optional = new Dictionary<string, ComponentArgument>(Optional);
            ComponentsBuilder builder = this;
            while (builder.Parent != null) {
                builder = resources[builder.Parent];
                if (builder.Root != null) root = builder.Root;
                foreach (KeyValuePair<string, ComponentArgument> pair in builder.Primary) primary.Add(pair.Key, pair.Value);
                foreach (KeyValuePair<string, ComponentArgument> pair in builder.Optional) optional.Add(pair.Key, pair.Value);
            }
            return new Components(root, primary, optional);
        }
    }
}
