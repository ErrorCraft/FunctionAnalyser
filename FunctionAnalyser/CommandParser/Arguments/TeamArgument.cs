using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class TeamArgument : IArgument<Team>
    {
        public ReadResults Parse(StringReader reader, out Team result)
        {
            ReadResults readResults = reader.ReadUnquotedString(out string team);
            result = new Team(team);
            return readResults;
        }
    }
}
