namespace ErrorCraft.CommandParser {
    public class CommandError {
        private string Message;

        public CommandError(string message) {
            Message = message;
        }

        public string GetMessage() {
            return Message;
        }
    }
}
