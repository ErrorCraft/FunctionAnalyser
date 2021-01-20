using CommandParser.Collections;
using CommandParser.Parsers.JsonParser.JsonArguments;
using CommandParser.Results;

namespace CommandParser.Parsers.ComponentParser.ComponentArguments
{
    public class ComponentResourceLocation : ComponentArgument
    {
        public override ReadResults Validate(JsonObject obj, string key, ComponentReader componentReader, Components components, IStringReader reader, int start, DispatcherResources resources)
        {
            if (!IsText(obj.GetChild(key)))
            {
                reader.SetCursor(start);
                return new ReadResults(false, ComponentCommandError.InvalidComponent(key, JsonString.NAME, obj.GetChild(key).GetName()).WithContext(reader));
            }
            return new ResourceLocationParser(reader).ReadFromString(obj.GetChild(key).ToString(), start, out _);
        }
    }
}
