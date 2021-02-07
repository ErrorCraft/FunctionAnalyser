using CommandParser.Minecraft;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class ItemEnchantmentArgument : IArgument<Enchantment>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Enchantment result)
        {
            result = default;
            ReadResults readResults = ResourceLocation.TryRead(reader, out ResourceLocation enchantment);
            if (!readResults.Successful) return readResults;
            if (!resources.Enchantments.Contains(enchantment))
            {
                return new ReadResults(false, CommandError.UnknownEnchantment(enchantment));
            }
            result = new Enchantment(enchantment);
            return new ReadResults(true, null);
        }
    }
}
