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
                Items = dispatcherResources.Items[keys.GetItemsKey()].Build(dispatcherResources.Items)
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
