using CommandParser.Results.Arguments;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class Enchantments
    {
        private readonly HashSet<string> Values;

        public Enchantments() : this(new HashSet<string>()) { }

        public Enchantments(HashSet<string> values)
        {
            Values = values;
        }

        public bool Contains(ResourceLocation enchantment)
        {
            return enchantment.IsDefaultNamespace() && Values.Contains(enchantment.Path);
        }
    }
}
