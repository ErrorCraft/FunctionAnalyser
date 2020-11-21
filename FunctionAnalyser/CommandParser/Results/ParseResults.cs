using CommandParser.Context;
using System.Collections.Generic;

namespace CommandParser.Results
{
    public class ParseResults
    {
        public CommandContext Context { get; }
        public StringReader Reader { get; }
        public List<CommandError> Errors { get; }

        public ParseResults(CommandContext context, StringReader reader, List<CommandError> errors)
        {
            Context = context;
            Reader = reader;
            Errors = errors;
        }
    }
}
