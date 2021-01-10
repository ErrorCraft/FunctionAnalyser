using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CommandParser.Collections
{
    public enum ContentType
    {
        [EnumMember(Value = "none")]
        None = 0,
        [EnumMember(Value = "colour")]
        Colour = 1
    }

    public class ScoreboardSlot
    {
        [JsonProperty("slot_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        private readonly ContentType Contents = ContentType.None;

        [JsonProperty("contents_optional")]
        private readonly bool ContentsOptional = false;

        public bool Read(string input, DispatcherResources resources)
        {
            if (string.IsNullOrEmpty(input)) return ContentsOptional || Contents == ContentType.None;
            string[] values = input.Substring(1).Split('.');
            return Contents switch
            {
                ContentType.None => string.IsNullOrEmpty(input),
                ContentType.Colour => values.Length == 2 && "team".Equals(values[0]) && resources.Colours.Contains(values[1]),
                _ => false
            };
        }
    }
}
