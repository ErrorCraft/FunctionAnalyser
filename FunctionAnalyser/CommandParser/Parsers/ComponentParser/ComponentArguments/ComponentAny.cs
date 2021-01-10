using CommandParser.Parsers.JsonParser.JsonArguments;
using CommandParser.Results;

namespace CommandParser.Parsers.ComponentParser.ComponentArguments
{
    public class ComponentAny : ComponentArgument
    {
        public override ReadResults Validate(JsonObject obj, string key, IStringReader reader, int start, DispatcherResources resources)
        {
            return obj.GetChild(key).ValidateComponent(reader, start, resources);
        }
    }
}
