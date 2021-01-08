using CommandParser.Results;

namespace CommandParser.Arguments
{
    public class BooleanArgument : IArgument<bool>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out bool result)
        {
            return reader.ReadBoolean(out result);
        }
    }
}
