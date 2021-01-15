using CommandParser.Results;

namespace CommandParser.Parsers.JsonParser.JsonArguments
{
    public interface IJsonArgument
    {
        string GetName();
        string AsJson();
    }
}
