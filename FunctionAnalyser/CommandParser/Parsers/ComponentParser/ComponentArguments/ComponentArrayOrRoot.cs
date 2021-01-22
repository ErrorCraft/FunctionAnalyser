using CommandParser.Collections;
using CommandParser.Parsers.JsonParser.JsonArguments;
using CommandParser.Results;

namespace CommandParser.Parsers.ComponentParser.ComponentArguments
{
    public class ComponentArrayOrRoot : ComponentArgument
    {
        public override ReadResults Validate(JsonObject obj, string key, ComponentReader componentReader, Components components, IStringReader reader, int start, DispatcherResources resources)
        {
            ComponentArray componentArray = new ComponentArray();
            ReadResults readResults = componentArray.Validate(obj, key, componentReader, components, reader, start, resources);

            if (!readResults.Successful)
            {
                ComponentRoot componentRoot = new ComponentRoot();
                return componentRoot.Validate(obj, key, componentReader, components, reader, start, resources);
            }

            return new ReadResults(true, null);
        }
    }
}
