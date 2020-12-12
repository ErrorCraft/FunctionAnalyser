using CommandParser.Collections;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class OperationArgument : IArgument<Operation>
    {
        public ReadResults Parse(IStringReader reader, out Operation result)
        {
            result = default;
            int start = reader.GetCursor();
            while (!reader.AtEndOfArgument()) reader.Skip();
            string operation = reader.GetString()[start..reader.GetCursor()];

            if (!Operations.Contains(operation))
            {
                return new ReadResults(false, CommandError.InvalidOperation());
            }

            result = new Operation(operation);
            return new ReadResults(true, null);
        }
    }
}
