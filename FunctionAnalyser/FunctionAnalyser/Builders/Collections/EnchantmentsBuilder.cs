using CommandParser.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FunctionAnalyser.Builders.Collections
{
    public class EnchantmentsBuilder
    {
        [JsonProperty("parent")]
        private readonly string Parent;
        [JsonProperty("values")]
        private readonly HashSet<string> Values;

        public Enchantments Build(Dictionary<string, EnchantmentsBuilder> resources)
        {
            HashSet<string> all = new HashSet<string>(Values);
            EnchantmentsBuilder builder = this;
            while (builder.Parent != null)
            {
                builder = resources[builder.Parent];
                foreach (string s in builder.Values) all.Add(s);
            }
            return new Enchantments(all);
        }
    }
}
