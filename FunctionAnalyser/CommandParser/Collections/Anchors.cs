using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class Anchors
    {
        private readonly HashSet<string> Values;

        public Anchors() : this(new HashSet<string>()) { }

        public Anchors(HashSet<string> values)
        {
            Values = values;
        }

        public bool Contains(string anchor)
        {
            return Values.Contains(anchor);
        }
    }
}
