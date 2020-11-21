using CommandParser.Parsers.JsonParser.JsonArguments;

namespace CommandParser.Results.Arguments
{
    public class Component
    {
        public IJsonArgument Argument { get; }

        public Component(IJsonArgument argument)
        {
            Argument = argument;
        }
    }
}
