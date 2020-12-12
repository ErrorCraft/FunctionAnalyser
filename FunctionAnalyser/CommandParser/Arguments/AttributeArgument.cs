using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class AttributeArgument : IArgument<Attribute>
    {
        public ReadResults Parse(IStringReader reader, out Attribute result)
        {
            result = default;
            ReadResults readResults = new ResourceLocationParser(reader).Read(out ResourceLocation attribute);
            if (readResults.Successful) result = new Attribute(attribute);
            return readResults;
        }
    }
}
