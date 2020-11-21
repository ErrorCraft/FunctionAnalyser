using CommandParser.Context;
using System.Collections.Generic;

namespace CommandParser.Results
{
    public class CommandResults
    {
        public bool Successful { get; }
        public CommandError Error { get; }
        public List<ParsedArgument> Arguments { get; }

        public CommandResults(bool successful, CommandError error, List<ParsedArgument> arguments)
        {
            Successful = successful;
            Error = error;
            Arguments = arguments;
        }
    }
}
