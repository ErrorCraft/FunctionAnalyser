using CommandParser.Collections;
using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class ItemEnchantmentArgument : IArgument<Enchantment>
    {
        public ReadResults Parse(StringReader reader, out Enchantment result)
        {
            result = default;
            ReadResults readResults = new ResourceLocationParser(reader).Read(out ResourceLocation enchantment);
            if (!readResults.Successful) return readResults;
            if (!Enchantments.Contains(enchantment))
            {
                return new ReadResults(false, CommandError.UnknownEnchantment(enchantment));
            }
            result = new Enchantment(enchantment);
            return new ReadResults(true, null);
        }
    }
}
