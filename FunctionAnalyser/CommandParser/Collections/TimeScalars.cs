using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class TimeScalars
    {
        private readonly Dictionary<char, int> Values;

        public TimeScalars(Dictionary<char, int> values)
        {
            Values = values;
        }

        public bool TryGetScalar(char input, out int scalar)
        {
            return Values.TryGetValue(input, out scalar);
        }
    }
}
