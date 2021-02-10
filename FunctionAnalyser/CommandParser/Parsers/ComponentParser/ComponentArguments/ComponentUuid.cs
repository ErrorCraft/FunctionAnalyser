using CommandParser.Collections;
using CommandParser.Parsers.JsonParser;
using CommandParser.Parsers.JsonParser.JsonArguments;
using CommandParser.Results;
using Utilities;

namespace CommandParser.Parsers.ComponentParser.ComponentArguments {
    public class ComponentUuid : ComponentArgument {
        public override ReadResults Validate(JsonObject obj, string key, ComponentReader componentReader, Components components, IStringReader reader, int start, DispatcherResources resources) {
            if (!IsText(obj.GetChild(key))) {
                reader.SetCursor(start);
                return ReadResults.Failure(ComponentCommandError.InvalidComponent(key, JsonArgumentType.String, obj.GetChild(key).GetArgumentType()).WithContext(reader));
            }
            if (!UUID.TryParse(obj.GetChild(key).ToString(), out _)) {
                reader.SetCursor(start);
                return ReadResults.Failure(CommandError.InvalidUuid().WithContext(reader));
            }
            return ReadResults.Success();
        }
    }
}
