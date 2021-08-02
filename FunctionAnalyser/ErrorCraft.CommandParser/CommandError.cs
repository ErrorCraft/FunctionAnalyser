namespace ErrorCraft.CommandParser {
    public class CommandError {
        private string Message;

        public CommandError(string message) {
            Message = message;
        }

        public string GetMessage() {
            return Message;
        }

        public static CommandError InvalidBoolean() => new CommandError("Invalid boolean");
    }
}
