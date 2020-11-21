using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class TimeScalars
    {
        private static Dictionary<char, int> Options = new Dictionary<char, int>();

        public static void Set(string json)
        {
            Options = JsonConvert.DeserializeObject<Dictionary<char, int>>(json);
        }

        public static bool TryGetScalar(char input, out int scalar)
        {
            return Options.TryGetValue(input, out scalar);
        }
    }
}
