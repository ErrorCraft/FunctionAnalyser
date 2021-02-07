using CommandParser.Minecraft;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class Gamemodes
    {
        private readonly Dictionary<GameType, HashSet<string>> Values;

        public Gamemodes() : this(new Dictionary<GameType, HashSet<string>>()) { }

        public Gamemodes(Dictionary<GameType, HashSet<string>> values)
        {
            Values = values;
        }

        public bool TryGet(string gamemode, out GameType result)
        {
            foreach (KeyValuePair<GameType, HashSet<string>> pair in Values)
            {
                if (pair.Value.Contains(gamemode))
                {
                    result = pair.Key;
                    return true;
                }
            }
            result = default;
            return false;
        }
    }
}
