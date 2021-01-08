using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FunctionAnalyser.Builders
{
    public class Path
    {
        [JsonProperty("name")]
        private readonly string Name;
        [JsonProperty("download_url")]
        private readonly string DownloadUrl;
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        private readonly PathType ContentType;

        public string GetName()
        {
            return System.IO.Path.GetFileNameWithoutExtension(Name);
        }

        public string GetDownloadUrl()
        {
            return DownloadUrl;
        }

        public PathType GetContentType()
        {
            return ContentType;
        }
    }
}
