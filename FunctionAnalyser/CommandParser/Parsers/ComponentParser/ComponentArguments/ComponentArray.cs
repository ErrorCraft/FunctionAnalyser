using CommandParser.Parsers.JsonParser.JsonArguments;
using CommandParser.Results;

namespace CommandParser.Parsers.ComponentParser.ComponentArguments
{
    public class ComponentArray : ComponentArgument
    {
        public override ReadResults Validate(JsonObject obj, string key, StringReader reader, int start)
        {
            if (obj.GetChild(key) is not JsonArray actualArray)
            {
                reader.Cursor = start;
                return new ReadResults(false, ComponentCommandError.StringFormat(key, JsonObject.Name, obj.GetChild(key).GetName()).WithContext(reader));
            }

            ReadResults readResults;
            foreach (IJsonArgument child in actualArray.GetChildren())
            {
                readResults = child.ValidateComponent(reader, start);
                if (!readResults.Successful) return readResults;
            }

            return new ReadResults(true, null);
        }
    }
}
