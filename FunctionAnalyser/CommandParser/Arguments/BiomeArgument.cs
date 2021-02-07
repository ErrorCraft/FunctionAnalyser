using CommandParser.Minecraft;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class BiomeArgument : IArgument<Biome>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Biome result)
        {
            result = default;
            ReadResults readResults = ResourceLocation.TryRead(reader, out ResourceLocation biome);
            if (readResults.Successful) result = new Biome(biome);
            return readResults;
        }
    }
}
