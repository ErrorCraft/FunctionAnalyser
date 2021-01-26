using CommandParser.Results;
using CommandParser.Results.Arguments;
using Newtonsoft.Json;

namespace CommandParser.Arguments
{
    public class ObjectiveArgument : IArgument<Objective>
    {
        [JsonProperty("use_bedrock")]
        private readonly bool UseBedrock;

        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Objective result)
        {
            result = default;
            ReadResults readResults;
            string objective;

            if (UseBedrock) readResults = reader.ReadString(out objective);
            else readResults = reader.ReadUnquotedString(out objective);

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
