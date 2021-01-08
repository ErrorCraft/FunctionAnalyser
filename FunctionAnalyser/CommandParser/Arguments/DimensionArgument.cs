using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class DimensionArgument : IArgument<Dimension>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Dimension result)
        {
            result = default;
            ReadResults readResults = new ResourceLocationParser(reader).Read(out ResourceLocation dimension);
            if (readResults.Successful) result = new Dimension(dimension);
            return readResults;
        }
    }
}
