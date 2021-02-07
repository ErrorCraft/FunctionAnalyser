using CommandParser.Minecraft;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class PredicateArgument : IArgument<Predicate>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Predicate result)
        {
            result = default;
            ReadResults readResults = ResourceLocation.TryRead(reader, out ResourceLocation predicate);
            if (readResults.Successful) result = new Predicate(predicate);
            return readResults;
        }
    }
}
