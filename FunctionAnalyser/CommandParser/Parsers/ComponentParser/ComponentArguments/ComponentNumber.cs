using CommandParser.Collections;
using CommandParser.Parsers.JsonParser;
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
                return ReadResults.Failure(ComponentCommandError.InvalidComponent(key, JsonArgumentType.Number, obj.GetChild(key).GetArgumentType()).WithContext(reader));
            }
            return ReadResults.Success();
        }
    }
}
