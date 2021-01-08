using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class Gamemodes
    {
        private static HashSet<string> Options = new HashSet<string>();
        private readonly HashSet<string> Values;

        public Gamemodes(HashSet<string> values)
        {
            Values = values;
        }

        public static void Set(string json)
        {
            Options = JsonConvert.DeserializeObject<HashSet<string>>(json);
        }

        public static bool Contains(string gamemode)
        {
            return Options.Contains(gamemode);
        }
    }
}
