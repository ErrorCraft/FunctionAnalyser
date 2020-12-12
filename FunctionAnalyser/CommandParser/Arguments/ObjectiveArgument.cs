using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class ObjectiveArgument : IArgument<Objective>
    {
        public ReadResults Parse(IStringReader reader, out Objective result)
        {
            result = default;
            ReadResults readResults = reader.ReadUnquotedString(out string objective);
            if (!readResults.Successful) return readResults;

            if (objective.Length > 16)
            {
                return new ReadResults(false, CommandError.ObjectiveNameTooLong());
            }
            result = new Objective(objective);
            return new ReadResults(true, null);
        }
    }
}
