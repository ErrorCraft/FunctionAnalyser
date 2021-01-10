using ProgramUpdater.Converters;
using Newtonsoft.Json;

namespace ProgramUpdater
{
    public class Version
    {
        [JsonProperty("tag_name")]
        [JsonConverter(typeof(TagNameConverter))]
        private readonly System.Version VersionTag;

        [JsonProperty("assets")]
        private readonly Assets[] Assets;

        public System.Version GetVersionTag()
        {
            return VersionTag;
        }

        public Assets[] GetAssets()
        {
            return Assets;
        }
    }
}
