using static CommandParser.Parsers.JsonParser.JsonCharacterProvider;
using System.Collections.Generic;
using CommandParser.Results;
using CommandParser.Parsers.ComponentParser;
using Newtonsoft.Json;
using CommandParser.Results.Arguments;

namespace CommandParser.Parsers.JsonParser.JsonArguments
{
    public class JsonObject : IJsonArgument
    {
        public const string Name = "Object";

        private readonly Dictionary<string, IJsonArgument> Arguments;

        public JsonObject()
        {
            Arguments = new Dictionary<string, IJsonArgument>();
        }

        public void Add(string name, IJsonArgument argument)
        {
            Arguments[name] = argument;
        }

        public IJsonArgument GetChild(string name)
        {
            return Arguments[name];
        }

        public string AsJson()
        {
            string s = $"{OBJECT_OPEN_CHARACTER}";
            foreach (KeyValuePair<string, IJsonArgument> argument in Arguments)
            {
                s += $"{new JsonString(argument.Key).AsJson()}{NAME_VALUE_SEPARATOR}{argument.Value.AsJson()}{ARGUMENT_SEPARATOR}";
            }
            return s.TrimEnd(ARGUMENT_SEPARATOR) + OBJECT_CLOSE_CHARACTER;
        }

        public string GetName() => Name;

        public ReadResults ValidateComponent(IStringReader reader, int start, DispatcherResources resources)
        {
            return new ReadResults(true, null);
            //return new ComponentReader(this, reader, start, resources).Validate(resources.Components);
        }

        public bool TryGetKey(string key, bool mayUseKeyResourceLocation, out string result)
        {
            if (mayUseKeyResourceLocation)
            {
                foreach (string jsonKey in Arguments.Keys)
                {
                    if (ResourceLocationParser.TryParse(jsonKey, out ResourceLocation resourceLocation) && resourceLocation.IsDefaultNamespace() && key == resourceLocation.Path)
                    {
                        result = jsonKey;
                        return true;
                    }
                }
                result = default;
                return false;
            }
            result = key;
            return Arguments.ContainsKey(key);
        }

        public bool ContainsKey(string key)
        {
            return Arguments.ContainsKey(key);
        }
    }
}
