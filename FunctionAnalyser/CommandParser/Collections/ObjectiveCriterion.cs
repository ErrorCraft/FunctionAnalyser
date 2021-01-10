using CommandParser.Results.Arguments;
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
            return CriterionType switch
            {
                CriterionType.None => string.IsNullOrEmpty(contents),
                CriterionType.Colour => resources.Colours.Contains(contents),
                CriterionType.Item => resources.Items.Contains(new ResourceLocation(contents)),
                CriterionType.Block => resources.Blocks.ContainsBlock(new ResourceLocation(contents)),
                CriterionType.Entity => resources.Entities.Contains(new ResourceLocation(contents)),
                CriterionType.Statistic => resources.ObjectiveCriteria.ContainsCustomCriterion(contents),
                _ => false
            };
        }
    }
}
