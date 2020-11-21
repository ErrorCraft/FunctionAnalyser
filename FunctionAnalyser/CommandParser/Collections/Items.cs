using CommandParser.Results.Arguments;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public static class Items
    {
        private static HashSet<string> Options = new HashSet<string>();

        public static void Set(string json)
        {
            Options = JsonConvert.DeserializeObject<HashSet<string>>(json);
        }

        public static bool Contains(ResourceLocation item)
        {
            return item.IsDefaultNamespace() && Options.Contains(item.Path);
        }
    }
}
