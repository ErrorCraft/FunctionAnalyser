using CommandParser.Minecraft;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class Items
    {
        private readonly HashSet<string> Values;

        public Items() : this(new HashSet<string>()) { }

        public Items(HashSet<string> values)
        {
            Values = values;
        }

        public bool Contains(ResourceLocation item)
        {
            return item.IsDefaultNamespace() && Values.Contains(item.Path);
        }
    }
}
