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
                return ReadResults.Failure(ComponentCommandError.InvalidComponent(key, JsonArgumentType.String, obj.GetChild(key).GetArgumentType()).WithContext(reader));
            }

            if (ResourceLocation.TryParse(obj.GetChild(key).ToString(), out _))
            {
                return ReadResults.Success();
            } else
            {
                reader.SetCursor(start);
                return ReadResults.Failure(CommandError.InvalidId().WithContext(reader));
            }
        }
    }
}
