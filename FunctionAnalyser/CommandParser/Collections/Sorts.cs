using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class Sorts
    {
        private readonly HashSet<string> Values;

        public Sorts(HashSet<string> values)
        {
            Values = values;
        }

        public bool Contains(string gamemode)
        {
            return Values.Contains(gamemode);
        }
    }
}
