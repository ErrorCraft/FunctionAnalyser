using System.Collections.Generic;

namespace CommandVerifier.NbtParser.Types
{
    class NbtLongArray : NbtCollection
    {
        public List<NbtLong> Values { get; private set; }

        public NbtLongArray()
        {
            Values = new List<NbtLong>();
        }

        public bool TryAdd(NbtArgument value)
        {
            if (typeof(NbtLong) != value.GetType()) return false;
            Values.Add((NbtLong)value);
            return true;
        }

        public string Get()
        {
            string s = "[L; ";
            for (int i = 0; i < Values.Count; i++)
            {
                s += Values[i].Get() + ", ";
            }
            return s.TrimEnd(new char[] { ',', ' ' }) + "]";
        }
        public string Id { get; } = "TAG_Long_Array";
    }
}
