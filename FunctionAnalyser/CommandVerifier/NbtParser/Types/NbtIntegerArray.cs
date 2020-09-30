using System.Collections.Generic;

namespace CommandVerifier.NbtParser.Types
{
    class NbtIntegerArray : NbtCollection
    {
        public List<NbtInteger> Values { get; private set; }

        public NbtIntegerArray()
        {
            Values = new List<NbtInteger>();
        }

        public bool TryAdd(NbtArgument value)
        {
            if (typeof(NbtInteger) != value.GetType()) return false;
            Values.Add((NbtInteger)value);
            return true;
        }

        public string Get()
        {
            string s = "[I; ";
            for (int i = 0; i < Values.Count; i++)
            {
                s += Values[i].Get() + ", ";
            }
            return s.TrimEnd(new char[] { ',', ' ' }) + "]";
        }
        public string Id { get; } = "TAG_Integer_Array";
    }
}
