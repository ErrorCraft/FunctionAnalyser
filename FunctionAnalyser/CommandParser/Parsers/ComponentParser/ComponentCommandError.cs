using CommandParser.Parsers.ComponentParser.ComponentArguments;
using CommandParser.Parsers.JsonParser;
using CommandParser.Parsers.JsonParser.JsonArguments;
using Utilities;

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

        public static CommandError InvalidComponent(string key, JsonArgumentType expected, JsonArgumentType received)
        {
            return CommandError.InvalidChatComponent($"Expected {key} to be {expected.GetDisplayName()}, was {received.GetDisplayName()}");
        }

        public static CommandError InvalidComponentArray(string key, JsonArgumentType expected, JsonArgumentType received)
        {
            return CommandError.InvalidChatComponent($"Expected values in {key} array to be {expected.GetDisplayName()}, was {received.GetDisplayName()}");
        }

        public static CommandError IncompleteComponent(string key, ComponentArgument component)
        {
            return CommandError.InvalidChatComponent($"A {key} component needs at least {component.StringifyChildrenKeys()}");
        }
    }
}
