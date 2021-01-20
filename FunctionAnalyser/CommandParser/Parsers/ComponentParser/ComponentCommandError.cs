using CommandParser.Parsers.ComponentParser.ComponentArguments;
using CommandParser.Parsers.JsonParser.JsonArguments;

namespace CommandParser.Parsers.ComponentParser
{
    public static class ComponentCommandError
    {
        public static CommandError UnknownComponent(IJsonArgument json)
        {
            return CommandError.InvalidChatComponent($"Don't know how to turn {json.AsJson()} into a component");
        }

        public static CommandError EmptyComponent()
        {
            return CommandError.InvalidChatComponent($"Empty array");
        }

        public static CommandError InvalidComponent(string key, string expected, string received)
        {
            return CommandError.InvalidChatComponent($"Expected {key} to be {expected}, was {received}");
        }

        public static CommandError InvalidComponentArray(string key, string expected, string received)
        {
            return CommandError.InvalidChatComponent($"Expected values in {key} array to be {expected}, was {received}");
        }

        public static CommandError IncompleteComponent(string key, ComponentArgument component)
        {
            return CommandError.InvalidChatComponent($"A {key} component needs at least {component.StringifyChildrenKeys()}");
        }
    }
}
