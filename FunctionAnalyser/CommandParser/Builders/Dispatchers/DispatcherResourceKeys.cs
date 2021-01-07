using Newtonsoft.Json;

namespace CommandParser.Builders.Dispatchers
{
    public class DispatcherResourceKeys
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
