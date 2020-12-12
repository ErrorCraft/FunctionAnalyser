using CommandParser.Converters;
using CommandParser.Parsers.JsonParser.JsonArguments;
using CommandParser.Results;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandParser.Parsers.ComponentParser.ComponentArguments
{
    [JsonConverter(typeof(ComponentConverter))]
    public abstract class ComponentArgument
    {
        [JsonProperty("children")]
        protected readonly Dictionary<string, ComponentArgument> Children = new Dictionary<string, ComponentArgument>();
        [JsonProperty("optional")]
        protected readonly bool Optional = false;
        [JsonProperty("match_first")]
        protected readonly bool MatchFirst = false;

        public abstract ReadResults Validate(JsonObject obj, string key, IStringReader reader, int start);

        protected ReadResults ValidateChildren(JsonObject obj, string key, IStringReader reader, int start)
        {
            ReadResults readResults;
            foreach (KeyValuePair<string, ComponentArgument> child in Children)
            {
                if (obj.ContainsKey(child.Key))
                {
                    readResults = child.Value.Validate(obj, child.Key, reader, start);
                    if (MatchFirst || !readResults.Successful) return readResults;
                } else if (!child.Value.Optional && !MatchFirst)
                {
                    reader.SetCursor(start);
                    return new ReadResults(false, ComponentCommandError.IncompleteComponent(key, this).WithContext(reader));
                }
            }
            if (MatchFirst)
            {
                reader.SetCursor(start);
                return new ReadResults(false, ComponentCommandError.UnknownComponentError(obj).WithContext(reader));
            }
            return new ReadResults(true, null);
        }

        public string StringifyChildrenKeys()
        {
            List<string> keys = new List<string>(Children.Keys);
            for (int i = keys.Count - 1; i > 0; i--)
            {
                if (Children[keys[i]].Optional) keys.RemoveAt(i);
            }

            if (keys.Count == 0)
            {
                return "";
            } else if (keys.Count == 1)
            {
                return $"'{keys[0]}'";
            } else if (keys.Count == 2)
            {
                return $"'{keys[0]}' and '{keys[1]}'";
            } else
            {
                string s = "";
                for (int i = 0; i < keys.Count - 1; i++)
                {
                    s += $"'{keys[i]}', ";
                }
                return $"{s} and {keys[^1]}";
            }
        }

        protected static bool IsText(IJsonArgument argument)
        {
            return argument is JsonString || argument is JsonNumber || argument is JsonBoolean;
        }
    }
}
