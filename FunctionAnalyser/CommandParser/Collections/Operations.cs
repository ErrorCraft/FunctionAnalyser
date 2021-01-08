using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class Operations
    {
        private static HashSet<string> Options = new HashSet<string>();
        private readonly HashSet<string> Values;

        public Operations(HashSet<string> values)
        {
            Values = values;
        }

        public static void Set(string json)
        {
            Options = JsonConvert.DeserializeObject<HashSet<string>>(json);
        }

        public static bool Contains(string operation)
        {
            return Options.Contains(operation);
        }
    }
}
