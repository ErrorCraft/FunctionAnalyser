namespace CommandParser.Results.Arguments
{
    public class LootTable
    {
        public ResourceLocation Value { get; }

        public LootTable(ResourceLocation lootTable)
        {
            Value = lootTable;
        }
    }
}
