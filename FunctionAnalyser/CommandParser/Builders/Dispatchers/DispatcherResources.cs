using Newtonsoft.Json;

namespace CommandParser.Builders.Dispatchers
{
    public class DispatcherResources
    {
        [JsonProperty("name")]
        private readonly string Name;
        [JsonProperty("resources")]
        private readonly DispatcherResourceKeys Resources;

        public string GetName()
        {
            return Name;
        }

        public DispatcherResourceKeys GetResourceKeys()
        {
            return Resources;
        }
    }
}
