using Newtonsoft.Json;

namespace FunctionAnalyser.Builders.Versions
{
    public class VersionResourceKeys
    {
        [JsonProperty("anchors")]
        private readonly string Anchors;

        [JsonProperty("blocks")]
        private readonly string Blocks;

        [JsonProperty("colours")]
        private readonly string Colours;

        [JsonProperty("commands")]
        private readonly string Commands;

        [JsonProperty("components")]
        private readonly string Components;

        [JsonProperty("enchantments")]
        private readonly string Enchantments;

        [JsonProperty("entities")]
        private readonly string Entities;

        [JsonProperty("gamemodes")]
        private readonly string Gamemodes;

        [JsonProperty("item_slots")]
        private readonly string ItemSlots;

        [JsonProperty("items")]
        private readonly string Items;

        [JsonProperty("mob_effects")]
        private readonly string MobEffects;

        [JsonProperty("objective_criteria")]
        private readonly string ObjectiveCriteria;

        [JsonProperty("operations")]
        private readonly string Operations;

        [JsonProperty("particles")]
        private readonly string Particles;

        [JsonProperty("scoreboard_slots")]
        private readonly string ScoreboardSlots;

        [JsonProperty("selector_arguments")]
        private readonly string SelectorArguments;

        [JsonProperty("sorts")]
        private readonly string Sorts;

        [JsonProperty("time_scalars")]
        private readonly string TimeScalars;

        [JsonProperty("structure_rotations")]
        private readonly string StructureRotations;

        [JsonProperty("structure_mirrors")]
        private readonly string StructureMirrors;

        public string GetAnchorsKey()
        {
            return Anchors;
        }

        public string GetBlocksKey()
        {
            return Blocks;
        }

        public string GetColoursKey()
        {
            return Colours;
        }

        public string GetCommandsKey()
        {
            return Commands;
        }

        public string GetComponentsKey()
        {
            return Components;
        }

        public string GetEnchantmentsKey()
        {
            return Enchantments;
        }

        public string GetEntitiesKey()
        {
            return Entities;
        }

        public string GetGamemodesKey()
        {
            return Gamemodes;
        }

        public string GetItemSlotsKey()
        {
            return ItemSlots;
        }

        public string GetItemsKey()
        {
            return Items;
        }

        public string GetMobEffectsKey()
        {
            return MobEffects;
        }

        public string GetObjectiveCriteriaKey()
        {
            return ObjectiveCriteria;
        }

        public string GetOperationsKey()
        {
            return Operations;
        }

        public string GetParticlesKey()
        {
            return Particles;
        }

        public string GetScoreboardSlotsKey()
        {
            return ScoreboardSlots;
        }

        public string GetSelectorArgumentsKey()
        {
            return SelectorArguments;
        }

        public string GetSortsKey()
        {
            return Sorts;
        }

        public string GetTimeScalarsKey()
        {
            return TimeScalars;
        }

        public string GetStructureRotationsKey()
        {
            return StructureRotations;
        }

        public string GetStructureMirrorsKey()
        {
            return StructureMirrors;
        }
    }
}
