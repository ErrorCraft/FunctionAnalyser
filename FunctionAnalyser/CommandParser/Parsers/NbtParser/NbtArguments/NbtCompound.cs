using System.Collections.Generic;

namespace CommandParser.Parsers.NbtParser.NbtArguments
{
    public class NbtCompound : INbtArgument
    {
        private readonly Dictionary<string, INbtArgument> Contents;

        public NbtCompound()
        {
            Contents = new Dictionary<string, INbtArgument>();
        }

        public void Add(string key, INbtArgument value)
        {
            if (Contents.ContainsKey(key)) Contents[key] = value;
            else Contents.Add(key, value);
        }

        public string GetName()
        {
            return "TAG_Compound";
        }

        public string ToSnbt()
        {
            string snbt = "{";
            foreach (string key in Contents.Keys)
            {
                snbt += key + ": " + Contents[key].ToSnbt() + ", ";
            }

            return snbt.TrimEnd(',', ' ') + "}";
        }
    }
}
