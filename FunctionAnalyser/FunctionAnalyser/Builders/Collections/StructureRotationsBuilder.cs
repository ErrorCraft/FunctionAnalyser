using CommandParser.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FunctionAnalyser.Builders.Collections
{
    public class StructureRotationsBuilder : IBuilder<StructureRotationsBuilder, StructureRotations>
    {
        [JsonProperty("parent")]
        private readonly string Parent;
        [JsonProperty("values")]
        private readonly HashSet<string> Values;

        public StructureRotations Build(Dictionary<string, StructureRotationsBuilder> resources)
        {
            HashSet<string> all = new HashSet<string>(Values);
            StructureRotationsBuilder builder = this;
            while (builder.Parent != null)
            {
                builder = resources[builder.Parent];
                foreach (string s in builder.Values) all.Add(s);
            }
            return new StructureRotations(all);
        }
    }
}
