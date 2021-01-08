using CommandParser.Collections;
using CommandParser.Parsers.ComponentParser.ComponentArguments;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FunctionAnalyser.Builders.Collections
{
    public class ComponentsBuilder
    {
        [JsonProperty("parent")]
        private readonly string Parent;
        [JsonProperty("content")]
        private readonly Dictionary<string, ComponentArgument> Content;
        [JsonProperty("children")]
        private readonly Dictionary<string, ComponentArgument> Children;
        [JsonProperty("formatting")]
        private readonly Dictionary<string, ComponentArgument> Formatting;
        [JsonProperty("interactivity")]
        private readonly Dictionary<string, ComponentArgument> Interactivity;

        public Components Build(Dictionary<string, ComponentsBuilder> resources)
        {
            Dictionary<string, ComponentArgument> content = new Dictionary<string, ComponentArgument>(Content);
            Dictionary<string, ComponentArgument> children = new Dictionary<string, ComponentArgument>(Children);
            Dictionary<string, ComponentArgument> formatting = new Dictionary<string, ComponentArgument>(Formatting);
            Dictionary<string, ComponentArgument> interactivity = new Dictionary<string, ComponentArgument>(Interactivity);
            ComponentsBuilder builder = this;
            while (builder.Parent != null)
            {
                builder = resources[builder.Parent];
                foreach (KeyValuePair<string, ComponentArgument> pair in builder.Content) content.Add(pair.Key, pair.Value);
                foreach (KeyValuePair<string, ComponentArgument> pair in builder.Children) children.Add(pair.Key, pair.Value);
                foreach (KeyValuePair<string, ComponentArgument> pair in builder.Formatting) formatting.Add(pair.Key, pair.Value);
                foreach (KeyValuePair<string, ComponentArgument> pair in builder.Interactivity) interactivity.Add(pair.Key, pair.Value);
            }
            return new Components(content, children, formatting, interactivity);
        }
    }
}
