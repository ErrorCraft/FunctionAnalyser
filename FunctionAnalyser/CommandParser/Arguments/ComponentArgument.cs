using CommandParser.Parsers.ComponentParser;
using CommandParser.Parsers.JsonParser;
using CommandParser.Parsers.JsonParser.JsonArguments;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class ComponentArgument : IArgument<Component>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Component result)
        {
            result = default;
            if (!reader.CanRead()) return new ReadResults(false, CommandError.IncorrectArgument().WithContext(reader));
            int start = reader.GetCursor();

            ReadResults readResults = new JsonReader(reader).ReadAny(out IJsonArgument json);
            if (!readResults.Successful) return readResults;
            result = new Component(json);
            return new ComponentReader(reader, start, resources).ValidateFromRoot(json, resources.Components);
        }
    }
}
