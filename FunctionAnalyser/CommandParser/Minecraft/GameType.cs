using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CommandParser.Minecraft {
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GameType {
        [EnumMember(Value = "survival")]
        SURVIVAL = 0,
        [EnumMember(Value = "creative")]
        CREATIVE = 1,
        [EnumMember(Value = "adventure")]
        ADVENTURE = 2,
        [EnumMember(Value = "spectator")]
        SPECTATOR = 3
    }
}
