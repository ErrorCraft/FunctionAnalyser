namespace ErrorCraft.CommandParser {
    public class CommandError {
        private string Message;

        public CommandError(string message) {
            Message = message;
        }

        public string GetMessage() {
            return Message;
        }

        public static CommandError ExpectedLiteral(string literal) => new CommandError($"Expected literal {literal}");
        public static CommandError InvalidBoolean() => new CommandError("Invalid boolean");
    }
}
