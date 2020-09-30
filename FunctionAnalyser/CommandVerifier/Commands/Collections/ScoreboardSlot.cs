using CommandVerifier.Commands.SubcommandTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace CommandVerifier.Commands.Collections
{
    public class ScoreboardSlot
    {
        public enum FromType
        {
            [EnumMember(Value = "none")]
            None = 0,
            [EnumMember(Value = "colour")]
            Colour = 1
        }

        [JsonProperty("from", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        [DefaultValue(FromType.None)]
        public FromType From { get; set; }

        [JsonProperty("contents_optional")]
        [DefaultValue(false)]
        public bool ContentsOptional { get; set; }

        public bool TryRead(string value)
        {
            if ((ContentsOptional || From == FromType.None) && string.IsNullOrEmpty(value)) return true;
            string[] values = value.Substring(1).Split('.');
            return From switch
            {
                FromType.None => string.IsNullOrEmpty(value),
                FromType.Colour => values.Length == 2 && values[0] == "team" && Colour.COLOURS.Contains(values[1]),
                _ => false
            };
        }
    }
}
