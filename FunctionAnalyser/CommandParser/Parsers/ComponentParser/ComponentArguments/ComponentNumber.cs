using CommandParser.Collections;
using CommandParser.Parsers.JsonParser.JsonArguments;
using CommandParser.Results;

namespace CommandParser.Parsers.ComponentParser.ComponentArguments
{
    public class ComponentNumber : ComponentArgument
    {
        public override ReadResults Validate(JsonObject obj, string key, ComponentReader componentReader, Components components, IStringReader reader, int start, DispatcherResources resources)
        {
            if (obj.GetChild(key) is not JsonNumber)
            {
                reader.SetCursor(start);
                return new ReadResults(false, ComponentCommandError.StringFormat(key, JsonNumber.NAME, obj.GetChild(key).GetName()).WithContext(reader));
            }
            return new ReadResults(true, null);
        }
    }
}
