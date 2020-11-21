namespace CommandParser.Results
{
    public class ReadResults
    {
        public bool Successful { get; }
        public CommandError Error { get; }

        public ReadResults(bool successful, CommandError error)
        {
            Successful = successful;
            Error = error;
        }
    }
}
