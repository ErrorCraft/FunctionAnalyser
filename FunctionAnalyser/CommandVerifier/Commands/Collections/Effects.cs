using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandVerifier.Commands.Collections
{
    public class Effects
    {
        public static HashSet<string> Options { get; private set; }

        static Effects()
        {
            Options = new HashSet<string>();
        }

        public static void SetOptions(string json)
        {
            Options = JsonConvert.DeserializeObject<HashSet<string>>(json);
        }
    }
}
