﻿using static CommandParser.Parsers.JsonParser.JsonCharacterProvider;

namespace CommandParser.Parsers.JsonParser.JsonArguments
{
    public class JsonString : IJsonArgument
    {
        public const string NAME = "String";

        private readonly string String;

        public JsonString(string s)
        {
            String = s;
        }

        public string AsJson()
        {
            return $"{STRING_CHARACTER}{String}{STRING_CHARACTER}";
        }

        public string GetName() => NAME;

        public JsonArgumentType GetArgumentType()
        {
            return JsonArgumentType.String;
        }

        public static implicit operator string(JsonString s)
        {
            return s.String;
        }

        public override string ToString()
        {
            return String;
        }
    }
}
