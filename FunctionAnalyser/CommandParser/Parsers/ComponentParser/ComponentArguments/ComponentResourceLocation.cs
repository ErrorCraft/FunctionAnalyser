using CommandParser.Collections;
using CommandParser.Minecraft;
using CommandParser.Parsers.JsonParser;
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
                return new ReadResults(false, ComponentCommandError.InvalidComponent(key, JsonArgumentType.String, obj.GetChild(key).GetArgumentType()).WithContext(reader));
            }

            if (ResourceLocation.TryParse(obj.GetChild(key).ToString(), out _))
            {
                return new ReadResults(true, null);
            } else
            {
                reader.SetCursor(start);
                return new ReadResults(false, CommandError.InvalidId().WithContext(reader));
            }
        }
    }
}
