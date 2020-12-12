using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class UuidArgument : IArgument<Uuid>
    {
        public ReadResults Parse(IStringReader reader, out Uuid result)
        {
            return UuidParser.FromReader(reader, out result);
        }
    }
}
