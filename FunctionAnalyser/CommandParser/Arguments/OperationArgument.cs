using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class OperationArgument : IArgument<Operation>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Operation result)
        {
            result = default;
            int start = reader.GetCursor();
            while (!reader.AtEndOfArgument()) reader.Skip();
            string operation = reader.GetString()[start..reader.GetCursor()];

            if (!resources.Operations.Contains(operation))
            {
                return ReadResults.Failure(CommandError.InvalidOperation());
            }

            result = new Operation(operation);
            return ReadResults.Success();
        }
    }
}
