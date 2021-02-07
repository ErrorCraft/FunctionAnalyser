using CommandParser.Minecraft;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class ItemModifierArgument : IArgument<ItemModifier>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out ItemModifier result)
        {
            result = default;
            ReadResults readResults = ResourceLocation.TryRead(reader, out ResourceLocation itemModifier);
            if (readResults.Successful) result = new ItemModifier(itemModifier);
            return readResults;
        }
    }
}
