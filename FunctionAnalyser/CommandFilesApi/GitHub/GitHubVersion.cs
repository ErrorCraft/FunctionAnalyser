using CommandFilesApi.Converters;
using Newtonsoft.Json;
using System;

namespace CommandFilesApi.GitHub
{
    public class GitHubVersion
    {
        [JsonProperty("tag_name")]
        [JsonConverter(typeof(TagNameConverter))]
        private readonly Version VersionTag;

        [JsonProperty("assets")]
        private readonly GitHubAssets[] Assets;

        public Version GetVersionTag()
        {
            return VersionTag;
        }

        public GitHubAssets[] GetAssets()
        {
            return Assets;
        }
    }
}
