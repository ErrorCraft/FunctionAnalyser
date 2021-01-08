using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class TeamArgument : IArgument<Team>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Team result)
        {
            ReadResults readResults = reader.ReadUnquotedString(out string team);
            result = new Team(team);
            return readResults;
        }
    }
}
