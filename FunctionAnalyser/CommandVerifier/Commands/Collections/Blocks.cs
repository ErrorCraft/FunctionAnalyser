using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandVerifier.Commands.Collections
{
    public class Blocks
    {
        public static Dictionary<string, Dictionary<string, HashSet<string>>> Options { get; private set; }

        static Blocks()
        {
            Options = new Dictionary<string, Dictionary<string, HashSet<string>>>();
        }

        public static void SetOptions(string json)
        {
            Options = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, HashSet<string>>>>(json);
        }
    }
}
