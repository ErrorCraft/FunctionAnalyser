using CommandParser.Minecraft;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class DimensionArgument : IArgument<Dimension>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Dimension result)
        {
            result = default;
            ReadResults readResults = ResourceLocation.TryRead(reader, out ResourceLocation dimension);
            if (readResults.Successful) result = new Dimension(dimension);
            return readResults;
        }
    }
}
