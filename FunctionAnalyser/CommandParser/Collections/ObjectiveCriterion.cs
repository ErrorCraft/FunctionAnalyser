using CommandParser.Minecraft;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CommandParser.Collections
{
    public enum CriterionType
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
    public class ObjectiveCriterion
    {
        [JsonProperty("criterion_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        private readonly CriterionType CriterionType = CriterionType.None;

        public bool Read(string contents, DispatcherResources resources)
        {
            bool successfulParse = ResourceLocation.TryParse(contents, out ResourceLocation resourceLocation);
            return CriterionType switch
            {
                CriterionType.None => string.IsNullOrEmpty(contents),
                CriterionType.Colour => resources.Colours.Contains(contents),
                CriterionType.Item => successfulParse && resources.Items.Contains(resourceLocation),
                CriterionType.Block => successfulParse && resources.Blocks.ContainsBlock(resourceLocation),
                CriterionType.Entity => successfulParse && resources.Entities.Contains(resourceLocation),
                CriterionType.Statistic => resources.ObjectiveCriteria.ContainsCustomCriterion(contents),
                _ => false
            };
        }
    }
}
