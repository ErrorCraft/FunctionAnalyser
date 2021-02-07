using CommandParser.Minecraft;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class Blocks
    {
        private readonly Dictionary<string, BlockState> Values;

        public Blocks() : this(new Dictionary<string, BlockState>()) { }

        public Blocks(Dictionary<string, BlockState> values)
        {
            Values = values;
        }

        public bool ContainsBlock(ResourceLocation item)
        {
            return item.IsDefaultNamespace() && Values.ContainsKey(item.Path);
        }

        public bool ContainsProperty(ResourceLocation item, string property)
        {
            return Values[item.Path].ContainsProperty(property);
        }

        public bool PropertyContainsValue(ResourceLocation item, string property, string value)
        {
            return Values[item.Path].PropertyContainsValue(property, value);
        }
    }
}
