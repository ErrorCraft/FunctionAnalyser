﻿using CommandParser.Collections;
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
        [JsonProperty("key_resource_location")]
        private readonly bool KeyResourceLocation = false;

        public abstract ReadResults Validate(JsonObject obj, string key, ComponentReader componentReader, Components components, IStringReader reader, int start, DispatcherResources resources);

        protected ReadResults ValidateChildren(JsonObject obj, string key, ComponentReader componentReader, Components components, IStringReader reader, int start, DispatcherResources resources)
        {
            ReadResults readResults;
            foreach (KeyValuePair<string, ComponentArgument> child in Children)
            {
                if (obj.ContainsKey(child.Key))
                {
                    readResults = child.Value.Validate(obj, child.Key, componentReader, components, reader, start, resources);
                    if (MatchFirst || !readResults.Successful) return readResults;
                } else if (!child.Value.Optional && !MatchFirst)
                {
                    reader.SetCursor(start);
                    return ReadResults.Failure(ComponentCommandError.IncompleteComponent(key, this).WithContext(reader));
                }
            }
            if (MatchFirst)
            {
                reader.SetCursor(start);
                return ReadResults.Failure(ComponentCommandError.UnknownComponent(obj).WithContext(reader));
            }
            return ReadResults.Success();
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

        public bool MayUseKeyResourceLocation()
        {
            return KeyResourceLocation;
        }

        protected static bool IsText(IJsonArgument argument)
        {
            return argument is JsonString || argument is JsonNumber || argument is JsonBoolean;
        }
    }
}
