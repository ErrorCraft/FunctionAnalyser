﻿using CommandParser.Collections;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class ItemSlotArgument : IArgument<ItemSlot>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out ItemSlot result)
        {
            result = default;
            int start = reader.GetCursor();
            ReadResults readResults = reader.ReadUnquotedString(out string slot);

            if (!readResults.Successful) return readResults;
            if (!ItemSlots.Contains(slot))
            {
                reader.SetCursor(start);
                return new ReadResults(false, CommandError.UnknownSlot(slot).WithContext(reader));
            }
            result = new Results.Arguments.ItemSlot(slot);
            return new ReadResults(true, null);
        }
    }
}
