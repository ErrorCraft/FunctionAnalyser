using CommandParser.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FunctionAnalyser.Builders.Collections
{
    public class TimeScalarsBuilder
    {
        [JsonProperty("parent")]
        private readonly string Parent;
        [JsonProperty("values")]
        private readonly Dictionary<char, int> Values;

        public TimeScalars Build(Dictionary<string, TimeScalarsBuilder> resources)
        {
            Dictionary<char, int> all = new Dictionary<char, int>(Values);
            TimeScalarsBuilder builder = this;
            while (builder.Parent != null)
            {
                builder = resources[builder.Parent];
                foreach (KeyValuePair<char, int> pair in builder.Values) all.Add(pair.Key, pair.Value);
            }
            return new TimeScalars(all);
        }
    }
}
