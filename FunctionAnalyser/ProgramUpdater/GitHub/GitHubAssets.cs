using Newtonsoft.Json;

namespace CommandFilesApi.GitHub
{
    public class GitHubAssets
    {
        [JsonProperty("name")]
        private readonly string Name;

        [JsonProperty("label")]
        private readonly string Label;

        [JsonProperty("browser_download_url")]
        private readonly string DownloadUrl;

        public string GetName()
        {
            return Name;
        }

        public string GetDownloadUrl()
        {
            return DownloadUrl;
        }

        public string GetLabel()
        {
            return Label;
        }
    }
}
