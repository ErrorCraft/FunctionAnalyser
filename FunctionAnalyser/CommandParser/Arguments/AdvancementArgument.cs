using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class AdvancementArgument : IArgument<Advancement>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Advancement result)
        {
            result = default;
            ReadResults readResults = new ResourceLocationParser(reader).Read(out ResourceLocation advancement);
            if (readResults.Successful) result = new Advancement(advancement);
            return readResults;
        }
    }
}
