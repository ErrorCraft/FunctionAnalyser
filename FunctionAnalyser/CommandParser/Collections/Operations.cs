using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class Operations
    {
        private readonly HashSet<string> Values;

        public Operations() : this(new HashSet<string>()) { }

        public Operations(HashSet<string> values)
        {
            Values = values;
        }

        public bool Contains(string operation)
        {
            return Values.Contains(operation);
        }
    }
}
