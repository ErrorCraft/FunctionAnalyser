using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class EntitySelectorOptions
    {
        private static Dictionary<string, EntitySelectorOption> Options = new Dictionary<string, EntitySelectorOption>();
        private readonly Dictionary<string, EntitySelectorOption> Values;

        public EntitySelectorOptions(Dictionary<string, EntitySelectorOption> values)
        {
            Values = values;
        }

        public static void Set(string json)
        {
            Options = JsonConvert.DeserializeObject<Dictionary<string, EntitySelectorOption>>(json);
        }

        public static bool TryGet(string input, out EntitySelectorOption option)
        {
            return Options.TryGetValue(input, out option);
        }
    }
}
