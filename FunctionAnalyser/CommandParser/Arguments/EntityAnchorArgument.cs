using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class EntityAnchorArgument : IArgument<Anchor>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Anchor result)
        {
            result = default;
            int start = reader.GetCursor();
            ReadResults readResults = reader.ReadUnquotedString(out string anchor);
            if (!readResults.Successful) return readResults;

            if (!resources.Anchors.Contains(anchor))
            {
                reader.SetCursor(start);
                return ReadResults.Failure(CommandError.InvalidEntityAnchor(anchor).WithContext(reader));
            }

            result = new Anchor(anchor);
            return ReadResults.Success();
        }
    }
}
