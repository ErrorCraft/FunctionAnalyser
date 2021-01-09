using CommandParser.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FunctionAnalyser.Builders.Collections
{
    public class GamemodesBuilder : IBuilder<GamemodesBuilder, Gamemodes>
    {
        [JsonProperty("parent")]
        private readonly string Parent;
        [JsonProperty("values")]
        private readonly HashSet<string> Values;

        public Gamemodes Build(Dictionary<string, GamemodesBuilder> resources)
        {
            HashSet<string> all = new HashSet<string>(Values);
            GamemodesBuilder builder = this;
            while (builder.Parent != null)
            {
                builder = resources[builder.Parent];
                foreach (string s in builder.Values) all.Add(s);
            }
            return new Gamemodes(all);
        }
    }
}
