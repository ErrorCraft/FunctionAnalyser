namespace CommandParser.Results
{
    public class ReadResults
    {
        public bool Successful { get; }
        public CommandError Error { get; }

        private ReadResults(bool successful, CommandError error)
        {
            Successful = successful;
            Error = error;
        }

        public static ReadResults Success()
        {
            return new ReadResults(true, null);
        }

        public static ReadResults Failure(CommandError error)
        {
            return new ReadResults(false, error);
        }
    }
}
