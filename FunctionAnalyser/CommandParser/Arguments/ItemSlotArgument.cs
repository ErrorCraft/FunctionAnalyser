using CommandParser.Collections;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class ItemSlotArgument : IArgument<ItemSlot>
    {
        public ReadResults Parse(StringReader reader, out ItemSlot result)
        {
            result = default;
            int start = reader.Cursor;
            ReadResults readResults = reader.ReadUnquotedString(out string slot);

            if (!readResults.Successful) return readResults;
            if (!ItemSlots.Contains(slot))
            {
                reader.Cursor = start;
                return new ReadResults(false, CommandError.UnknownSlot(slot).WithContext(reader));
            }
            result = new Results.Arguments.ItemSlot(slot);
            return new ReadResults(true, null);
        }
    }
}
