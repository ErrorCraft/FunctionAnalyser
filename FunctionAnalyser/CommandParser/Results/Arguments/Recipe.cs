namespace CommandParser.Results.Arguments
{
    public class Recipe
    {
        public ResourceLocation Value { get; }

        public Recipe(ResourceLocation recipe)
        {
            Value = recipe;
        }
    }
}
