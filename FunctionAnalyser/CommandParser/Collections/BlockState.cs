using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class BlockState : Dictionary<string, HashSet<string>>
    {
        public bool ContainsProperty(string property)
        {
            return ContainsKey(property);
        }

        public bool PropertyContainsValue(string property, string value)
        {
            return this[property].Contains(value);
        }
    }
}
