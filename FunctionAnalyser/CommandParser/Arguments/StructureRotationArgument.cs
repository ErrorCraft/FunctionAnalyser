using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class StructureRotationArgument : IArgument<StructureRotation>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out StructureRotation result)
        {
            result = default;
            int start = reader.GetCursor();
            while (reader.CanRead() && IsUnquotedStringPart(reader.Peek())) reader.Skip();
            string structureRotation = reader.GetString()[start..reader.GetCursor()];

            if (!resources.StructureRotations.Contains(structureRotation))
            {
                return new ReadResults(false, CommandError.UnknownStructureRotation(structureRotation));
            }

            result = new StructureRotation(structureRotation);
            return new ReadResults(true, null);
        }

        private static bool IsUnquotedStringPart(char c)
        {
            return c >= '0' && c <= '9' ||
                c >= 'A' && c <= 'Z' ||
                c >= 'a' && c <= 'z' ||
                c == '_' || c == '-' ||
                c == '.' || c == '§';
        }
    }
}
