using System.Collections.Immutable;

namespace CommandParser.Parsers.JsonParser
{
    public static class JsonCharacterProvider
    {
        public const char OBJECT_OPEN_CHARACTER = '{';
        public const char OBJECT_CLOSE_CHARACTER = '}';
        public const char ARRAY_OPEN_CHARACTER = '[';
        public const char ARRAY_CLOSE_CHARACTER = ']';
        public const char NAME_VALUE_SEPARATOR = ':';
        public const char ARGUMENT_SEPARATOR = ',';
        public const char STRING_CHARACTER = '"';
        public static readonly ImmutableHashSet<char> ESCAPABLE_CHARACTERS = ImmutableHashSet.Create('\'', '"', '\\', 'b', 'f', 'n', 'r', 't');
        public const char UNICODE_CHARACTER = 'u';
        public const char ESCAPE_CHARACTER = '\\';
    }
}
