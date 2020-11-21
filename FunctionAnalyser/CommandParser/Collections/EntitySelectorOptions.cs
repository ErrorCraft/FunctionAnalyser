using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public static class EntitySelectorOptions
    {
        private static Dictionary<string, EntitySelectorOption> Options = new Dictionary<string, EntitySelectorOption>();
        
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
