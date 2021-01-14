using CommandParser.Collections;
using CommandParser.Parsers.JsonParser.JsonArguments;
using CommandParser.Results;

namespace CommandParser.Parsers.ComponentParser.ComponentArguments
{
    public class ComponentObject : ComponentArgument
    {
        public override ReadResults Validate(JsonObject obj, string key, ComponentReader componentReader, Components components, IStringReader reader, int start, DispatcherResources resources)
        {
            if (obj.GetChild(key) is not JsonObject actualObject)
            {
                reader.SetCursor(start);
                return new ReadResults(false, ComponentCommandError.StringFormat(key, JsonObject.Name, obj.GetChild(key).GetName()).WithContext(reader));
            }
            return ValidateChildren(actualObject, key, componentReader, components, reader, start, resources);
        }
    }
}
