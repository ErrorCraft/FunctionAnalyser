using CommandParser;
using CommandParser.Collections;
using CommandParser.Tree;
using FunctionAnalyser.Builders.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FunctionAnalyser.Builders.Versions
{
    public class VersionsBuilder
    {
        private readonly Dictionary<string, VersionResources> Versions;

        public VersionsBuilder(Dictionary<string, VersionResources> versions)
        {
            Versions = versions;
        }

        public Dictionary<string, Dispatcher> Build(DispatcherResourcesBuilder resources)
        {
            Dictionary<string, Dispatcher> dispatchers = new Dictionary<string, Dispatcher>();
            foreach (KeyValuePair<string, VersionResources> pair in Versions)
            {
                dispatchers.Add(pair.Key, GetDispatcher(resources, pair.Value));
            }
            return dispatchers;
        }

        private static Dispatcher GetDispatcher(DispatcherResourcesBuilder dispatcherResources, VersionResources versionResources)
        {
            VersionResourceKeys keys = versionResources.GetResourceKeys();
            RootNode commandRootNode = GetResources<CommandsBuilder, RootNode>(dispatcherResources.Commands, keys.GetCommandsKey());
            DispatcherResources commandResources = new DispatcherResources()
            {
                Anchors = GetResources<AnchorsBuilder, Anchors>(dispatcherResources.Anchors, keys.GetAnchorsKey()),
                Blocks = GetResources<BlocksBuilder, Blocks>(dispatcherResources.Blocks, keys.GetBlocksKey()),
                Colours = GetResources<ColoursBuilder, Colours>(dispatcherResources.Colours, keys.GetColoursKey()),
                Components = GetResources<ComponentsBuilder, Components>(dispatcherResources.Components, keys.GetComponentsKey()),
                Enchantments = GetResources<EnchantmentsBuilder, Enchantments>(dispatcherResources.Enchantments, keys.GetEnchantmentsKey()),
                Entities = GetResources<EntitiesBuilder, Entities>(dispatcherResources.Entities, keys.GetEntitiesKey()),
                Gamemodes = GetResources<GamemodesBuilder, Gamemodes>(dispatcherResources.Gamemodes, keys.GetGamemodesKey()),
                Items = GetResources<ItemsBuilder, Items>(dispatcherResources.Items, keys.GetItemsKey()),
                ItemComponents = GetResources<ComponentsBuilder, Components>(dispatcherResources.ItemComponents, keys.GetItemComponentsKey()),
                ItemSlots = GetResources<ItemSlotsBuilder, ItemSlots>(dispatcherResources.ItemSlots, keys.GetItemSlotsKey()),
                MobEffects = GetResources<MobEffectsBuilder, MobEffects>(dispatcherResources.MobEffects, keys.GetMobEffectsKey()),
                ObjectiveCriteria = GetResources<ObjectiveCriteriaBuilder, ObjectiveCriteria>(dispatcherResources.ObjectiveCriteria, keys.GetObjectiveCriteriaKey()),
                Operations = GetResources<OperationsBuilder, Operations>(dispatcherResources.Operations, keys.GetOperationsKey()),
                Particles = GetResources<ParticlesBuilder, Particles>(dispatcherResources.Particles, keys.GetParticlesKey()),
                ScoreboardSlots = GetResources<ScoreboardSlotsBuilder, ScoreboardSlots>(dispatcherResources.ScoreboardSlots, keys.GetScoreboardSlotsKey()),
                SelectorArguments = GetResources<SelectorArgumentsBuilder, EntitySelectorOptions>(dispatcherResources.SelectorArguments, keys.GetSelectorArgumentsKey()),
                Sorts = GetResources<SortsBuilder, Sorts>(dispatcherResources.Sorts, keys.GetSortsKey()),
                TimeScalars = GetResources<TimeScalarsBuilder, TimeScalars>(dispatcherResources.TimeScalars, keys.GetTimeScalarsKey()),
                StructureRotations = GetResources<StructureRotationsBuilder, StructureRotations>(dispatcherResources.StructureRotations, keys.GetStructureRotationsKey()),
                StructureMirrors = GetResources<StructureMirrorsBuilder, StructureMirrors>(dispatcherResources.StructureMirrors, keys.GetStructureMirrorsKey())
            };

            Dispatcher dispatcher = new Dispatcher(versionResources.GetName(), commandRootNode, commandResources, versionResources.GetUseBedrockStringReader());
            return dispatcher;
        }

        private static U GetResources<T, U>(Dictionary<string, T> resources, string key) where T : IBuilder<T, U> where U : class, new()
        {
            if (key == null) return new U();
            return resources[key].Build(resources);
        }

        public static VersionsBuilder FromJson(string json)
        {
            Dictionary<string, VersionResources> versions = JsonConvert.DeserializeObject<Dictionary<string, VersionResources>>(json);
            return new VersionsBuilder(versions);
        }
    }
}
