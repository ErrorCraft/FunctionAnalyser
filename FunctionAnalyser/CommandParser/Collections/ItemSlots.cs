using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class ItemSlots
    {
        private readonly HashSet<string> Values;

        public ItemSlots() : this(new HashSet<string>()) { }

        public ItemSlots(HashSet<string> values)
        {
            Values = values;
        }

        public bool Contains(string slot)
        {
            return Values.Contains(slot);
        }
    }
}
