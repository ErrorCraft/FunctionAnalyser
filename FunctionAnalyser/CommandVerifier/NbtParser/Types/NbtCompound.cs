using System;
using System.Collections.Generic;
using System.Text;

namespace CommandVerifier.NbtParser.Types
{
    class NbtCompound : NbtArgument
    {
        public Dictionary<string, NbtArgument> Values { get; private set; }

        public NbtCompound()
        {
            Values = new Dictionary<string, NbtArgument>();
        }

        public void Add(string key, NbtArgument value)
        {
            // Overwrite old values
            if (Values.ContainsKey(key)) Values[key] = value;
            else Values.Add(key, value);
        }

        public string Get()
        {
            string s = "{";
            foreach (string key in Values.Keys)
            {
                s += NbtArgument.TryQuote(key) + ": " + Values[key].Get() + ", ";
            }
            return s.TrimEnd(new char[] { ',', ' ' }) + "}";
        }
        public string Id { get; } = "TAG_Compound";
    }
}
