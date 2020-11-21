using CommandParser.Results;

namespace CommandParser.Arguments
{
    public interface IArgument<T>
    {
        ReadResults Parse(StringReader reader, out T result);
    }
}
