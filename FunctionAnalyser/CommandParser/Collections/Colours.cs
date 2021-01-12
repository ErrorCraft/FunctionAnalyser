using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class Colours
    {
        private readonly HashSet<string> Values;

        public Colours() : this(new HashSet<string>()) { }

        public Colours(HashSet<string> values)
        {
            Values = values;
        }

        public bool Contains(string colour)
        {
            return Values.Contains(colour);
        }
    }
}
