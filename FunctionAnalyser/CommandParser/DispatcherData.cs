using Newtonsoft.Json;

namespace CommandParser
{
    public class DispatcherData
    {
        [JsonProperty("comment_string")]
        private readonly string CommentString;

        public string GetCommentString()
        {
            return CommentString;
        }
    }
}
