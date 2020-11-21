using System.Collections.Generic;

namespace CommandParser.Context
{
    public class CommandContext
    {
        public List<ParsedArgument> Results { get; }
        public Range Range { get; private set; }
        public bool Executable { get; private set; }

        public CommandContext(int start)
        {
            Results = new List<ParsedArgument>();
            Range = Range.At(start);
        }

        private CommandContext(List<ParsedArgument> results, Range range, bool executable) 
        {
            Range = range;
            Results = results;
            Executable = executable;
        }

        public void Executes(bool executable)
        {
            Executable = executable;
        }

        public void AddArgument(ParsedArgument argument)
        {
            Results.Add(argument);
        }

        public void EncompassRange(Range range)
        {
            Range = Range.Encompassing(Range, range);
        }

        public CommandContext Copy()
        {
            return new CommandContext(new List<ParsedArgument>(Results), Range, Executable);
        }
    }
}
