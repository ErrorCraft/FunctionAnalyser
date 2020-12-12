using CommandParser.Results;

namespace CommandParser.Parsers.JsonParser.JsonArguments
{
    public interface IJsonArgument
    {
        ReadResults ValidateComponent(IStringReader reader, int start);
        string GetName();
        string AsJson();
    }
}
