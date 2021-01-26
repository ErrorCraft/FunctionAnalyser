using CommandParser;
using Newtonsoft.Json;

namespace FunctionAnalyser.Builders.Versions
{
    public class VersionResources
    {
        [JsonProperty("name")]
        private readonly string Name;
        [JsonProperty("resources")]
        private readonly VersionResourceKeys ResourceKeys;
        [JsonProperty("use_bedrock_string_reader")]
        private readonly bool UseBedrockStringReader;
        [JsonProperty("data")]
        private readonly DispatcherData Data;

        public string GetName()
        {
            return Name;
        }

        public VersionResourceKeys GetResourceKeys()
        {
            return ResourceKeys;
        }

        public bool GetUseBedrockStringReader()
        {
            return UseBedrockStringReader;
        }

        public DispatcherData GetData()
        {
            return Data;
        }
    }
}
