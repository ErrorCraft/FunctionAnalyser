using static CommandParser.Parsers.JsonParser.JsonCharacterProvider;
using System.Collections.Generic;
using CommandParser.Results;
using CommandParser.Parsers.ComponentParser;

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

        public ReadResults ValidateComponent(StringReader reader, int start)
        {
            return new ComponentReader(this, reader, start).Validate();
        }

        public bool ContainsKey(string key)
        {
            return Arguments.ContainsKey(key);
        }
    }
}
