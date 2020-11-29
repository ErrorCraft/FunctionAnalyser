﻿using CommandParser.Parsers.JsonParser.JsonArguments;
using CommandParser.Results;

namespace CommandParser.Parsers.ComponentParser.ComponentArguments
{
    public class ComponentNumber : ComponentArgument
    {
        public override ReadResults Validate(JsonObject obj, string key, StringReader reader, int start)
        {
            if (obj.GetChild(key) is not JsonNumber)
            {
                reader.Cursor = start;
                return new ReadResults(false, ComponentCommandError.StringFormat(key, JsonNumber.NAME, obj.GetChild(key).GetName()).WithContext(reader));
            }
            return new ReadResults(true, null);
        }
    }
}