using CommandParser.Collections;
using CommandParser.Minecraft;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ErrorCraft.PackAnalyser.Builders.Collections {
    public class GamemodesBuilder : IBuilder<GamemodesBuilder, Gamemodes> {
        [JsonProperty("values")]
        private readonly Dictionary<GameType, HashSet<string>> Values;

        public Gamemodes Build(Dictionary<string, GamemodesBuilder> resources) {
            return new Gamemodes(Values);
        }
    }
}
