using CommandParser.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FunctionAnalyser.Builders.Collections
{
    public class MobEffectsBuilder
    {
        [JsonProperty("parent")]
        private readonly string Parent;
        [JsonProperty("values")]
        private readonly HashSet<string> Values;

        public MobEffects Build(Dictionary<string, MobEffectsBuilder> resources)
        {
            HashSet<string> all = new HashSet<string>(Values);
            MobEffectsBuilder builder = this;
            while (builder.Parent != null)
            {
                builder = resources[builder.Parent];
                foreach (string s in builder.Values) all.Add(s);
            }
            return new MobEffects(all);
        }
    }
}
