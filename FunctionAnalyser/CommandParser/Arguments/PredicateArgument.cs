using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class PredicateArgument : IArgument<Predicate>
    {
        public ReadResults Parse(IStringReader reader, out Predicate result)
        {
            result = default;
            ReadResults readResults = new ResourceLocationParser(reader).Read(out ResourceLocation predicate);
            if (readResults.Successful) result = new Predicate(predicate);
            return readResults;
        }
    }
}
