using System.Collections.Generic;

namespace CommandVerifier.NbtParser.Types
{
    class NbtCompound : INbtArgument
    {
        public Dictionary<string, INbtArgument> Values { get; private set; }

        public NbtCompound()
        {
            Values = new Dictionary<string, INbtArgument>();
        }

        public void Add(string key, INbtArgument value)
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
                s += INbtArgument.TryQuote(key) + ": " + Values[key].Get() + ", ";
            }
            return s.TrimEnd(new char[] { ',', ' ' }) + "}";
        }
        public string Id { get; } = "TAG_Compound";
    }
}
