using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class EntitySelectorOptions
    {
        private readonly Dictionary<string, EntitySelectorOption> Values;

        public EntitySelectorOptions() : this(new Dictionary<string, EntitySelectorOption>()) { }

        public EntitySelectorOptions(Dictionary<string, EntitySelectorOption> values)
        {
            Values = values;
        }

        public bool TryGet(string input, out EntitySelectorOption option)
        {
            return Values.TryGetValue(input, out option);
        }
    }
}
