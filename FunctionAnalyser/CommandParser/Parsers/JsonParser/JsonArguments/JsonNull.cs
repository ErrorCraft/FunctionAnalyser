﻿using CommandParser.Parsers.ComponentParser;
using CommandParser.Results;

namespace CommandParser.Parsers.JsonParser.JsonArguments
{
    public class JsonNull : IJsonArgument
    {
        public const string NAME = "Null";
        public const string NULL = "null";

        public string AsJson()
        {
            return NULL;
        }

        public string GetName() => NAME;

        public ReadResults ValidateComponent(StringReader reader, int start)
        {
            reader.Cursor = start;
            return new ReadResults(false, ComponentCommandError.UnknownComponentError(this).WithContext(reader));
        }

        public override string ToString()
        {
            return NULL;
        }
    }
}