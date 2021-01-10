using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class ScoreboardSlots
    {
        private readonly Dictionary<string, ScoreboardSlot> Values;

        public ScoreboardSlots(Dictionary<string, ScoreboardSlot> values)
        {
            Values = values;
        }

        public bool TryGetSlot(string slot, out ScoreboardSlot result)
        {
            return Values.TryGetValue(slot, out result);
        }
    }
}
