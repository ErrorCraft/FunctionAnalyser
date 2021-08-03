using ErrorCraft.CommandParser.Results;

namespace ErrorCraft.CommandParser.Tree {
    public class LiteralNode : Node {
        private readonly string Literal;

        public LiteralNode(string literal) {
            Literal = literal;
        }

        public override string GetName() {
            return Literal;
        }

        public override ParseResults Parse(IStringReader reader) {
            if (ParseLiteral(reader) == -1) {
                return ParseResults.Failure(CommandError.ExpectedLiteral(Literal));
            }
            return ParseResults.Success();
        }

        private int ParseLiteral(IStringReader reader) {
            int start = reader.GetCursor();
            if (!reader.CanRead(Literal.Length)) {
                return -1;
            }
            int end = start + Literal.Length;
            if (reader.GetString()[start..end] != Literal) {
                return -1;
            }
            reader.Skip(Literal.Length);
            if (reader.CanRead() && reader.Peek() != ' ') {
                reader.SetCursor(start);
                return -1;
            }
            return end;
        }
    }
}
