using System;
using System.Collections.Generic;

namespace CommandVerifier.NbtParser.Types
{
    class NbtList : NbtCollection
    {
        public List<NbtArgument> Values { get; private set; }
        private readonly Type ListType;

        public NbtList(Type t)
        {
            Values = new List<NbtArgument>();
            ListType = t;
        }

        public bool TryAdd(NbtArgument value)
        {
            if (ListType != value.GetType()) return false;
            Values.Add(value);
            if ("TAG_List".Equals(Id)) Id = "list of " + value.Id;
            return true;
        }

        public string Get()
        {
            string s = "[";
            for (int i = 0; i < Values.Count; i++)
            {
                s += Values[i].Get() + ", ";
            }
            return s.TrimEnd(new char[] { ',', ' ' }) + "]";
        }
        public string Id { get; set; } = "TAG_List";
    }
}
