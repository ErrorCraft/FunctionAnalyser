using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class Gamemodes
    {
        private readonly HashSet<string> Values;

        public Gamemodes(HashSet<string> values)
        {
            Values = values;
        }

        public bool Contains(string gamemode)
        {
            return Values.Contains(gamemode);
        }
    }
}
