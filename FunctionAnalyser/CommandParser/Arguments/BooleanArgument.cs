using CommandParser.Results;

namespace CommandParser.Arguments
{
    public class BooleanArgument : IArgument<bool>
    {
        public ReadResults Parse(StringReader reader, out bool result)
        {
            return reader.ReadBoolean(out result);
        }
    }
}
