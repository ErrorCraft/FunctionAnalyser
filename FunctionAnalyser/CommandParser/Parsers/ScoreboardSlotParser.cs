using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Parsers
{
    public class ScoreboardSlotParser
    {
        private readonly IStringReader StringReader;

        public ScoreboardSlotParser(IStringReader stringReader)
        {
            StringReader = stringReader;
        }

        public ReadResults Read(out ScoreboardSlot result)
        {
            result = default;
            ReadResults readResults = StringReader.ReadUnquotedString(out string slot);
            if (!readResults.Successful) return readResults;

            string[] values = slot.Split('.');
            if (Collections.ScoreboardSlots.TryGetSlot(values[0], out Collections.ScoreboardSlot contents))
            {
                if (contents.Read(slot.Substring(values[0].Length)))
                {
                    result = new ScoreboardSlot(slot);
                    return new ReadResults(true, null);
                }
            }
            return new ReadResults(false, CommandError.UnknownDisplaySlot(slot));
        }
    }
}
