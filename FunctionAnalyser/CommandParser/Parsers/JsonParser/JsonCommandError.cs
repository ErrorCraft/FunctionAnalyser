using static CommandParser.Parsers.JsonParser.JsonCharacterProvider;

namespace CommandParser.Parsers.JsonParser
{
    public static class JsonCommandError
    {
        public static CommandError MalformedJson()
        {
            return CommandError.InvalidChatComponent($"Malformed JSON");
        }

        public static CommandError UnterminatedEscapeSequence()
        {
            return CommandError.InvalidChatComponent($"Unterminated escape sequence");
        }

        public static CommandError InvalidUnicodeCharacter(string unicode)
        {
            return CommandError.InvalidChatComponent($"{ESCAPE_CHARACTER}{UNICODE_CHARACTER}{unicode}");
        }

        public static CommandError InvalidEscapeSequence()
        {
            return CommandError.InvalidChatComponent($"Invalid escape sequence");
        }

        public static CommandError UnterminatedString()
        {
            return CommandError.InvalidChatComponent($"Unterminated string");
        }

        public static CommandError ExpectedNameValueSeparator()
        {
            return CommandError.InvalidChatComponent($"Expected '{NAME_VALUE_SEPARATOR}'");
        }

        public static CommandError EndOfInput()
        {
            return CommandError.InvalidChatComponent($"End of input");
        }
    }
}
