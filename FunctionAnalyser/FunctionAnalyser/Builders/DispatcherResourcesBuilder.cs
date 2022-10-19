using ErrorCraft.PackAnalyser.Builders.Collections;
using System.Collections.Generic;

namespace ErrorCraft.PackAnalyser.Builders {
    public class DispatcherResourcesBuilder {
        public Dictionary<string, AnchorsBuilder> Anchors { get; init; }
        public Dictionary<string, BlocksBuilder> Blocks { get; init; }
        public Dictionary<string, CommandsBuilder> Commands { get; init; }
        public Dictionary<string, ColoursBuilder> Colours { get; init; }
        public Dictionary<string, ComponentsBuilder> Components { get; init; }
        public Dictionary<string, EnchantmentsBuilder> Enchantments { get; init; }
        public Dictionary<string, EntitiesBuilder> Entities { get; init; }
        public Dictionary<string, GamemodesBuilder> Gamemodes { get; init; }
        public Dictionary<string, ItemsBuilder> Items { get; init; }
        public Dictionary<string, ComponentsBuilder> ItemComponents { get; init; }
        public Dictionary<string, ItemSlotsBuilder> ItemSlots { get; init; }
        public Dictionary<string, MobEffectsBuilder> MobEffects { get; init; }
        public Dictionary<string, ObjectiveCriteriaBuilder> ObjectiveCriteria { get; init; }
        public Dictionary<string, OperationsBuilder> Operations { get; init; }
        public Dictionary<string, ParticlesBuilder> Particles { get; init; }
        public Dictionary<string, ScoreboardSlotsBuilder> ScoreboardSlots { get; init; }
        public Dictionary<string, SelectorArgumentsBuilder> SelectorArguments { get; init; }
        public Dictionary<string, SortsBuilder> Sorts { get; init; }
        public Dictionary<string, TimeScalarsBuilder> TimeScalars { get; init; }
        public Dictionary<string, StructureRotationsBuilder> StructureRotations { get; init; }
        public Dictionary<string, StructureMirrorsBuilder> StructureMirrors { get; init; }
    }
}
