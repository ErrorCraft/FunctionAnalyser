using ErrorCraft.CommandParser.Results;

namespace ErrorCraft.CommandParser.Arguments {
    public class IntegerArgument : IArgument<int> {
        public ParseResults Parse(IStringReader reader, out int result) {
            return reader.ReadInteger(out result);
        }
    }
}
