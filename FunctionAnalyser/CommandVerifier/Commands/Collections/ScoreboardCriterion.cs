using CommandVerifier.Commands.SubcommandTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace CommandVerifier.Commands.Collections
{
    public class ScoreboardCriterion
    {
        public enum FromType
        {
            [EnumMember(Value = "none")]
            None = 0,
            [EnumMember(Value = "colour")]
            Colour = 1,
            [EnumMember(Value = "item")]
            Item = 2,
            [EnumMember(Value = "block")]
            Block = 3,
            [EnumMember(Value = "entity")]
            Entity = 4,
            [EnumMember(Value = "statistic")]
            Statistic = 5
        }

        [JsonProperty("from")]
        [JsonConverter(typeof(StringEnumConverter))]
        [DefaultValue(FromType.None)]
        public FromType From { get; set; }

        public bool TryRead(string value)
        {
            return From switch
            {
                FromType.None => string.IsNullOrEmpty(value),
                FromType.Colour => Colour.COLOURS.Contains(value),
                FromType.Item => Items.Options.Contains(value),
                FromType.Block => Blocks.Options.ContainsKey(value),
                FromType.Entity => Entities.Options.Contains(value),
                FromType.Statistic => ScoreboardCriteria.CustomCriteria.Contains(value),
                _ => false
            };
        }
    }
}
