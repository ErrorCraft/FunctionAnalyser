using CommandParser.Parsers.JsonParser.JsonArguments;

namespace CommandParser.Results.Arguments
{
    public class ItemComponent
    {
        public IJsonArgument Argument { get; }

        public ItemComponent(IJsonArgument argument)
        {
            Argument = argument;
        }
    }
}
