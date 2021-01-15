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

        public override ReadResults Validate(JsonObject obj, string key, ComponentReader componentReader, Components components, IStringReader reader, int start, DispatcherResources resources)
        {
            if (obj.GetChild(key) is not JsonArray actualArray)
            {
                reader.SetCursor(start);
                return new ReadResults(false, ComponentCommandError.StringFormat(key, JsonArray.Name, obj.GetChild(key).GetName()).WithContext(reader));
            }

            if (actualArray.GetLength() == 0 && !MayBeEmpty)
            {
                reader.SetCursor(start);
                return new ReadResults(false, ComponentCommandError.EmptyComponent().WithContext(reader));
            }

            ReadResults readResults;
            foreach (IJsonArgument child in actualArray.GetChildren())
            {
                //readResults = child.ValidateComponent(reader, start, resources);
                readResults = componentReader.ValidateContents(child, components);
                if (!readResults.Successful) return readResults;
            }

            return new ReadResults(true, null);
        }
    }
}
