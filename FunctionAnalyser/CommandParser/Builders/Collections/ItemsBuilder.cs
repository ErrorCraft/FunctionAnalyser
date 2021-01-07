using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandParser.Builders.Collections
{
    public class ItemsBuilder
    {
        [JsonProperty("parent")]
        private readonly string Parent;
        [JsonProperty("values")]
        private readonly HashSet<string> Values;

        public string GetParent()
        {
            return Parent;
        }

        public HashSet<string> GetValues()
        {
            return Values;
        }

        public override string ToString()
        {
            return string.Join(", ", Values);
        }
    }
}
