using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class StructureRotationArgument : IArgument<StructureRotation>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out StructureRotation result)
        {
            result = default;
            return new ReadResults(true, null);
        }
    }
}
