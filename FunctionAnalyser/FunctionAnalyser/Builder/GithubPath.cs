using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;

namespace FunctionAnalyser.Builder
{
    public class GithubPath
    {
        [JsonProperty("name")]
        private readonly string Name;
        [JsonProperty("download_url")]
        private readonly string DownloadUrl;
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        private readonly GithubPathType ContentType;

        public string GetName()
        {
            return Path.GetFileNameWithoutExtension(Name);
        }

        public string GetDownloadUrl()
        {
            return DownloadUrl;
        }

        public GithubPathType GetContentType()
        {
            return ContentType;
        }
    }
}
