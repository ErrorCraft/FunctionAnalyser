using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class ScoreboardSlotArgument : IArgument<ScoreboardSlot>
    {
        public ReadResults Parse(StringReader reader, out ScoreboardSlot result)
        {
            return new ScoreboardSlotParser(reader).Read(out result);
        }
    }
}
