using Newtonsoft.Json;

namespace ErrorCraft.PackAnalyser.Builders {
    class Definitions {
        [JsonProperty("anchors")]
        private readonly string[] Anchors;
        [JsonProperty("blocks")]
        private readonly string[] Blocks;
        [JsonProperty("colours")]
        private readonly string[] Colours;
        [JsonProperty("commands")]
        private readonly string[] Commands;
        [JsonProperty("components")]
        private readonly string[] Components;
        [JsonProperty("enchantments")]
        private readonly string[] Enchantments;
        [JsonProperty("entities")]
        private readonly string[] Entities;
        [JsonProperty("gamemodes")]
        private readonly string[] Gamemodes;
        [JsonProperty("items")]
        private readonly string[] Items;
        [JsonProperty("item_components")]
        private readonly string[] ItemComponents;
        [JsonProperty("item_slots")]
        private readonly string[] ItemSlots;
        [JsonProperty("mob_effects")]
        private readonly string[] MobEffects;
        [JsonProperty("objective_criteria")]
        private readonly string[] ObjectiveCriteria;
        [JsonProperty("operations")]
        private readonly string[] Operations;
        [JsonProperty("particles")]
        private readonly string[] Particles;
        [JsonProperty("scoreboard_slots")]
        private readonly string[] ScoreboardSlots;
        [JsonProperty("selector_arguments")]
        private readonly string[] SelectorArguments;
        [JsonProperty("sorts")]
        private readonly string[] Sorts;
        [JsonProperty("time_scalars")]
        private readonly string[] TimeScalars;
        [JsonProperty("structure_rotations")]
        private readonly string[] StructureRotations;
        [JsonProperty("structure_mirrors")]
        private readonly string[] StructureMirrors;

        public string[] GetAnchors() {
            return Anchors;
        }

        public string[] GetBlocks() {
            return Blocks;
        }

        public string[] GetColours() {
            return Colours;
        }

        public string[] GetCommands() {
            return Commands;
        }

        public string[] GetComponents() {
            return Components;
        }

        public string[] GetEnchantments() {
            return Enchantments;
        }

        public string[] GetEntities() {
            return Entities;
        }

        public string[] GetGamemodes() {
            return Gamemodes;
        }

        public string[] GetItems() {
            return Items;
        }

        public string[] GetItemComponents() {
            return ItemComponents;
        }

        public string[] GetItemSlots() {
            return ItemSlots;
        }

        public string[] GetMobEffects() {
            return MobEffects;
        }

        public string[] GetObjectiveCriteria() {
            return ObjectiveCriteria;
        }

        public string[] GetOperations() {
            return Operations;
        }

        public string[] GetParticles() {
            return Particles;
        }

        public string[] GetScoreboardSlots() {
            return ScoreboardSlots;
        }

        public string[] GetSelectorArguments() {
            return SelectorArguments;
        }

        public string[] GetSorts() {
            return Sorts;
        }

        public string[] GetTimeScalars() {
            return TimeScalars;
        }

        public string[] GetStructureRotations() {
            return StructureRotations;
        }

        public string[] GetStructureMirrors() {
            return StructureMirrors;
        }
    }
}
