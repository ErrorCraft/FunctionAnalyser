namespace ErrorCraft.CommandParser.Results {
    public class ParseResults {
        public bool Successful { get; }

        private ParseResults(bool successful) {
            Successful = successful;
        }

        public static ParseResults Success() {
            return new ParseResults(true);
        }

        public static ParseResults Failure() {
            return new ParseResults(false);
        }
    }
}
