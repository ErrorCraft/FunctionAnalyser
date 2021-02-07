using CommandParser.Minecraft;
using CommandParser.Tree;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class Particles
    {
        private readonly Dictionary<string, RootNode> Values;

        public Particles() : this(new Dictionary<string, RootNode>()) { }

        public Particles(Dictionary<string, RootNode> values)
        {
            Values = values;
        }

        public bool TryGetNodes(ResourceLocation item, out Dictionary<string, Node> nodes)
        {
            nodes = default;
            if (!item.IsDefaultNamespace() || !Values.TryGetValue(item.Path, out RootNode root)) return false;
            nodes = root.Children ?? new Dictionary<string, Node>();
            return true;
        }
    }
}
