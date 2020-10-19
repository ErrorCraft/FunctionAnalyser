using CommandVerifier.Commands;
using CommandVerifier.ComponentParser.JsonTypes;

namespace CommandVerifier.ComponentParser
{
    public static class ComponentError
    {
        public static CommandError MalformedJsonError()
        {
            return CommandError.InvalidChatComponent("Malformed JSON");
        }

        public static CommandError ExpectedValueError()
        {
            return CommandError.InvalidChatComponent("Expected value");
        }

        public static CommandError UnterminatedEscapeSequenceError()
        {
            return CommandError.InvalidChatComponent("Unterminated escape sequence");
        }

        public static CommandError InvalidEscapeSequenceError()
        {
            return CommandError.InvalidChatComponent("Invalid escape sequence");
        }

        public static CommandError ExpectedNameValueSeparatorError()
        {
            return CommandError.InvalidChatComponent("Expected ':'");
        }

        public static CommandError EndOfInputError()
        {
            return CommandError.InvalidChatComponent("End of input");
        }

        public static CommandError UnterminatedStringError()
        {
            return CommandError.InvalidChatComponent("Unterminated string");
        }

        public static CommandError StringFormatError(string key, string expected, string received)
        {
            return CommandError.InvalidChatComponent($"Expected {key} to be {expected}, was {received}");
        }

        public static CommandError IncompleteComponentError(string key, ComponentTypes.Component component)
        {
            return CommandError.InvalidChatComponent($"A {key} component needs at least {component.GetContentsKeys()}");
        }

        public static CommandError EmptyComponentError()
        {
            return CommandError.InvalidChatComponent("empty");
        }

        public static CommandError UnknownComponentError(IComponent component)
        {
            return CommandError.InvalidChatComponent($"Don't know how to turn {component.AsJson()} into a component");
        }
    }
}
