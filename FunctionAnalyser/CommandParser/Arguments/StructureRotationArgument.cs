using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class StructureRotationArgument : IArgument<StructureRotation>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out StructureRotation result)
        {
            result = default;
            ReadResults readResults = reader.ReadUnquotedString(out string structureRotation);
            if (!readResults.Successful) return readResults;

            if (!resources.StructureRotations.Contains(structureRotation))
            {
                return new ReadResults(false, CommandError.UnknownStructureRotation(structureRotation));
            }

            result = new StructureRotation(structureRotation);
            return new ReadResults(true, null);
        }
    }
}
