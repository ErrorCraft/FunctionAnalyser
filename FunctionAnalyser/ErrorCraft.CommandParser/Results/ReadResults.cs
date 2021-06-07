namespace ErrorCraft.CommandParser.Results {
    public class ReadResults {
        public bool Successful { get; }

        private ReadResults(bool successful) {
            Successful = successful;
        }

        public static ReadResults Success() {
            return new ReadResults(true);
        }

        public static ReadResults Failure() {
            return new ReadResults(false);
        }
    }
}
