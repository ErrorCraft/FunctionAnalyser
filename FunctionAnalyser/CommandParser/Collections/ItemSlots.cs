using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public static class ItemSlots
    {
        private static HashSet<string> Options = new HashSet<string>();

        public static void Set(string json)
        {
            Options = JsonConvert.DeserializeObject<HashSet<string>>(json);
        }

        public static bool Contains(string slot)
        {
            return Options.Contains(slot);
        }
    }
}
