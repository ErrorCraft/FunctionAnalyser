using System.Collections.Generic;

namespace CommandVerifier.NbtParser.Types
{
    class NbtByteArray : NbtCollection
    {
        public List<NbtByte> Values { get; private set; }

        public NbtByteArray()
        {
            Values = new List<NbtByte>();
        }

        public bool TryAdd(NbtArgument value)
        {
            if (typeof(NbtByte) != value.GetType()) return false;
            Values.Add((NbtByte)value);
            return true;
        }

        public string Get()
        {
            string s = "[B; ";
            for (int i = 0; i < Values.Count; i++)
            {
                s += Values[i].Get() + ", ";
            }
            return s.TrimEnd(new char[] { ',', ' ' }) + "]";
        }
        public string Id { get; } = "TAG_Byte_Array";
    }
}
