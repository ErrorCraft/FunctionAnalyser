using CommandParser;
using CommandParser.Tree;
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
            RootNode commandRootNode = dispatcherResources.Commands[keys.GetCommandsKey()].Build(dispatcherResources.Commands);
            DispatcherResources commandResources = new DispatcherResources()
            {
                Anchors = dispatcherResources.Anchors[keys.GetAnchorsKey()].Build(dispatcherResources.Anchors),
                Blocks = dispatcherResources.Blocks[keys.GetBlocksKey()].Build(dispatcherResources.Blocks),
                Colours = dispatcherResources.Colours[keys.GetColoursKey()].Build(dispatcherResources.Colours),
                Components = dispatcherResources.Components[keys.GetComponentsKey()].Build(dispatcherResources.Components),
                Enchantments = dispatcherResources.Enchantments[keys.GetEnchantmentsKey()].Build(dispatcherResources.Enchantments),
                Entities = dispatcherResources.Entities[keys.GetEntitiesKey()].Build(dispatcherResources.Entities),
                Gamemodes = dispatcherResources.Gamemodes[keys.GetGamemodesKey()].Build(dispatcherResources.Gamemodes),
                ItemSlots = dispatcherResources.ItemSlots[keys.GetItemSlotsKey()].Build(dispatcherResources.ItemSlots),
                Items = dispatcherResources.Items[keys.GetItemsKey()].Build(dispatcherResources.Items),
                MobEffects = dispatcherResources.MobEffects[keys.GetMobEffectsKey()].Build(dispatcherResources.MobEffects),
                ObjectiveCriteria = dispatcherResources.ObjectiveCriteria[keys.GetObjectiveCriteriaKey()].Build(dispatcherResources.ObjectiveCriteria),
                Operations = dispatcherResources.Operations[keys.GetOperationsKey()].Build(dispatcherResources.Operations),
                Particles = dispatcherResources.Particles[keys.GetParticlesKey()].Build(dispatcherResources.Particles),
                ScoreboardSlots = dispatcherResources.ScoreboardSlots[keys.GetScoreboardSlotsKey()].Build(dispatcherResources.ScoreboardSlots),
                SelectorArguments = dispatcherResources.SelectorArguments[keys.GetSelectorArgumentsKey()].Build(dispatcherResources.SelectorArguments),
                Sorts = dispatcherResources.Sorts[keys.GetSortsKey()].Build(dispatcherResources.Sorts),
                TimeScalars = dispatcherResources.TimeScalars[keys.GetTimeScalarsKey()].Build(dispatcherResources.TimeScalars)
            };

            Dispatcher dispatcher = new Dispatcher(versionResources.GetName(), commandRootNode, commandResources);
            return dispatcher;
        }

        public static VersionsBuilder FromJson(string json)
        {
            Dictionary<string, VersionResources> versions = JsonConvert.DeserializeObject<Dictionary<string, VersionResources>>(json);
            return new VersionsBuilder(versions);
        }
    }
}
