using Newtonsoft.Json;

namespace FunctionAnalyser.Builders.Versions
{
    public class VersionResourceKeys
    {
        [JsonProperty("commands")]
        private readonly string Commands;

        [JsonProperty("items")]
        private readonly string Items;

        public string GetCommandsKey()
        {
            return Commands;
        }

        public string GetItemsKey()
        {
            return Items;
        }
    }
}
