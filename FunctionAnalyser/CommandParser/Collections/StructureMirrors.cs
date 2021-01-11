using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class StructureMirrors
    {
        private readonly HashSet<string> Values;

        public StructureMirrors(HashSet<string> values)
        {
            Values = values;
        }

        public bool Contains(string structureMirror)
        {
            return Values.Contains(structureMirror);
        }
    }
}
