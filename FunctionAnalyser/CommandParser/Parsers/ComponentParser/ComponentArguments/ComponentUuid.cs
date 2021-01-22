using CommandParser.Collections;
using CommandParser.Parsers.JsonParser;
using CommandParser.Parsers.JsonParser.JsonArguments;
using CommandParser.Results;

namespace CommandParser.Parsers.ComponentParser.ComponentArguments
{
    public class ComponentUuid : ComponentArgument
    {
        public override ReadResults Validate(JsonObject obj, string key, ComponentReader componentReader, Components components, IStringReader reader, int start, DispatcherResources resources)
        {
            if (!IsText(obj.GetChild(key)))
            {
                reader.SetCursor(start);
                return new ReadResults(false, ComponentCommandError.InvalidComponent(key, JsonArgumentType.String, obj.GetChild(key).GetArgumentType()).WithContext(reader));
            }
            UuidParser uuidParser = new UuidParser(obj.GetChild(key).ToString());
            if (!uuidParser.Parse(out _))
            {
                reader.SetCursor(start);
                return new ReadResults(false, CommandError.InvalidUuid().WithContext(reader));
            }
            return new ReadResults(true, null);
        }
    }
}
