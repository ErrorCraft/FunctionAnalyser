using CommandParser.Results;

namespace CommandParser.Arguments
{
    public interface IArgument<T>
    {
        ReadResults Parse(IStringReader reader, out T result);
    }
}
