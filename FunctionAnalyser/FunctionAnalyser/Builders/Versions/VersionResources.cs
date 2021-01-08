using Newtonsoft.Json;

namespace FunctionAnalyser.Builders.Versions
{
    public class VersionResources
    {
        [JsonProperty("name")]
        private readonly string Name;
        [JsonProperty("resources")]
        private readonly VersionResourceKeys ResourceKeys;

        public string GetName()
        {
            return Name;
        }

        public VersionResourceKeys GetResourceKeys()
        {
            return ResourceKeys;
        }
    }
}
