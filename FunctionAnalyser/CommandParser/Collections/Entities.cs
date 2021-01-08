using CommandParser.Results.Arguments;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class Entities
    {
        private static HashSet<string> Options = new HashSet<string>();
        private readonly HashSet<string> Values;

        public Entities(HashSet<string> values)
        {
            Values = values;
        }

        public static void Set(string json)
        {
            Options = JsonConvert.DeserializeObject<HashSet<string>>(json);
        }

        public static bool Contains(ResourceLocation entity)
        {
            return entity.IsDefaultNamespace() && Options.Contains(entity.Path);
        }
    }
}
