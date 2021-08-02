namespace ErrorCraft.CommandParser.Results {
    public class ParseResults {
        public bool Successful { get; }
        public CommandError Error { get; }

        private ParseResults(bool successful, CommandError error) {
            Successful = successful;
            Error = error;
        }

        public static ParseResults Success() {
            return new ParseResults(true, null);
        }

        public static ParseResults Failure(CommandError error) {
            return new ParseResults(false, error);
        }
    }
}
