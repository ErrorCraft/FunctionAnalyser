using CommandParser.Parsers.JsonParser.JsonArguments;
using CommandParser.Results;

namespace CommandParser.Parsers.ComponentParser.ComponentArguments
{
    public class ComponentString : ComponentArgument
    {
        public override ReadResults Validate(JsonObject obj, string key, StringReader reader, int start)
        {
            if (!IsText(obj.GetChild(key)))
            {
                reader.Cursor = start;
                return new ReadResults(false, ComponentCommandError.StringFormat(key, JsonString.NAME, obj.GetChild(key).GetName()).WithContext(reader));
            }
            return ValidateChildren(obj, key, reader, start);
        }
    }
}
