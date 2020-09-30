using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandVerifier.Commands.Collections
{
    public class Enchantments
    {
        public static HashSet<string> Options { get; private set; }

        static Enchantments()
        {
            Options = new HashSet<string>();
        }

        public static void SetOptions(string json)
        {
            Options = JsonConvert.DeserializeObject<HashSet<string>>(json);
        }
    }
}
