namespace ErrorCraft.CommandParser.Tree {
    public class LiteralNode : Node {
        private readonly string Literal;

        public LiteralNode(string literal) {
            Literal = literal;
        }

        public override string GetName() {
            return Literal;
        }
    }
}
