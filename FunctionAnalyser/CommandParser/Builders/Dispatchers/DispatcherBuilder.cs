using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandParser.Builders.Dispatchers
{
    public class DispatcherBuilder
    {
        private Dictionary<string, DispatcherResources> Versions;

        public DispatcherBuilder(Dictionary<string, DispatcherResources> versions)
        {
            Versions = versions;
        }

        public Dictionary<string, Dispatcher> Build()
        {
            return null;
        }

        public static DispatcherBuilder FromJson(string json)
        {
            Dictionary<string, DispatcherResources> versions = JsonConvert.DeserializeObject<Dictionary<string, DispatcherResources>>(json);
            return new DispatcherBuilder(versions);
        }
    }
}
