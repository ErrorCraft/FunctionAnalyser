using CommandParser.Context;
using System.Collections.Generic;

namespace CommandParser.Results
{
    public class ParseResults
    {
        public CommandContext Context { get; }
        public IStringReader Reader { get; }
        public List<CommandError> Errors { get; }

        public ParseResults(CommandContext context, IStringReader reader, List<CommandError> errors)
        {
            Context = context;
            Reader = reader;
            Errors = errors;
        }
    }
}
