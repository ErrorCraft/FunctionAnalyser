using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class ResourceLocationArgument : IArgument<ResourceLocation>
    {
        public ReadResults Parse(IStringReader reader, out ResourceLocation result)
        {
            return new ResourceLocationParser(reader).Read(out result);
        }
    }
}
