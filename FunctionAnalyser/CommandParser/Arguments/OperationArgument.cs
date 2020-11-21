using CommandParser.Collections;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class OperationArgument : IArgument<Operation>
    {
        public ReadResults Parse(StringReader reader, out Operation result)
        {
            result = default;
            int start = reader.Cursor;
            while (!reader.AtEndOfArgument()) reader.Skip();
            string operation = reader.Command[start..reader.Cursor];

            if (!Operations.Contains(operation))
            {
                return new ReadResults(false, CommandError.InvalidOperation());
            }

            result = new Operation(operation);
            return new ReadResults(true, null);
        }
    }
}
