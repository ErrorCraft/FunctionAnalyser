using CommandParser.Results.Arguments;
using CommandParser.Tree;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class Particles
    {
        private static Dictionary<string, RootNode> Options = new Dictionary<string, RootNode>();
        private readonly Dictionary<string, RootNode> Values;

        public Particles(Dictionary<string, RootNode> values)
        {
            Values = values;
        }

        public static void Set(string json)
        {
            Options = JsonConvert.DeserializeObject<Dictionary<string, RootNode>>(json);
        }

        public static bool TryGetNodes(ResourceLocation item, out Dictionary<string, Node> nodes)
        {
            nodes = default;
            if (!item.IsDefaultNamespace() || !Options.TryGetValue(item.Path, out RootNode root)) return false;
            nodes = root.Children ?? new Dictionary<string, Node>();
            return true;
        }
    }
}
