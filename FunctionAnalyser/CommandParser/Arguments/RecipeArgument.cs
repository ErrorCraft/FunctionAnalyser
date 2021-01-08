using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class RecipeArgument : IArgument<Recipe>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Recipe result)
        {
            result = default;
            ReadResults readResults = new ResourceLocationParser(reader).Read(out ResourceLocation recipe);
            if (readResults.Successful) result = new Recipe(recipe);
            return readResults;
        }
    }
}
