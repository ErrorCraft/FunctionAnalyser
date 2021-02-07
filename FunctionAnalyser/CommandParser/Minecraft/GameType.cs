using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CommandParser.Minecraft
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GameType
    {
        [EnumMember(Value = "survival")]
        Survival = 0,
        [EnumMember(Value = "creative")]
        Creative = 1,
        [EnumMember(Value = "adventure")]
        Adventure = 2,
        [EnumMember(Value = "spectator")]
        Spectator = 3
    }
}
