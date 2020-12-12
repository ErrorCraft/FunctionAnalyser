using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class ItemModifierArgument : IArgument<ItemModifier>
    {
        public ReadResults Parse(IStringReader reader, out ItemModifier result)
        {
            result = default;
            ReadResults readResults = new ResourceLocationParser(reader).Read(out ResourceLocation itemModifier);
            if (readResults.Successful) result = new ItemModifier(itemModifier);
            return readResults;
        }
    }
}
