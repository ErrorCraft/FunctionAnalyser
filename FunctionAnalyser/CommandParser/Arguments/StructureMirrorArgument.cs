using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class StructureMirrorArgument : IArgument<StructureMirror>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out StructureMirror result)
        {
            result = default;
            ReadResults readResults = reader.ReadUnquotedString(out string structureMirror);
            if (!readResults.Successful) return readResults;

            if (!resources.StructureMirrors.Contains(structureMirror))
            {
                return new ReadResults(false, CommandError.UnknownStructureMirror(structureMirror));
            }

            result = new StructureMirror(structureMirror);
            return new ReadResults(true, null);
        }
    }
}
