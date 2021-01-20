using CommandParser.Collections;
using CommandParser.Parsers.JsonParser.JsonArguments;
using CommandParser.Results;
using Newtonsoft.Json;

namespace CommandParser.Parsers.ComponentParser.ComponentArguments
{
    public class ComponentArray : ComponentArgument
    {
        [JsonProperty("may_be_empty")]
        private readonly bool MayBeEmpty = false;
        [JsonProperty("object_only")]
        private readonly bool ObjectOnly = false;

        public override ReadResults Validate(JsonObject obj, string key, ComponentReader componentReader, Components components, IStringReader reader, int start, DispatcherResources resources)
        {
            if (obj.GetChild(key) is not JsonArray actualArray)
            {
                reader.SetCursor(start);
                return new ReadResults(false, ComponentCommandError.InvalidComponent(key, JsonArray.Name, obj.GetChild(key).GetName()).WithContext(reader));
            }

            if (actualArray.GetLength() == 0 && !MayBeEmpty)
            {
                reader.SetCursor(start);
                return new ReadResults(false, ComponentCommandError.EmptyComponent().WithContext(reader));
            }

            ReadResults readResults;
            foreach (IJsonArgument child in actualArray.GetChildren())
            {
                if (ObjectOnly && child is not JsonObject)
                {
                    reader.SetCursor(start);
                    return new ReadResults(false, ComponentCommandError.InvalidComponentArray(key, JsonObject.Name, child.GetName()).WithContext(reader));
                }
                readResults = componentReader.ValidateContents(child, components);
                if (!readResults.Successful) return readResults;
            }

            return new ReadResults(true, null);
        }
    }
}
