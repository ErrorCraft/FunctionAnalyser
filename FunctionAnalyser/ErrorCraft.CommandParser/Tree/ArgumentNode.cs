using ErrorCraft.CommandParser.Arguments;
using ErrorCraft.CommandParser.Results;

namespace ErrorCraft.CommandParser.Tree {
    public class ArgumentNode<T> : Node {
        private readonly string Name;
        private readonly IArgument<T> Argument;

        public ArgumentNode(string name, IArgument<T> argument) : this(name, argument, false) { }

        public ArgumentNode(string name, IArgument<T> argument, bool executable) : base(executable) {
            Name = name;
            Argument = argument;
        }

        public override string GetName() {
            return Name;
        }

        public override ParseResults Parse(IStringReader reader) {
            return Argument.Parse(reader, out _);
        }
    }
}
