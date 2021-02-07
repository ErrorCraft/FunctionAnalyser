using CommandParser.Minecraft;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class LootTableArgument : IArgument<LootTable>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out LootTable result)
        {
            result = default;
            ReadResults readResults = ResourceLocation.TryRead(reader, out ResourceLocation lootTable);
            if (readResults.Successful) result = new LootTable(lootTable);
            return readResults;
        }
    }
}
