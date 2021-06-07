using ErrorCraft.CommandParser.Results;

namespace ErrorCraft.CommandParser.Arguments {
    public interface IArgument<T> {
        ReadResults Parse(IStringReader reader, out T result);
    }
}
