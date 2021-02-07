using CommandParser.Minecraft;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class RecipeArgument : IArgument<Recipe>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Recipe result)
        {
            result = default;
            ReadResults readResults = ResourceLocation.TryRead(reader, out ResourceLocation recipe);
            if (readResults.Successful) result = new Recipe(recipe);
            return readResults;
        }
    }
}
