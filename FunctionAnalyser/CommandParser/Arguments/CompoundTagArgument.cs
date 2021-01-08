using CommandParser.Parsers.NbtParser;
using CommandParser.Parsers.NbtParser.NbtArguments;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class CompoundTagArgument : IArgument<Nbt>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Nbt result)
        {
            ReadResults readResults = NbtReader.ReadCompound(reader, out NbtCompound nbtResult);
            result = new Nbt(nbtResult);
            return readResults;
        }
    }
}
