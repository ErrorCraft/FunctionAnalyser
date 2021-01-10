using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class ColourArgument : IArgument<Colour>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Colour result)
        {
            result = default;
            ReadResults readResults = reader.ReadUnquotedString(out string colour);
            if (!readResults.Successful) return readResults;

            if (!resources.Colours.Contains(colour))
            {
                return new ReadResults(false, CommandError.UnknownColour(colour));
            }

            result = new Colour(colour);
            return new ReadResults(true, null);
        }
    }
}
