﻿using ErrorCraft.CommandParser.Results;

namespace ErrorCraft.CommandParser.Arguments {
    public class BooleanArgument : IArgument<bool> {
        public ParseResults Parse(IStringReader reader, out bool result) {
            return reader.ReadBoolean(out result);
        }
    }
}