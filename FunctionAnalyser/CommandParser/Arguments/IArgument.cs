using CommandParser.Results;

namespace CommandParser.Arguments
{
    public interface IArgument<T>
    {
        ReadResults Parse(IStringReader reader, DispatcherResources resources, out T result);
    }
}
