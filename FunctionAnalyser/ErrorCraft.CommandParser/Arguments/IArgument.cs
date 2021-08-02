using ErrorCraft.CommandParser.Results;

namespace ErrorCraft.CommandParser.Arguments {
    public interface IArgument<T> {
        ParseResults Parse(IStringReader reader, out T result);
    }
}
