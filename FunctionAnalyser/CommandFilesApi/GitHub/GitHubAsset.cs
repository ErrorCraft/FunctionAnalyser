using Newtonsoft.Json;

namespace CommandFilesApi.GitHub
{
    public class GitHubAsset
    {
        [JsonProperty("name")]
        private readonly string Name;

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
    }
}
