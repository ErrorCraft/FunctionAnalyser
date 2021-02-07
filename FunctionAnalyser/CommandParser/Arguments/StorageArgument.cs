using CommandParser.Minecraft;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class StorageArgument : IArgument<Storage>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Storage result)
        {
            result = default;
            ReadResults readResults = ResourceLocation.TryRead(reader, out ResourceLocation storage);
            if (readResults.Successful) result = new Storage(storage);
            return readResults;
        }
    }
}
