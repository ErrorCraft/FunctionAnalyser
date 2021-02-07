using CommandParser.Collections;
using CommandParser.Parsers.JsonParser;
using CommandParser.Parsers.JsonParser.JsonArguments;
using CommandParser.Results;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandParser.Parsers.ComponentParser.ComponentArguments
{
    public class ComponentBinding : ComponentArgument
    {
        [JsonProperty("bind_to")]
        private readonly string BindTo = "";
        [JsonProperty("values")]
        private readonly Dictionary<string, ComponentArgument> Values = new Dictionary<string, ComponentArgument>();

        public override ReadResults Validate(JsonObject obj, string key, ComponentReader componentReader, Components components, IStringReader reader, int start, DispatcherResources resources)
        {
            if (!obj.ContainsKey(BindTo)) return ReadResults.Success();
            if (!IsText(obj.GetChild(BindTo)))
            {
                reader.SetCursor(start);
                return ReadResults.Failure(ComponentCommandError.InvalidComponent(key, JsonArgumentType.String, obj.GetChild(BindTo).GetArgumentType()).WithContext(reader));
            }

            string binding = obj.GetChild(BindTo).ToString();
            if (Values.TryGetValue(binding, out ComponentArgument argument)) return argument.Validate(obj, key, componentReader, components, reader, start, resources);
            return ReadResults.Success();
        }
    }
}
