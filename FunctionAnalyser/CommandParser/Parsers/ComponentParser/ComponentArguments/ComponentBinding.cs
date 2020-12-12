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

        public override ReadResults Validate(JsonObject obj, string key, IStringReader reader, int start)
        {
            if (obj.ContainsKey(BindTo)) return new ReadResults(true, null);
            if (!IsText(obj.GetChild(BindTo)))
            {
                reader.SetCursor(start);
                return new ReadResults(false, ComponentCommandError.StringFormat(key, JsonString.NAME, obj.GetChild(BindTo).GetName()).WithContext(reader));
            }

            string binding = obj.GetChild(BindTo).ToString();
            if (Values.TryGetValue(binding, out ComponentArgument argument)) return argument.Validate(obj, key, reader, start);
            return new ReadResults(true, null);
        }
    }
}
