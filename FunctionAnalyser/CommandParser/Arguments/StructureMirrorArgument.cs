using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class StructureMirrorArgument : IArgument<StructureMirror>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out StructureMirror result)
        {
            result = default;
            int start = reader.GetCursor();
            while (reader.CanRead() && IsUnquotedStringPart(reader.Peek())) reader.Skip();
            string structureMirror = reader.GetString()[start..reader.GetCursor()];

            if (!resources.StructureMirrors.Contains(structureMirror))
            {
                return ReadResults.Failure(CommandError.UnknownStructureMirror(structureMirror));
            }

            result = new StructureMirror(structureMirror);
            return ReadResults.Success();
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
