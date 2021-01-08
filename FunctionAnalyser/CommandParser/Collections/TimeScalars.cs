using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class TimeScalars
    {
        private static Dictionary<char, int> Options = new Dictionary<char, int>();
        private readonly Dictionary<char, int> Values;

        public TimeScalars(Dictionary<char, int> values)
        {
            Values = values;
        }

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
