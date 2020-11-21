namespace CommandParser.Results.Arguments
{
    public class Predicate
    {
        public ResourceLocation Value { get; }

        public Predicate(ResourceLocation predicate)
        {
            Value = predicate;
        }
    }
}
