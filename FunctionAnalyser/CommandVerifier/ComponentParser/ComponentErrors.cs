using CommandVerifier.Commands;
using CommandVerifier.ComponentParser.JsonTypes;

namespace CommandVerifier.ComponentParser
{
    public static class ComponentErrors
    {
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
