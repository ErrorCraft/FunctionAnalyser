using System.Collections.Generic;

namespace CommandVerifier.NbtParser.Types
{
    class NbtIntegerArray : INbtCollection
    {
        public List<NbtInteger> Values { get; private set; }

        public NbtIntegerArray()
        {
            Values = new List<NbtInteger>();
        }

        public bool TryAdd(INbtArgument value)
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
