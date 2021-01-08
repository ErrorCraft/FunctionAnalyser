using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class StorageArgument : IArgument<Storage>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Storage result)
        {
            result = default;
            ReadResults readResults = new ResourceLocationParser(reader).Read(out ResourceLocation storage);
            if (readResults.Successful) result = new Storage(storage);
            return readResults;
        }
    }
}
