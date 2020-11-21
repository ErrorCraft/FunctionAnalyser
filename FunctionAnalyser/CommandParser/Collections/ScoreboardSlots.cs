using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public static class ScoreboardSlots
    {
        private static Dictionary<string, ScoreboardSlot> Slots = new Dictionary<string, ScoreboardSlot>();

        public static void Set(string json)
        {
            Slots = JsonConvert.DeserializeObject<Dictionary<string, ScoreboardSlot>>(json);
        }

        public static bool TryGetSlot(string slot, out ScoreboardSlot result)
        {
            return Slots.TryGetValue(slot, out result);
        }
    }
}
