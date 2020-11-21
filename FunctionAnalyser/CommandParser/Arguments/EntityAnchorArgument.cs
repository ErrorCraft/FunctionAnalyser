﻿using CommandParser.Collections;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class EntityAnchorArgument : IArgument<Anchor>
    {
        public ReadResults Parse(StringReader reader, out Anchor result)
        {
            result = default;
            int start = reader.Cursor;
            ReadResults readResults = reader.ReadUnquotedString(out string anchor);
            if (!readResults.Successful) return readResults;

            if (!Anchors.Contains(anchor))
            {
                reader.Cursor = start;
                return new ReadResults(false, CommandError.InvalidEntityAnchor(anchor).WithContext(reader));
            }

            result = new Anchor(anchor);
            return new ReadResults(true, null);
        }
    }
}
