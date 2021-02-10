using CommandParser.Results;
using Utilities;

namespace CommandParser.Arguments {
    public class UuidArgument : IArgument<UUID> {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out UUID result) {
            int start = reader.GetCursor();
            while (reader.CanRead() && IsUuidPart(reader.Peek())) {
                reader.Skip();
            }
            string uuid = reader.GetString()[start..reader.GetCursor()];
            if (!UUID.TryParse(uuid, out result)) {
                return ReadResults.Failure(CommandError.InvalidUuid());
            }
            return ReadResults.Success();
        }

        private static bool IsUuidPart(char c) {
            return c >= '0' && c <= '9' ||
                   c >= 'a' && c <= 'f' ||
                   c >= 'A' && c <= 'F' ||
                   c == '-';
        }
    }
}
