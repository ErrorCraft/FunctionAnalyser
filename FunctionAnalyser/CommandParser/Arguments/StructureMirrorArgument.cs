using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class StructureMirrorArgument : IArgument<StructureMirror>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out StructureMirror result)
        {
            result = default;
            return new ReadResults(true, null);
        }
    }
}
