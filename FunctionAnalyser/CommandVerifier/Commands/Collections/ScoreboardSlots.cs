using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandVerifier.Commands.Collections
{
    public class ScoreboardSlots
    {
        public static Dictionary<string, ScoreboardSlot> Options { get; private set; }
        
        static ScoreboardSlots()
        {
            Options = new Dictionary<string, ScoreboardSlot>();
        }

        public static void SetOptions(string json)
        {
            Options = JsonConvert.DeserializeObject<Dictionary<string, ScoreboardSlot>>(json, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
        }
    }
}
