namespace CommandParser.Results.Arguments
{
    public class Enchantment
    {
        public ResourceLocation Value { get; }

        public Enchantment(ResourceLocation enchantment)
        {
            Value = enchantment;
        }
    }
}
