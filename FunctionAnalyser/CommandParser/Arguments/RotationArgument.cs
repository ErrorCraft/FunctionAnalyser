using CommandParser.Parsers.Coordinates;
using CommandParser.Results;
using CommandParser.Results.Arguments.Coordinates;

namespace CommandParser.Arguments
{
    public class RotationArgument : IArgument<Rotation>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Rotation result)
        {
            return new RotationParser(reader).Parse(out result);
        }
    }
}
