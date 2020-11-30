namespace CommandParser.Results.Arguments
{
    public class ItemModifier
    {
        public ResourceLocation Value { get; }

        public ItemModifier(ResourceLocation itemModifier)
        {
            Value = itemModifier;
        }
    }
}
