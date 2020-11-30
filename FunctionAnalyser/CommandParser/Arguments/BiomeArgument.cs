using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class BiomeArgument : IArgument<Biome>
    {
        public ReadResults Parse(StringReader reader, out Biome result)
        {
            result = default;
            ReadResults readResults = new ResourceLocationParser(reader).Read(out ResourceLocation biome);
            if (readResults.Successful) result = new Biome(biome);
            return readResults;
        }
    }
}
