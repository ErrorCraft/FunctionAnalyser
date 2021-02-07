using CommandParser.Collections;
using CommandParser.Parsers.JsonParser;
using CommandParser.Parsers.JsonParser.JsonArguments;
using CommandParser.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CommandParser.Parsers.ComponentParser.ComponentArguments
{
    public class ComponentArray : ComponentArgument
    {
        [JsonProperty("may_be_empty")]
        private readonly bool MayBeEmpty = false;
        [JsonProperty("array_contents")]
        [JsonConverter(typeof(StringEnumConverter))]
        private readonly JsonArgumentType? ArrayContents = null;

        public override ReadResults Validate(JsonObject obj, string key, ComponentReader componentReader, Components components, IStringReader reader, int start, DispatcherResources resources)
        {
            if (obj.GetChild(key) is not JsonArray actualArray)
            {
                reader.SetCursor(start);
                return ReadResults.Failure(ComponentCommandError.InvalidComponent(key, JsonArgumentType.Array, obj.GetChild(key).GetArgumentType()).WithContext(reader));
            }

            if (actualArray.GetLength() == 0 && !MayBeEmpty)
            {
                reader.SetCursor(start);
                return ReadResults.Failure(ComponentCommandError.EmptyComponent().WithContext(reader));
            }

            ReadResults readResults;
            foreach (IJsonArgument child in actualArray.GetChildren())
            {
                if (ArrayContents is not null && child.GetArgumentType() != ArrayContents)
                {
                    reader.SetCursor(start);
                    return ReadResults.Failure(ComponentCommandError.InvalidComponentArray(key, ArrayContents.Value, child.GetArgumentType()).WithContext(reader));
                }
                readResults = componentReader.ValidateContents(child, components);
                if (!readResults.Successful) return readResults;
            }

            return ReadResults.Success();
        }
    }
}
