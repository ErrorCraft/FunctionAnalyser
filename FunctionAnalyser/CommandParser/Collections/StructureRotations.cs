using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class StructureRotations
    {
        private readonly HashSet<string> Values;

        public StructureRotations() : this(new HashSet<string>()) { }

        public StructureRotations(HashSet<string> values)
        {
            Values = values;
        }

        public bool Contains(string structureRotation)
        {
            return Values.Contains(structureRotation);
        }
    }
}
