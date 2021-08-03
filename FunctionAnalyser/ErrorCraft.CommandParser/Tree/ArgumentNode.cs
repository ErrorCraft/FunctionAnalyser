using ErrorCraft.CommandParser.Results;

namespace ErrorCraft.CommandParser.Tree {
    public class ArgumentNode<T> : Node {
        private readonly string Name;

        public ArgumentNode(string name) {
            Name = name;
        }

        public override string GetName() {
            return Name;
        }

        public override ParseResults Parse(IStringReader reader) {
            throw new System.NotImplementedException();
        }
    }
}
