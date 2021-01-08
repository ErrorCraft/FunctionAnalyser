using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class Colours
    {
        private static HashSet<string> Options = new HashSet<string>();
        private readonly HashSet<string> Values;

        public Colours(HashSet<string> values)
        {
            Values = values;
        }

        public static void Set(string json)
        {
            Options = JsonConvert.DeserializeObject<HashSet<string>>(json);
        }

        public static bool Contains(string colour)
        {
            return Options.Contains(colour);
        }
    }
}
