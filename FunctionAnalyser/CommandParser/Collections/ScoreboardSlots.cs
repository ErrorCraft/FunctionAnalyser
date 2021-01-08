using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class ScoreboardSlots
    {
        private static Dictionary<string, ScoreboardSlot> SlotsObsolete = new Dictionary<string, ScoreboardSlot>();
        private readonly Dictionary<string, ScoreboardSlot> Values;

        public ScoreboardSlots(Dictionary<string, ScoreboardSlot> values)
        {
            Values = values;
        }

        public static void Set(string json)
        {
            SlotsObsolete = JsonConvert.DeserializeObject<Dictionary<string, ScoreboardSlot>>(json);
        }

        public static bool TryGetSlot(string slot, out ScoreboardSlot result)
        {
            return SlotsObsolete.TryGetValue(slot, out result);
        }
    }
}
