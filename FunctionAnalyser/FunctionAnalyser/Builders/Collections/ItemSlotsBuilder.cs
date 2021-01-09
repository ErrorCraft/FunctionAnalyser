using CommandParser.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FunctionAnalyser.Builders.Collections
{
    public class ItemSlotsBuilder : IBuilder<ItemSlotsBuilder, ItemSlots>
    {
        [JsonProperty("parent")]
        private readonly string Parent;
        [JsonProperty("values")]
        private readonly HashSet<string> Values;

        public ItemSlots Build(Dictionary<string, ItemSlotsBuilder> resources)
        {
            HashSet<string> all = new HashSet<string>(Values);
            ItemSlotsBuilder builder = this;
            while (builder.Parent != null)
            {
                builder = resources[builder.Parent];
                foreach (string s in builder.Values) all.Add(s);
            }
            return new ItemSlots(all);
        }
    }
}
