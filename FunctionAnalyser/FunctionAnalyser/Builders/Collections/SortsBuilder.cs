using CommandParser.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FunctionAnalyser.Builders.Collections
{
    public class SortsBuilder
    {
        [JsonProperty("parent")]
        private readonly string Parent;
        [JsonProperty("values")]
        private readonly HashSet<string> Values;

        public Sorts Build(Dictionary<string, SortsBuilder> resources)
        {
            HashSet<string> all = new HashSet<string>(Values);
            SortsBuilder builder = this;
            while (builder.Parent != null)
            {
                builder = resources[builder.Parent];
                foreach (string s in builder.Values) all.Add(s);
            }
            return new Sorts(all);
        }
    }
}
