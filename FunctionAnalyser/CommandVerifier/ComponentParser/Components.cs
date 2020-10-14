using CommandVerifier.ComponentParser.ComponentTypes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandVerifier.ComponentParser
{
    public class Components
    {
        [JsonProperty("content")]
        public static Dictionary<string, Component> Content { get; set; }

        [JsonProperty("children")]
        public static Dictionary<string, Component> Children { get; set; }

        [JsonProperty("formatting")]
        public static Dictionary<string, Component> Formatting { get; set; }

        [JsonProperty("interactivity")]
        public static Dictionary<string, Component> Interactivity { get; set; }

        public static void SetOptions(string json)
        {
            JsonConvert.DeserializeObject<Components>(json, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
        }
    }
}
