using CommandParser.Parsers.NbtParser;
using CommandParser.Parsers.NbtParser.NbtArguments;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class NbtTagArgument : IArgument<Nbt>
    {
        public ReadResults Parse(StringReader reader, out Nbt result)
        {
            ReadResults readResults = NbtReader.ReadValue(reader, out INbtArgument nbtResult);
            result = new Nbt(nbtResult);
            return readResults;
        }
    }
}
