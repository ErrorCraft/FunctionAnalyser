using CommandParser.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FunctionAnalyser.Builders.Collections
{
    public class ScoreboardSlotsBuilder
    {
        [JsonProperty("parent")]
        private readonly string Parent;
        [JsonProperty("values")]
        private readonly Dictionary<string, ScoreboardSlot> Values;

        public ScoreboardSlots Build(Dictionary<string, ScoreboardSlotsBuilder> resources)
        {
            Dictionary<string, ScoreboardSlot> all = new Dictionary<string, ScoreboardSlot>(Values);
            ScoreboardSlotsBuilder builder = this;
            while (builder.Parent != null)
            {
                builder = resources[builder.Parent];
                foreach (KeyValuePair<string, ScoreboardSlot> pair in builder.Values) all.Add(pair.Key, pair.Value);
            }
            return new ScoreboardSlots(all);
        }
    }
}
