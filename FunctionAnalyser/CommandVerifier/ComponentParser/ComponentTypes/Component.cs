using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace CommandVerifier.ComponentParser.ComponentTypes
{
    [JsonConverter(typeof(Converters.ComponentConverter))]
    public abstract class Component
    {
        [JsonProperty("contents")]
        private protected Dictionary<string, Component> Contents;

        [JsonProperty("match_first_in_contents")]
        [DefaultValue(false)]
        private protected bool MatchFirst;

        [JsonProperty("optional")]
        [DefaultValue(false)]
        private protected bool Optional;

        public abstract bool Validate(JsonTypes.Object obj, string key, StringReader reader, int start, bool mayThrow);

        public string GetContentsKeys()
        {
            List<string> keys = new List<string>(Contents.Keys);
            for (int i = keys.Count - 1; i > 0; i--)
            {
                if (Contents[keys[i]].Optional) keys.RemoveAt(i);
            }
            if (keys.Count == 1)
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
                    s += "'" + keys[i] + "', ";
                }
                return s + $"and '{keys[^1]}'";
            }
        }

        private protected bool ValidateContents(JsonTypes.Object obj, string key, StringReader reader, int start, bool mayThrow)
        {
            if (Contents != null)
            {
                foreach (string contentsKey in Contents.Keys)
                {
                    if (!obj.Values.ContainsKey(contentsKey))
                    {
                        if (!Contents[contentsKey].Optional && !MatchFirst)
                        {
                            reader.SetCursor(start);
                            if (mayThrow) ComponentErrors.IncompleteComponentError(key, this).AddWithContext(reader);
                            return false;
                        }
                    }
                    else
                    {
                        if (!Contents[contentsKey].Validate(obj, contentsKey, reader, start, mayThrow)) return false;
                        else if (MatchFirst) return true;
                    }
                }

                if (MatchFirst)
                {
                    reader.SetCursor(start);
                    if (mayThrow) ComponentErrors.UnknownComponentError(obj).AddWithContext(reader);
                    return false;
                }
            }
            return true;
        }

        private protected bool IsText(JsonTypes.IComponent component)
        {
            return component is JsonTypes.String || component is JsonTypes.Number || component is JsonTypes.Boolean;
        }
    }
}
